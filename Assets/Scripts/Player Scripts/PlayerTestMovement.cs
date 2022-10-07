using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestMovement : MonoBehaviour
{
    //declare variables
    float distToGround;
    private Rigidbody2D body;
    BoxCollider2D collider_player;
    Vector2 otherForcesInitialJump;
    Vector2 otherForces;
    float adaptiveForceX;
    float counterWallJump;

    //TRIGERS
    bool triggerWallJumpLeft;
    bool triggerWallJumpRight;
    bool triggerDoubleJump;

    // SERIALIZED FIELDS
    [SerializeField] float wallJumpStrength;
    [SerializeField] float speed;
    [SerializeField] private LayerMask doNotCollide;
    [SerializeField] float jumpStrength;
    [SerializeField] float otherSpeed;
    [SerializeField] float otherForcesY;
    [SerializeField] float frictionFactor;
    [SerializeField] float frictionFactorOnMove;

    // Start is called before the first frame update
    void Start()
    {
        counterWallJump = 0;
        body = GetComponent<Rigidbody2D>();
        collider_player = GetComponent<BoxCollider2D>();
        distToGround = collider_player.bounds.extents.y;

        //Sets layers not to collide with
        doNotCollide = ~doNotCollide;
    }

    // Update is called once per frame
    void Update()
    {

        body.freezeRotation = true;

        ResetTriggers();
        WalkDefualt();
        Jump();
        DoubleJump();

    }

    private void FixedUpdate()
    {
        //otherForces = new Vector2(Mathf.Clamp(otherForces.x * (1 - Time.deltaTime * 0.1f), 0, otherSpeed), otherForces.y);
    }


    //Walk left and right
    void WalkDefualt()
    {

        //NOR LEFT NOR RIGHT
        if (triggerWallJumpLeft == false && triggerWallJumpRight == false)
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        }

        //LEFT
        if (triggerWallJumpLeft == true)
        {
            counterWallJump += frictionFactor * Time.deltaTime;
            otherForces = new Vector2(Mathf.Clamp(otherForces.x - counterWallJump / otherForces.x, 0, otherSpeed), otherForces.y);

            //Initial bounce off the wall
            otherForcesInitialJump = new Vector2(Mathf.Clamp(Time.deltaTime * 10, 0, otherSpeed), Mathf.Clamp(Time.deltaTime * 10, 0, otherSpeed));
            if (otherForcesInitialJump.x == otherSpeed)
            {
                otherForcesInitialJump.x = 0;
            }
            // END


            if (Input.GetAxis("Horizontal") > 0)
            {
                if (otherForces.x != 0)
                {
                    body.velocity = new Vector2(speed / 2 + otherForces.x, body.velocity.y);
                }
                else
                {
                    body.velocity = new Vector2(speed, body.velocity.y);
                }
                counterWallJump += frictionFactorOnMove * Time.deltaTime;
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                counterWallJump += frictionFactorOnMove * Time.deltaTime;
                body.velocity = new Vector2(-speed + otherForces.x, body.velocity.y);
            }

            if (Input.GetAxis("Horizontal") == 0)
            {
                body.velocity = new Vector2(otherForces.x, body.velocity.y);
            }
        }

        //RIGHT

        if (triggerWallJumpRight == true)
        {
            counterWallJump += frictionFactor * Time.deltaTime;
            otherForces = new Vector2(Mathf.Clamp(otherForces.x + counterWallJump / Mathf.Abs(otherForces.x), -otherSpeed, 0), otherForces.y);

            //Initial bounce off the wall
            otherForcesInitialJump = new Vector2(Mathf.Clamp(Time.deltaTime * 10, 0, otherSpeed), Mathf.Clamp(Time.deltaTime * 10, 0, otherSpeed));
            if (otherForcesInitialJump.x == otherSpeed)
            {
                otherForcesInitialJump.x = 0;
            }
            // END


            if (Input.GetAxis("Horizontal") < 0)
            {
                if (otherForces.x != 0)
                {
                    body.velocity = new Vector2(-speed / 2 + otherForces.x, body.velocity.y);
                }
                else
                {
                    body.velocity = new Vector2(-speed, body.velocity.y);
                }
                counterWallJump += frictionFactorOnMove * Time.deltaTime;
            }

            if (Input.GetAxis("Horizontal") > 0)
            {
                counterWallJump += frictionFactorOnMove * Time.deltaTime;
                body.velocity = new Vector2(speed + otherForces.x, body.velocity.y);
            }

            if (Input.GetAxis("Horizontal") == 0)
            {
                body.velocity = new Vector2(otherForces.x, body.velocity.y);
            }
        }


    }

    //END

    //Jumping
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (IsGrounded(doNotCollide))
            {
                triggerDoubleJump = true;
                body.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
            }
            else if (IsGroundedLeft(doNotCollide))
            {
                triggerWallJumpLeft = true;
                triggerWallJumpRight = false;
                otherForces = new Vector2(otherSpeed, 0);
                body.AddForce(new Vector2(0, wallJumpStrength), ForceMode2D.Impulse);
                counterWallJump = 0;
            }

            else if (IsGroundedRight(doNotCollide))
            {
                triggerWallJumpRight = true;
                triggerWallJumpLeft = false;
                otherForces = new Vector2(-otherSpeed, 0);
                body.AddForce(new Vector2(-otherSpeed, wallJumpStrength), ForceMode2D.Impulse);
                counterWallJump = 0;

            }

        }
    }
    //END

    // DOUBLE JUMP
    void DoubleJump()
    {
        if (triggerDoubleJump == true && Input.GetKeyDown(KeyCode.W) && IsGrounded(doNotCollide) == false && IsGroundedLeft(doNotCollide) == false && IsGroundedRight(doNotCollide) == false)
        {
            body.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
            triggerDoubleJump = false;
        }
    }
    // END

    //Is grounded and touching walls checks methods

    bool IsGrounded(LayerMask mask)
    {
        Debug.DrawRay(transform.position, Vector2.down * distToGround, Color.red);
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.3f, mask.value);
    }

    bool IsGroundedRight(LayerMask mask)
    {
        return Physics2D.Raycast(transform.position, Vector2.right, distToGround + 0.5f, mask.value);
    }
    bool IsGroundedLeft(LayerMask mask)
    {
        return Physics2D.Raycast(transform.position, Vector2.left, distToGround + 0.5f, mask.value);
    }

    //END

    //reset triggers if on ground
    void ResetTriggers()
    {
        if (IsGrounded(doNotCollide))
        {
            triggerWallJumpLeft = false;
            triggerWallJumpRight = false;
        }
    }
    //END

}