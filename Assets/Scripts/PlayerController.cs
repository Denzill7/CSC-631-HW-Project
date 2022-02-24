using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Rotational Speed
    public float speed = 0f;
    Vector3 forwardRotate;
    Vector3 backwardRotate;
    Vector3 leftRotate;
    Vector3 rightRotate;

    // Start is called before the first frame update
    void Start()
    {
        forwardRotate = new Vector3(10, 0, 0);
        backwardRotate = new Vector3(-10, 0, 0);
        leftRotate = new Vector3(0, 0, 10);
        rightRotate = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)) {
            transform.Rotate(forwardRotate);
            transform.position += Vector3.forward * Time.deltaTime * 10;
        }
        if(Input.GetKey(KeyCode.DownArrow)) {
            transform.position += Vector3.back * Time.deltaTime * 10;
            transform.Rotate(backwardRotate);
        }
        if(Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * Time.deltaTime * 10;
            transform.Rotate(rightRotate);
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * Time.deltaTime * 10;
            transform.Rotate(leftRotate);
        }
    }
}
