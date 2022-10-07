using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{

    Rigidbody2D body;
    [SerializeField] Transform target;
    BoxCollider2D collider_enemy;
    float gravity;
    float distToGround;
    float a;
    float angle;
    float coefficient;
    Vector2 p2;
    Vector2 h;
    Vector2 go;
    float x;
    float distanceX;
    float distanceY;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        gravity = Physics2D.gravity.magnitude;
        body = gameObject.GetComponent<Rigidbody2D>();

        collider_enemy = GetComponent<BoxCollider2D>();
        distToGround = collider_enemy.bounds.extents.y;

        distanceX = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(target.position.x, 0));

        distanceY = Vector2.Distance(new Vector2(0, transform.position.y), new Vector2(0, target.position.y)) + 1;

        p2 = new Vector2(distanceX, 0);
        h = new Vector2(0, distanceY + 10);

        DefineParabola();

        x = -distanceX / 2;
    }

    // Update is called once per frame
    void Update()
    {
        x += 10f * Time.deltaTime;

        coefficient = ReturnK(x);

        angle = GetAngle(coefficient);


        // GET VECTOR

        direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        Debug.Log(direction.normalized);

        body.velocity = direction * 15;

    }

    void DefineParabola()
    {
        // Y = a(x - x0)^2 + y0

        a = (p2.y - h.y) / Mathf.Pow(p2.x - h.x, 2);

    }

    float ReturnK(float x)
    {
        float k = x * Derivative(a);

        return k;
    }


    float Derivative(float a)
    {
        a = a * 2;
        return a;
    }

    float GetAngle(float k)
    {
        float angl = Mathf.Atan(k);

        return angl;
    }
}
