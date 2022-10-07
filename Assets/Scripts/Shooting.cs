using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject ShotBullet;
    public GameObject ShotBulletDmg;
    public GameObject ShotBulletSniper;

    float time;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //GameObject newBullet = Instantiate(ShotBullet, new Vector2(rb.position.x, rb.position.y), transform.rotation); SHOOT BRICK
            GameObject newBullet = Instantiate(ShotBulletSniper, new Vector2(rb.position.x, rb.position.y), transform.rotation);
        }

        if (Input.GetMouseButton(1))
        {
            Time.timeScale = 0.5f;
            //GameObject newBullet1 = Instantiate(ShotBulletDmg, new Vector2(rb.position.x, rb.position.y), transform.rotation);
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
