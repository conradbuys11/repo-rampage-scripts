using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Vector3 direction = Vector3.zero;
        if (other.GetComponentInParent<PlayerPushbox>())
        {
            if (Mathf.RoundToInt(GetComponentInParent<Transform>().GetComponentInParent<Transform>().rotation.eulerAngles.y) == 180)
            {
                other.GetComponentInParent<PlayerPushbox>().Stagger(2);
            }
            else
            {
                other.GetComponentInParent<PlayerPushbox>().Stagger(-2);
            }
            other.GetComponentInParent<PlayerPushbox>().TakeDamage(1f);
        }
    }
}
