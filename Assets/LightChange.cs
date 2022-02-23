using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public Light light;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if(light.intensity == 1)
            {
                light.intensity = 0;
            }
            else
            {
                light.intensity = 1;
            }
        }
    }
}
