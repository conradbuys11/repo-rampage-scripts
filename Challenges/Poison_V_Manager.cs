using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison_V_Manager : MonoBehaviour
{
    GameObject addPoison;
    GameObject removePoison;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            addPoison = other.GetComponentInParent<Rigidbody>().gameObject;
            addPoison.AddComponent<Poison>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            removePoison = other.GetComponentInParent<Rigidbody>().gameObject;
            Destroy(removePoison.GetComponent<Poison>());
        }
    }
}
