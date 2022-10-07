using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemyFinal : MonoBehaviour
{
    public Rigidbody2D body;
    public GameObject targetPlayer;
    public LayerMask layer;

    // Start is called before the first frame update
    void Start()
    {
        Launch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Launch()
    {
        Vector2 Vo = CalculateVelocity(targetPlayer.transform.position, transform.position, 1f);
        //transform.rotation = Quaternion.LookRotation(Vo);
        body.velocity = Vo;
    }

    Vector2 CalculateVelocity(Vector2 target, Vector2 origin, float time)
    {
        // define the distance x and y first


        Vector2 distance = target - origin;


        Debug.Log(distance.magnitude);

        Vector2 distanceX = distance  * 1/8f;
        distanceX.y = 0f;

        // create a float that represents our distance
        float Sy = distance.y;
        float Sx = distanceX.magnitude;

        float Vx = Sx / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector2 result = distanceX.normalized;
        result *= Vx;
        result.y = Vy;

        return result;
    }

}
