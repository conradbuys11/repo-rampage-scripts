using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerFist : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Vector3 direction = Vector3.zero;
        if (other.GetComponentInParent<PlayerPushbox>())
        {
            if (Mathf.RoundToInt(GetComponentInParent<Transform>().GetComponentInParent<Transform>().rotation.eulerAngles.y) == 180)
            {
                direction = new Vector3(1000, 0, 0);
            }
            else
            {
                direction = new Vector3(-1000, 0, 0);
            }
            other.GetComponentInParent<PlayerPushbox>().TakeDamage(1f);
        }
    }
}
