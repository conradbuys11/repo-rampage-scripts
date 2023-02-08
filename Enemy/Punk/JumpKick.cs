using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpKick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Vector3 direction = Vector3.zero;
        if (other.GetComponentInChildren<PlayerPushbox>())
        {
            if (other.transform.position.x > transform.position.x)
            {
                direction = new Vector3(10, 0, 0);
            }
            else
            {
                direction = new Vector3(-10, 0, 0);
            }
            other.GetComponentInChildren<PlayerPushbox>().TakeDamage(15);
        }
    }
}
