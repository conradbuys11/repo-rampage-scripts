using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTunnel_Manager : MonoBehaviour
{

    public float Strength;

    public Vector3 WindDirection;

    void OnTriggerStay(Collider col)
    {
        Rigidbody colRigidbody = col.GetComponentInParent<Rigidbody>();
        if (colRigidbody != null)
        {
            colRigidbody.AddForce(WindDirection * Strength);
        }
    }

}
