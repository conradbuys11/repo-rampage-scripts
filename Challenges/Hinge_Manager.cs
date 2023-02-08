using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hinge_Manager : MonoBehaviour
{
    Rigidbody myHinge;

    public Payload_Manager inPlace;

	void Start ()
    {
        myHinge = GetComponent<Rigidbody>();
	}
	
	
	void Update ()
    {
		if(inPlace.station == true)
        {
            myHinge.isKinematic = false;
        }
	}
}
