using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareUp : MonoBehaviour

    

{

    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 3) {
            body.velocity = Vector2.up * 3;
        }
    }
}
