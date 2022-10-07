
using UnityEngine;
using System.Collections;

public class ProjectileFire : MonoBehaviour
{

    [SerializeField]
    Transform target;

    Rigidbody2D body;

    Vector2 fV;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();

        fV = GetIntentPower(new Vector2(target.position.x, target.position.y), 60);

        body.AddForce(fV, ForceMode2D.Impulse);
    }


    private Vector2 GetIntentPower(Vector2 target, float initialAngle)
    {

        float gravity = Physics.gravity.magnitude;
        // Selected angle in radians

        // Planar distance between objects
        Vector2 p1 = new Vector2(transform.position.x, transform.position.y);
        Vector2 p2 = new Vector2(target.x, transform.position.y);
        float Xdistance = Vector2.Distance(p1, p2);

        // Distance along the y axis between objects
        float yOffset = transform.position.y - target.y; //Its not work


        float angle = initialAngle * Mathf.Deg2Rad;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(Xdistance, 2)) / (Xdistance * Mathf.Tan(angle) + yOffset));

        Vector2 velocity = new Vector2(initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector2.Angle(Vector2.right, p2 - p1);
        Vector2 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector2.right) * velocity;

        // Fire!
        //rigid.velocity = finalVelocity;
        return finalVelocity;

        // Alternative way:
        // rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);
    }

}

