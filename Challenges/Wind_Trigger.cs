using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_Trigger : MonoBehaviour
{
    G_Fan_Manager windTrigger;
    public GameObject myFan;

    void Start()
    {
        windTrigger = myFan.GetComponent<G_Fan_Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            windTrigger.HitTheFan();
            Destroy(gameObject);
        }
    }
}
