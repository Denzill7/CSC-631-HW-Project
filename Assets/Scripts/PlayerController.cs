using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Rotational Speed
    public int user_id;
    public string name;
    public float speed = 0f;
    public GameObject gun;
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    public float zAngle = 0f;
    private GameObject effectToSpawn;

    Vector3 forwardRotate;
    Vector3 backwardRotate;
    Vector3 leftRotate;
    Vector3 rightRotate;

    public PlayerController(int user_id, string name, GameObject gun)
    {
        this.user_id = user_id;
        this.name = name;
        this.gun = gun;
    }

    public static PlayerController makePlayerObject(int user_id, string name, GameObject gun)
    {
        GameObject go = new GameObject("Player" + user_id + "Gun");
        PlayerController ret = go.AddComponent<PlayerController>();
        ret.user_id = user_id;
        ret.name = name;
        ret.gun = gun;

        return ret;
    }

    // Start is called before the first frame update
    void Start()
    {
        forwardRotate = new Vector3(10, 0, 0);
        backwardRotate = new Vector3(-10, 0, 0);
        leftRotate = new Vector3(0, 0, 10);
        rightRotate = new Vector3(0, 0, -10);

        zAngle = 0f;
        effectToSpawn = vfx[0];
    }

    public void RotateLeft()
    {
        zAngle = zAngle - 2;
        gun.transform.Rotate(0, 0, zAngle);
        zAngle = 0;
    }

    public void RotateRight()
    {
        zAngle = zAngle + 2;
        gun.transform.Rotate(0, 0, zAngle);
    }

    public void MoveUp()
    {
        gun.transform.position += Vector3.up;
    }

    public void MoveDown()
    {
        gun.transform.position -= Vector3.up;
    }

    public void FireGun()

    {
        Instantiate(effectToSpawn, firePoint.transform.position, firePoint.transform.rotation);
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
