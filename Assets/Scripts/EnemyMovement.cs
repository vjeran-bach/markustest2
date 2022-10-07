using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float speed = 10f;
    public Transform pos;
    public Rigidbody2D body;
    bool switch1 = true;
    BoxCollider2D collider_enemy;
    HealthPlayer playerHealth;
    bool switch2 = true;
    private float[] values = new float[] {0.5f, 1f, 1.5f};
    bool switch3 = false;
    float distToGround;
    public bool switchCliffJump = true;
    [SerializeField] float jumpForce = 4.5f;
    float range = 40;
    GameObject player;
    Transform playerTransform;
    float distancePlayer;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider_enemy = GetComponent<BoxCollider2D>();
        distToGround = collider_enemy.bounds.extents.y;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        body.freezeRotation = true;

        var pos = GameObject.Find("Player").transform.position;

        distancePlayer = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(pos.x, 0));




        if (Vector2.Distance(new Vector2(0, transform.position.y), new Vector2(0, pos.y)) < 8)
        {
            switch3 = true;
        }

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(pos.x, pos.y)) < range && switch3 == true)
        {
            if (transform.position.x > pos.x)
        {
            if (Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(pos.x, 0)) < 1.3f)
                {
                body.velocity = new Vector2(0, body.velocity.y);
             }   
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(pos.x, pos.y)) < 3f)
            {
                body.velocity = new Vector2(0, body.velocity.y);
                DamagePlayer();
            }
            else
            {
                body.velocity = new Vector2(-speed, body.velocity.y);
                    if (CheckEdges() == false && switchCliffJump == true)
                    {
                        switchCliffJump = false;
                        body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                        StartCoroutine(DoTimerCliffJump());
                    }

                }
            
        }
        else if (transform.position.x < pos.x)
        {
        if (Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(pos.x, 0)) < 1.3f)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(pos.x, pos.y)) < 3f)
        {
              body.velocity = new Vector2(0, body.velocity.y);
              DamagePlayer();

            }
            else
            {
                body.velocity = new Vector2(speed, body.velocity.y);
                    if (CheckEdgesRight() == false && switchCliffJump == true)
                    {
                        switchCliffJump = false;
                        body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                        StartCoroutine(DoTimerCliffJump());
                    }
                }
               
        }

        if (transform.position.y < pos.y && switch1 == true && (Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(pos.x, 0)) < 25) && CheckEdges() == true && CheckEdgesRight() == true && IsGrounded() == true)
        {
            StartCoroutine(DoTimer());
            switch1 = false;
        }

        /*if(transform.position.y > pos.y && switch1 == true && ((Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(pos.x, 0)) < 15)))
        {
            
        }*/
        }

        CheckEdges();
        CheckEdgesRight();
        
    }


    IEnumerator DoTimer(float countTime = 0.1f)
    {
        float count = 0;

        while (true)
        {
            yield return new WaitForSeconds(countTime);
            count += 0.1f;
            
            if (count == 0.2f && IsGrounded() == true) {
                body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }

            if (IsGrounded() && count > 2)
            {
                switch1 = true;
                yield break;
            }
        }
    }

    IEnumerator DoTimerCliffJump(float countTime = 0.1f)
    {
        float count = 0;

        while (true)
        {
            yield return new WaitForSeconds(countTime);
            count += 0.1f;

            if (count >= 1f && IsGrounded())
            {
                switchCliffJump = true;
                yield break;
            }
        }
    }


    bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector2.down * distToGround, Color.red);
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.3f, 7);
    }

    void DamagePlayer ()
    {
        if (switch2 == true) {
            StartCoroutine(DoTimerDamage());
            switch2 = false;
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

    bool CheckEdges()
    {
        Debug.DrawRay(new Vector2(transform.position.x - 1, transform.position.y), Vector2.down * distToGround + Vector2.down, Color.red);
        return Physics2D.Raycast(new Vector2(transform.position.x - 1, transform.position.y), Vector2.down, distToGround + 1, 7);
    }

    bool CheckEdgesRight()
    {
        Debug.DrawRay(new Vector2(transform.position.x + 1, transform.position.y), Vector2.down * distToGround + Vector2.down, Color.red);
        return Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y), Vector2.down, distToGround + 1, 7);
    }
}
