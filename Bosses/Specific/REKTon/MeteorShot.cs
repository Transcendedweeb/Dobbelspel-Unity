using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShot : MonoBehaviour
{
    public GameObject player;
    public float speed = 20f;
    bool hasPassedPlayer = false;
    bool invoked = false;
    Vector3 forwardDirection;

    void Update()
    {
        if (player != null)
        {
            if (!hasPassedPlayer)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
                if (Vector3.Dot(direction, (player.transform.position - transform.position).normalized) < 0)
                {
                    hasPassedPlayer = true;
                    forwardDirection = direction;
                }
            }
            else
            {
                if (!invoked) Invoke("DestroySelf", 2f);
                transform.position += forwardDirection * speed * Time.deltaTime;
            }
        }
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
