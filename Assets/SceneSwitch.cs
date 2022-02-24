using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            switch(SceneManager.GetActiveScene().buildIndex) {
                case 0: SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); break;
                case 1: SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); break;
                case 2: SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2); break;
            }
            // if (SceneManager.GetActiveScene().buildIndex == 1) {
            //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            // }
            // else if (SceneManager.GetActiveScene().buildIndex == 0)
            // {
            //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            // }
            // else
            // {
            //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            // }
        }
    }
}
