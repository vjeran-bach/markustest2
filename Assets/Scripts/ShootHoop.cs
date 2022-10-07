using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHoop : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float h;

    float gravity;

    Rigidbody2D body;

    float xOffset;
    float yOffset;

    float velUp;
    float velRight;

    // Start is called before the first frame update
    void Start()
    {
        gravity = Physics2D.gravity.magnitude;

        body = gameObject.GetComponent<Rigidbody2D>();

        xOffset = target.position.x - transform.position.x;
        yOffset = target.position.y - transform.position.y;

        velUp = Mathf.Sqrt(2 * gravity * h);
        velRight = (xOffset / (Mathf.Sqrt((-2 * yOffset) / -gravity)) + Mathf.Sqrt((2 * (yOffset - h) / -gravity)));

        body.AddForce(new Vector2(velRight, velUp) * body.mass, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
