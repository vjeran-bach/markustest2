using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class FlyingEnemyMovement : MonoBehaviour
{


    Rigidbody2D body;
    [SerializeField]public float moveSpeed;
    Vector2 pos;
    bool switch2 = true;
    HealthPlayer playerHealth;


    // Use this for initialization
    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector2 pos = player.transform.position;

        Vector2 dir = pos - new Vector2(transform.position.x, transform.position.y);

        if (Vector2.Distance(transform.position, pos) < 25)
        {
            body.velocity = dir.normalized * moveSpeed;
        }

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(pos.x, pos.y)) < 1)
        {
            DamagePlayer();
        }

        void DamagePlayer()
        {
            if (switch2 == true)
            {
                StartCoroutine(DoTimerDamage());
                switch2 = false;
            }
        }
    }

    IEnumerator DoTimerDamage(float countTime = 0.1f)
    {
        float count = 0;

        while (true)
        {
            yield return new WaitForSeconds(countTime);
            count += 0.1f;

            if (count == 0.3f)
            {
                playerHealth.health -= 1;
                Debug.Log("DAMAGE");
            }

            if (count > 5f)
            {
                switch2 = true;
                yield break;
            }
        }
    }
}

