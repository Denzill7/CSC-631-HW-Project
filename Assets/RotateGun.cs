using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    public float zAngle = 0f;

    public GameObject gun;
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();

    private GameObject effectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
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
}
