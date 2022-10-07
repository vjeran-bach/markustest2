using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunBullet : MonoBehaviour
{

    Transform playerTransform;

    [SerializeField] float speed;

    Rigidbody2D body;

    Vector2 direction;


    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        body = gameObject.GetComponent<Rigidbody2D>();

        Vector2 direction = new Vector2(playerTransform.position.x, playerTransform.position.y) - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        body.velocity = direction.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Object.Destroy(gameObject);
    }
}
