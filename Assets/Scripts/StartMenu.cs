using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private GameObject playButton;
    private GameObject loginButton;
    private GameObject registerButton;
    private GameObject startGameButton;
    private GameObject networkMenu;
    private NetworkManager networkManager;
    private GameManager gameManager;
    private MessageQueue msgQueue;
	private GameObject gun;

	public TMPro.TextMeshProUGUI player1Name;
	public TMPro.TextMeshProUGUI player2Name;
	private GameObject player1Input;
	private GameObject player2Input;

	private string p1Name = "Player 1";
	private string p2Name = "Player 2";

	private TMPro.TextMeshProUGUI playerName;
	private TMPro.TextMeshProUGUI opponentName;
	private GameObject playerInput;
	private GameObject opponentInput;

	public TMPro.TMP_InputField player1InputField;
	public TMPro.TMP_InputField player2InputField;
	private TMPro.TMP_InputField playerInputField;
	private TMPro.TMP_InputField opponentInputField;

	private bool ready = false;
	private bool opReady = false;
	// Start is called before the first frame update
	void Start()
    {
        playButton = GameObject.Find("Play Button");
        networkMenu = GameObject.Find("NetworkMenu");
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        msgQueue = networkManager.GetComponent<MessageQueue>();
		gun = GameObject.Find("Gun");

        // add callbacks
        msgQueue.AddCallback(Constants.SMSG_JOIN, OnResponseJoin);
		//msgQueue.AddCallback(Constants.SMSG_LEAVE, OnResponseLeave);
		msgQueue.AddCallback(Constants.SMSG_SETNAME, OnResponseSetName);
		msgQueue.AddCallback(Constants.SMSG_READY, OnResponseReady);

		//player1Name = GameObject.Find("Player1Name").GetComponent<TMPro.TextMeshProUGUI>();
		//player2Name = GameObject.Find("Player2Name").GetComponent<TMPro.TextMeshProUGUI>();
		player1Input = GameObject.Find("Player1Input");
		player2Input = GameObject.Find("Player2Input");
	}

	public void OnResponseJoin(ExtendedEventArgs eventArgs)
	{
		ResponseJoinEventArgs args = eventArgs as ResponseJoinEventArgs;
		if (args.status == 0)
		{
			if (args.user_id == 1)
			{
				player1Name.text = p1Name;
				opponentName = player2Name;
				playerInput = player1Input;
				opponentInput = player2Input;
				playerInputField = player1InputField;
				opponentInputField = player2InputField;
				Debug.Log("Player 1 has joined");
			}
			else if (args.user_id == 2)
			{
				player2Name.text = p2Name;
				opponentName = player1Name;
				playerInput = player2Input;
				opponentInput = player1Input;
				playerInputField = player2InputField;
				opponentInputField = player1InputField;
				Debug.Log("Player 2 has joined");
			}
			else
			{
				Debug.Log("ERROR: Invalid user_id in ResponseJoin: " + args.user_id);
				//messageBoxMsg.text = "Error joining game. Network returned invalid response.";
				//messageBox.SetActive(true);
				return;
			}
			Constants.USER_ID = args.user_id;
			Constants.OP_ID = 3 - args.user_id;

			if (args.op_id > 0)
			{
				if (args.op_id == Constants.OP_ID)
				{
					opponentName.text = args.op_name;
					opReady = args.op_ready;
				}
				else
				{
					Debug.Log("ERROR: Invalid op_id in ResponseJoin: " + args.op_id);
					//messageBoxMsg.text = "Error joining game. Network returned invalid response.";
					//messageBox.SetActive(true);
					return;
				}
			}
			else
			{
				opponentName.text = "Waiting for opponent";
			}

			//playerInput.SetActive(true);
			//opponentName.gameObject.SetActive(true);
			//playerName.gameObject.SetActive(false);
			//opponentInput.SetActive(false);

			//rootMenuPanel.SetActive(false);
			//networkMenuPanel.SetActive(true);
		}
		else
		{
			//messageBoxMsg.text = "Server is full.";
			//messageBox.SetActive(true);
		}
	}

	public void OnResponseSetName(ExtendedEventArgs eventArgs)
	{
		ResponseSetNameEventArgs args = eventArgs as ResponseSetNameEventArgs;
		if (args.user_id != Constants.USER_ID)
		{
			opponentName.text = args.name;
			if (args.user_id == 1)
			{
				player1Name.text = args.name;
			}
			else
			{
				player2Name.text = args.name;
			}
		}
	}

	public void OnPlayerNameSet()
	{
		string name = playerInputField.text;
		Debug.Log("Send SetNameReq: " + name);
		networkManager.SendSetNameRequest(name);
		if (Constants.USER_ID == 1)
		{
			player1Name.text = name;
		}
		else
		{
			player2Name.text = name;
		}
	}

	public void onPlayClick()
    {
        Debug.Log("Send JoinReq");
        bool connected = networkManager.SendJoinRequest();
        if (!connected)
        {
            //messageBoxMsg.text = "Unable to connect to server.";
            //messageBox.SetActive(true);
        }
    }

	public void OnReadyClick()
	{
		Debug.Log("Send ReadyReq");
		networkManager.SendReadyRequest();
	}

	public void OnResponseReady(ExtendedEventArgs eventArgs)
	{
		ResponseReadyEventArgs args = eventArgs as ResponseReadyEventArgs;
		if (Constants.USER_ID == -1) // Haven't joined, but got ready message
		{
			opReady = true;
		}
		else
		{
			if (args.user_id == Constants.OP_ID)
			{
				opReady = true;
			}
			else if (args.user_id == Constants.USER_ID)
			{
				ready = true;
			}
			else
			{
				Debug.Log("ERROR: Invalid user_id in ResponseReady: " + args.user_id);
				//messageBoxMsg.text = "Error starting game. Network returned invalid response.";
				//messageBox.SetActive(true);
				return;
			}
		}

		Debug.Log("Player Ready?: " + ready);
		Debug.Log("Opponent Ready?: " + opReady);

		if (ready && opReady)
		{
			StartNetworkGame();
		}
	}

	// fix typo in here
	private void StartNetworkGame()
	{
		GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		if (p1Name.Length == 0)
		{
			p1Name = "Player 1";
		}
		if (p2Name.Length == 0)
		{
			p2Name = "Player 2";
		}
		SceneManager.LoadScene("SampleScene");
		PlayerController player1 = GameObject.Find("Gun1").GetComponent<PlayerController>();      //(1, p1Name, new Color(0.9f, 0.1f, 0.1f), Constants.USER_ID == 1)
		PlayerController player2 = GameObject.Find("Gun2").GetComponent<PlayerController>();        //(2, p2Name, new Color(0.2f, 0.2f, 1.0f), Constants.USER_ID == 2)
		gameManager.Init(player1, player2);
		
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
