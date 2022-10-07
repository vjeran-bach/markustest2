using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatJumpEnemy : MonoBehaviour
{
    [SerializeField] Transform target;

    float gravity;

    Rigidbody2D body;

    float xOffset;
    float yOffset;

    float velUp;
    float velRight;

    float otherX;
    float otherY;

    bool trigger = false;

    float distance;
    float distanceX;

    float counter;

    float counterX;

    Vector2 initialVel;

    float distToGround;
    float counterY;

    bool triggerDir = false;

    float walkSpeed = 8;

    bool triggerAddForce;

    BoxCollider2D collider_enemy;

    Vector2 goToPlayer;

    float airSpeed = 7;

    float distanceY;
    // Start is called before the first frame update
    void Start()
    {
        gravity = Physics2D.gravity.magnitude;
        body = gameObject.GetComponent<Rigidbody2D>();

        collider_enemy = GetComponent<BoxCollider2D>();
        distToGround = collider_enemy.bounds.extents.y;

        /*gravity = Physics2D.gravity.magnitude;

        body = gameObject.GetComponent<Rigidbody2D>();

        xOffset = target.position.x - transform.position.x;
        yOffset = target.position.y - transform.position.y + 1;

        distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(target.position.x, target.position.y));

        initialVel = new Vector2(xOffset, yOffset).normalized * distance * 1.5f;

        body.AddForce(initialVel, ForceMode2D.Impulse);*/

        StartCoroutine(DoTimer(1));

    }

    // Update is called once per frame
    void Update()
    {

        GroundWalkToPlayer();


        distanceX = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(target.position.x, 0));
        otherX = (target.position.x - transform.position.x);
        otherY = (target.position.y - transform.position.y);

        if (distanceX < 3 && trigger == true)
        {
            counter += 0.5f * Time.deltaTime;
            counterY += 0.5f * Time.deltaTime;
        }

        if (trigger == true && distanceX > 0.1f)
        {
            if (IsGrounded() == false)
            {
                if ((transform.position.x > target.position.x && triggerDir == true) || (transform.position.y > target.position.y + 5 && triggerDir == true))
                {
                    body.velocity = new Vector2(body.velocity.x + Mathf.Clamp(counter, 0, Mathf.Abs(body.velocity.x) - 0.5f + airSpeed), body.velocity.y - Mathf.Clamp(counterY, 0, body.velocity.y + 25));
                }
                else if ((transform.position.x < target.position.x && triggerDir == false) || (transform.position.y > target.position.y + 5 && triggerDir == false))
                {
                    body.velocity = new Vector2(body.velocity.x - Mathf.Clamp(counter, 0, body.velocity.x - 0.5f + airSpeed), body.velocity.y - Mathf.Clamp(counterY, 0, body.velocity.y + 25));
                }

            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (trigger == true)
        {
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }


    void jump()
    {
        body.velocity = new Vector2(0, 0);
        //trigger = true; TO BE REMOVED
        counter = 0;
        counterY = 0;

        triggerAddForce = true;

        StartCoroutine(DoTimerOffset(1));

        /*if (transform.position.x < target.position.x)
        {
            xOffset = target.position.x - transform.position.x;
        }
        else
        {
            xOffset = transform.position.x - target.position.x;
        }
        yOffset = target.position.y - transform.position.y + 1;*/
        //COMMENTED BCS OF COROUTINE TEST

        /*distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(target.position.x, target.position.y));

        if (transform.position.x > target.position.x)
        {
            triggerDir = true;
            initialVel = new Vector2(-xOffset, yOffset).normalized * Mathf.Clamp(distance * 1.5f, 0, 30);
            if (initialVel.y > 0.5 && distance > 40)
            {
                initialVel = new Vector2(-xOffset, yOffset / 2f);
            }
            if (initialVel.x > 0.5 && distance > 40)
            {
                initialVel = new Vector2(-xOffset / 2.5f, yOffset);
            }
        }
        else
        {
            triggerDir = false;
            initialVel = new Vector2(xOffset, yOffset).normalized * Mathf.Clamp(distance * 1.5f, 0, 30);

            /*if (initialVel.y > 0.5 && distance > 40)
            {
                initialVel = new Vector2(xOffset, yOffset / 2f);
            }
            if (initialVel.x > 0.5 && distance > 40)
            {
                initialVel = new Vector2(xOffset / 2.5f, yOffset);
            }

        }

        body.AddForce(initialVel, ForceMode2D.Impulse);*/

        StartCoroutine(DoTimer(1));

        StartCoroutine(DoTimerBreakTrigger(1));
    }

    IEnumerator DoTimer(float countTime = 0.1f)
    {
        int count = 0;

        while (true)
        {
            yield return new WaitForSeconds(countTime);
            count++;

            if (count >= Random.Range(1, 6) && IsGrounded() == true)
            {
                jump();
                yield break;
            }
        }
    }

    bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector2.down * distToGround, Color.red);
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.3f, 7);
    }




    IEnumerator DoTimerBreakTrigger(float countTime = 0.1f)
    {
        int count = 0;

        while (true)
        {
            yield return new WaitForSeconds(countTime);
            count++;

            if (count > 0)
            {
                BreakTrigger();
                yield break;
            }
        }
    }





    void BreakTrigger()
    {
        if (IsGrounded() == true)
        {
            //body.velocity = new Vector2(0, body.velocity.y); REMOVE
            trigger = false;
        }
    }

    void GroundWalkToPlayer()
    {
        if (IsGrounded() && otherX > 0 && trigger == false)
        {
            body.velocity = new Vector2(walkSpeed, body.velocity.y);
        }
        else if (IsGrounded() && otherX < 0 && trigger == false)
        {
            body.velocity = new Vector2(-walkSpeed, body.velocity.y);
        }
    }


    IEnumerator DoTimerOffset(float countTime = 0.1f)
    {
        int count = 0;

        while (true)
        {
            yield return new WaitForSeconds(countTime);
            count++;
            if (IsGrounded())
            {
                if (transform.position.x < target.position.x)
                {
                    xOffset = target.position.x - transform.position.x;
                }
                else
                {
                    xOffset = transform.position.x - target.position.x;
                }
                yOffset = target.position.y - transform.position.y + 1;

                distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(target.position.x, target.position.y));
                distanceX = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(target.position.x, 0));
                distanceY = Vector2.Distance(new Vector2(0, transform.position.y), new Vector2(0, target.position.y));

                if (transform.position.x > target.position.x)
                {
                    triggerDir = true;
                    initialVel = new Vector2(-xOffset, yOffset).normalized * Mathf.Clamp(distance * 1.5f, 0, 15);
                    if (distanceX > 20)
                    {
                        initialVel = new Vector2(-xOffset, yOffset / 3f);
                    }
                    if (distanceY > 20)
                    {
                        initialVel = new Vector2(-xOffset / 3.5f, yOffset);
                    }
                }
                else
                {
                    triggerDir = false;

                    initialVel = new Vector2(xOffset, yOffset).normalized * Mathf.Clamp(distance * 1.5f, 0, 15);
                    if (distanceX > 20)
                    {
                        initialVel = new Vector2(xOffset, yOffset / 3f);
                    }
                    if (distanceY > 20)
                    {
                        initialVel = new Vector2(xOffset / 3.5f, yOffset);
                    }
                }

                if (triggerAddForce == true)
                {
                    initialVel = new Vector2(initialVel.x, Mathf.Clamp(initialVel.y, 0, 14));
                    body.AddForce(initialVel, ForceMode2D.Impulse);
                    triggerAddForce = false;
                }
            }

            if (transform.position.x > target.position.x)
            {
                goToPlayer = new Vector2(-xOffset, 0).normalized;
            }
            else if (transform.position.x < target.position.x)
            {
                goToPlayer = new Vector2(xOffset, 0).normalized;
            }

            //if (distance < 8 && distance > 3)
            //{
                //body.velocity = new Vector2(body.velocity.x + goToPlayer.x * airSpeed, body.velocity.y);
           //}


            if (count >= 2 && IsGrounded() == true)
            {
                triggerAddForce = true;
                yield break;
            }
        }
    }

}