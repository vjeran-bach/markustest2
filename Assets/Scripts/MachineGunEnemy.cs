using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunEnemy : MonoBehaviour
{

    Transform playerTransform;

    [SerializeField] GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        StartCoroutine(TimerShoot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimerShoot(float countTime = 0.11f)
    {
        float count = 0;

        while (true)
        {
            yield return new WaitForSeconds(countTime);
            count += 1f;

            if (count >= 3f)
            {
                GameObject newBullet = Instantiate(bullet, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), transform.rotation);
                StartCoroutine(TimerShoot());
                Debug.Log("SHOOT");
                yield break;
            }
        }
    }


}
