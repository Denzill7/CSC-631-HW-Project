using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public Transform beamCheck;
    public LayerMask wallMask;
    public LayerMask gunMask;
    public ParticleSystem explosion;
    static float score = 0f;
    public TextMeshProUGUI scoreText;

    bool isTouchingWall;
    bool isTouchingGun;

    public Material wallMaterial;
    

    void Start()
    {
        explosion.Stop();
        isTouchingWall = false;
        isTouchingGun = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position -= transform.right * (speed * Time.deltaTime);
        }
        isTouchingWall = Physics.CheckSphere(beamCheck.position, 1f, wallMask);
        isTouchingGun = Physics.CheckSphere(beamCheck.position, 0.5f, gunMask);

        if (isTouchingWall)
        {
            Destroy(gameObject);
            explosion.transform.position = gameObject.transform.position;
            explosion.Play();

            // make a random color
            Color randomColor = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
        );
            wallMaterial.color = randomColor;
            isTouchingWall = false;
        }

        if (isTouchingGun)
        {
            Destroy(gameObject);
            explosion.transform.position = gameObject.transform.position;
            explosion.Play();
            // change score text
            score++;
            scoreText.text = "Score: " + score;
            isTouchingGun = false;
        }

    }
}
