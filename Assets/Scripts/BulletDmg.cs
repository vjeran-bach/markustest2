using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BulletDmg : MonoBehaviour
{


    Rigidbody2D body;
    [SerializeField]public float moveSpeed;
    Vector3 mousePos;
    private Camera cam;
 


    // Use this for initialization

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Health>().health -= 1f;
        }

        Destroy(gameObject);
    }

    void Awake()
    {

        Camera cam = Camera.main;

        body = GetComponent<Rigidbody2D>();

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePos - transform.position;

        body.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;

        StartCoroutine(DoTimer(1));

        //body.AddForce(new Vector2(direction.x, direction.y).normalized * moveSpeed);


    }

    // Update is called once per frame
    void Update()
    {


        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Vector3 direction = mousePos - transform.position;

        //body.AddForce(new Vector2(direction.x, direction.y).normalized * moveSpeed);

    }

    IEnumerator DoTimer(float countTime = 1f)
    {
        int count = 0;

        while (true)
        {
            yield return new WaitForSeconds(countTime);
            count++;
            Debug.Log(count);

            if (count == 1) {
                gameObject.layer = 0;
                yield break;
            }
        }
    }
}

