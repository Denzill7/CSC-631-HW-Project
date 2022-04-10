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

	private TMPro.TextMeshProUGUI player1Name;
	private TMPro.TextMeshProUGUI player2Name;
	private GameObject player1Input;
	private GameObject player2Input;

	private string p1Name = "Player 1";
	private string p2Name = "Player 2";

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

        // add callbacks
        msgQueue.AddCallback(Constants.SMSG_JOIN, OnResponseJoin);
		//msgQueue.AddCallback(Constants.SMSG_LEAVE, OnResponseLeave);
		//msgQueue.AddCallback(Constants.SMSG_SETNAME, OnResponseSetName);
		//msgQueue.AddCallback(Constants.SMSG_READY, OnResponseReady);

		player1Name = GameObject.Find("Player1Name").GetComponent<TMPro.TextMeshProUGUI>();
		player2Name = GameObject.Find("Player2Name").GetComponent<TMPro.TextMeshProUGUI>();
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
				//playerName = player1Name;
				//opponentName = player2Name;
				//playerInput = player1Input;
				//opponentInput = player2Input;
				Debug.Log("Player 1 has joined");
			}
			else if (args.user_id == 2)
			{
				//playerName = player2Name;
				//opponentName = player1Name;
				//playerInput = player2Input;
				//opponentInput = player1Input;
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
					//opponentName.text = args.op_name;
					//opReady = args.op_ready;
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
				//opponentName.text = "Waiting for opponent";
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
