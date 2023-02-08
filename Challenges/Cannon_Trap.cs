using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Trap : MonoBehaviour
{
    Cannon_Manager fireTrigger;
    public GameObject myCannon;
	
	void Start ()
    {
        fireTrigger = myCannon.GetComponent<Cannon_Manager>();
	}
	
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            fireTrigger.BurstFire();
            Destroy(gameObject);
        }
    }
}
