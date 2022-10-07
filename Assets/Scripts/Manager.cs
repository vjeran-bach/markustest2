using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 4)
        {
            Instantiate(Enemy, new Vector2(Random.Range(-30, 20), 50), transform.rotation);
            Debug.Log("NO MORE");
        }

    }
}
