using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KneeThrust : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Vector3 direction = Vector3.zero;
        if (other.GetComponentInParent<PlayerPushbox>())
        {
            if (Mathf.RoundToInt(GetComponentInParent<Transform>().GetComponentInParent<Transform>().rotation.eulerAngles.y) == 180)
            {
                print("Push");
                direction = new Vector3(1000, 0, 0);
            }
            else
            {
                print("Push Back");
                direction = new Vector3(-1000, 0, 0);
            }
            other.GetComponentInParent<PlayerPushbox>().TakeDamage(1f);
        }
    }
}
