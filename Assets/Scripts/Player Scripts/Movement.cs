using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    float distToGround;
    private Rigidbody2D body;
    [SerializeField] float speed = 8.5f;
    BoxCollider2D collider_player;
    Vector2 otherForces;
    float jumpStrength = 30;
    float wallJumpStrength = 25;
    float counter = 0;
    bool wallJumpSwitch = false;
    bool movementCounterSwitch = false;
    float counter2 = 0;
    bool wallJumpSwitchRight = false;
    float counter3;
    float otherSpeed;
    bool movementCounterSwitchRight = false;
    float counter4 = 0;
    [SerializeField] float wallJumpMovementCounterTime;
    [SerializeField] float CounterAdder;
    [SerializeField] float fallSpeed;
    float fallCounter = 0;
    bool doubleJumpSwitch = false;
    [SerializeField] private LayerMask doNotCollide;

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

    // Start is called before the first frame update
    void Start()
    {

        body = GetComponent<Rigidbody2D>();
        collider_player = GetComponent<BoxCollider2D>();
        distToGround = collider_player.bounds.extents.y;
        otherSpeed = 9f / 10f * speed;

        doNotCollide = ~doNotCollide;




    }

    // Update is called once per frame
    void Update()

    {

        body.freezeRotation = true;

        WallJumpRightSwitchTrueJumping();

        CounterSwitchRightEnabler();

        MovementSwitchRightHappening();

        WallJumpSwitchRightAdder();













        ///////////////////////////////

        WallJumpSwitchLeftTrueJumping();

        MovementCounterSwitchLeftEnabler();

        MovememntCounterSwitchLeftHappening();

        WallJumpSwitchLeftAdder();



        IfNotWallJumpingGoToDefualt();

        ResetPlayerIfIsGrounded();

        JumpingAndWallJumping();



        if (doubleJumpSwitch == true && IsGrounded(doNotCollide) == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                body.AddForce(new Vector2(0, jumpStrength /3 * 2), ForceMode2D.Impulse);
                doubleJumpSwitch = false;
            }
        }

        otherForces = new Vector2(otherForces.x * (1 - Time.deltaTime * 0.1f), otherForces.y);

        if (Input.GetKey(KeyCode.S))
        {
            fallCounter += 0.25f * Time.deltaTime;
            body.AddForce(Vector2.down * fallSpeed * Mathf.Clamp(fallCounter, 0, 1));
        }
    }

    private void JumpingAndWallJumping()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {



            if (IsGrounded(doNotCollide))
            {
                body.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
                doubleJumpSwitch = true;
            }

            else if (IsGroundedLeft(doNotCollide))
            {
                doubleJumpSwitch = false;
                body.AddForce(new Vector2(0, wallJumpStrength), ForceMode2D.Impulse);
                otherForces += new Vector2(otherSpeed, 0);

                wallJumpSwitch = true;
                wallJumpSwitchRight = false;
                movementCounterSwitch = false;
                movementCounterSwitchRight = false;


            }

            else if (IsGroundedRight(doNotCollide))
            {
                doubleJumpSwitch = false;
                body.AddForce(new Vector2(0, wallJumpStrength), ForceMode2D.Impulse);
                otherForces += new Vector2(-otherSpeed, 0);
                wallJumpSwitchRight = true;
                wallJumpSwitch = false;
                movementCounterSwitch = false;
                movementCounterSwitchRight = false;

            }


        }
    }

    private void WallJumpSwitchLeftAdder()
    {
        if (Input.GetKey(KeyCode.D) && wallJumpSwitch == true && wallJumpSwitchRight == false && movementCounterSwitchRight == false)
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed + otherForces.x, body.velocity.y + otherForces.y);
            counter2 += 2f * Time.deltaTime;
        }
    }

    private void MovememntCounterSwitchLeftHappening()
    {
        if (movementCounterSwitch == true && movementCounterSwitchRight == false && wallJumpSwitchRight == false)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                body.velocity = new Vector2(Input.GetAxis("Horizontal") * Mathf.Clamp(counter2, 0, Mathf.Abs(otherForces.x) + speed) + otherForces.x, body.velocity.y);
                counter2 += CounterAdder * Time.deltaTime;
            }
        }
    }

    private void ResetPlayerIfIsGrounded()
    {
        if (IsGrounded(doNotCollide))
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y + otherForces.y);
            otherForces.x = 0;
            movementCounterSwitch = false;
            wallJumpSwitch = false;
            wallJumpSwitchRight = false;
            movementCounterSwitchRight = false;
            counter4 = 0f;
            counter = 0f;
            counter3 = 0f;
            counter2 = 0f;
        }
    }

    private void MovementCounterSwitchLeftEnabler()
    {
        if (body.velocity.x < wallJumpMovementCounterTime && wallJumpSwitch == true && wallJumpSwitchRight == false && movementCounterSwitchRight == false)
        {
            movementCounterSwitch = true;
            counter2 = 0f;
            wallJumpSwitch = false;

        }
    }

    private void IfNotWallJumpingGoToDefualt()
    {
        if (wallJumpSwitch == false && wallJumpSwitchRight == false && movementCounterSwitch == false && movementCounterSwitchRight == false)
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y + otherForces.y);
        }
    }

    private void WallJumpSwitchLeftTrueJumping()
    {
        if (wallJumpSwitch == true && wallJumpSwitchRight == false && movementCounterSwitchRight == false)
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * Mathf.Clamp(counter, 0, Mathf.Abs(otherForces.x) + speed) + otherForces.x, body.velocity.y + otherForces.y);
            if (IsGroundedRight(doNotCollide))
            {
                otherForces = new Vector2(0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                counter += 2f * Time.deltaTime;
            }
        }
    }



    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void WallJumpSwitchRightAdder()
    {
        if (Input.GetKey(KeyCode.A) && wallJumpSwitchRight == true && wallJumpSwitch == false && movementCounterSwitch == false)
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed + otherForces.x, body.velocity.y);
            counter4 += 2f * Time.deltaTime;
        }
    }

    private void MovementSwitchRightHappening()
    {
        if (movementCounterSwitchRight == true && movementCounterSwitch == false && wallJumpSwitch == false)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                body.velocity = new Vector2(Input.GetAxis("Horizontal") * Mathf.Clamp(counter4, 0, Mathf.Abs(otherForces.x) + speed) + otherForces.x, body.velocity.y + otherForces.y);
                    counter4 += CounterAdder * Time.deltaTime;
            }
        }
    }

    private void CounterSwitchRightEnabler()
    {
        if (body.velocity.x > -wallJumpMovementCounterTime && wallJumpSwitchRight == true && wallJumpSwitch == false && movementCounterSwitch == false)
        {
            movementCounterSwitchRight = true;
            counter4 = 0f;
            wallJumpSwitchRight = false;
        }
    }

    private void WallJumpRightSwitchTrueJumping()
    {
        if (wallJumpSwitchRight == true && wallJumpSwitch == false && movementCounterSwitch == false)
        {
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * Mathf.Clamp(counter3, 0, Mathf.Abs(otherForces.x) + speed) + otherForces.x, body.velocity.y + otherForces.y);
            if (IsGroundedLeft(doNotCollide))
            {
                otherForces = new Vector2(0, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                counter3 += 2f * Time.deltaTime;
            }
        }
    }
}
