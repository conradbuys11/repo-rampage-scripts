using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force_Manager : MonoBehaviour
{
    private float Strength;

    public Vector3 ForceDir;

    public void Start()
    {
        Strength = 100;
    }

    private void OnTriggerStay(Collider col)
    {
        Rigidbody colRigidbody = col.GetComponentInParent<Rigidbody>();
        if(colRigidbody != null)
        {
            colRigidbody.AddForce(ForceDir * Strength);
        }
    }
}
