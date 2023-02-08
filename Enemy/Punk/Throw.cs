using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    float destroyTime;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<PlayerPushbox>())
        {
            Vector3 direction = Vector3.zero;
            if (other.transform.position.x > transform.position.x)
            {
                direction = new Vector3(10, 0, 0);
            }
            else
            {
                direction = new Vector3(-10, 0, 0);
            }
            other.GetComponentInChildren<PlayerPushbox>().TakeDamage(20);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        destroyTime += Time.deltaTime;
        if(destroyTime >= 4)
        {
            Destroy(gameObject);
        }
    }

}
