using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShader : MonoBehaviour
{
    public Material[] materials;
    private MeshRenderer meshRenderer;
    public int number = 0;

    void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.M)){
            if(number == 0) {
                number = 1;
            }
            else {
                number = 0;
            }
            meshRenderer.material = materials[number];
        }
    }
}
