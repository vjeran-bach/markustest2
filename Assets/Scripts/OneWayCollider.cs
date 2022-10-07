using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayCollider : MonoBehaviour
{

    BoxCollider2D platform;
    bool playerOnPlatform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        platform = gameObject.GetComponent<BoxCollider2D>();
    }


}
