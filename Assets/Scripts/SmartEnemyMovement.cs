using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartEnemyMovement : MonoBehaviour
{
    float xOffset;
    float yOffset;

    float x0;
    float y0;

    float vY;
    float vX;

    float gravity;

    float v0;

    float distance;

    [SerializeField]
    Transform target;

    float initialAngle;

    float initialVelocity;

    float xFinal;
    float yFinal;

    float xVel;
    float yVel;

    Rigidbody2D body;

    float yOffsetAngle;

    float xOffsetTest;

    // Start is called before the first frame update
    void Awake()
    {

        gravity = Physics2D.gravity.magnitude;

        body = gameObject.GetComponent<Rigidbody2D>();



        xOffset = target.position.x - transform.position.x;

        xOffsetTest = transform.position.x - target.position.x;

        yOffset = target.position.y - transform.position.y;

        yOffsetAngle = target.position.y - transform.position.y;

        x0 = 0;
        y0 = yOffset;

        yFinal = 0;

        distance = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(target.position.x, 0));

        initialAngle = Mathf.Atan2(yOffset, xOffset);


        initialVelocity = (1 / Mathf.Cos(initialAngle)) * Mathf.Sqrt((1/2f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(initialAngle) + yOffset));

        xVel = initialVelocity * Mathf.Cos(initialAngle);

        yVel = initialVelocity * Mathf.Sin(initialAngle);

        Vector2 initialVector = new Vector2(xVel, yVel);

        body.AddForce(initialVector * body.mass, ForceMode2D.Impulse);

        Debug.Log(initialVelocity);

        Debug.Log(xVel);

        Debug.Log(yVel);

        Debug.Log(initialAngle * Mathf.Rad2Deg);

        Debug.Log("OFFSETS");

        Debug.Log(xOffset);

        Debug.Log(yOffset);

        Debug.Log(distance);

        Debug.Log(gravity);
    }


    // Update is called once per frame
    void Update()
    {

    }


}
