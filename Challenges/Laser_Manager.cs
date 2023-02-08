using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Manager : MonoBehaviour
{
    public GameObject lazzer;
    public GameObject pylonA;
    public GameObject pylonB;
    public GameObject control;

    public bool activate;
    private bool change;

    public float lazSped;

	void Start ()
    {

	}
	
	void Update ()
    {
        ActiveLaser();
        if (change)
        {
            activate = false;
        }
        else
        {
            activate = true;
        }

        if(control == null)
        {
            Invoke("SelfDestruct", 3);
        }
	}

    void ActiveLaser()
    {
        if (activate)
        {
            lazzer.transform.position = Vector3.MoveTowards(lazzer.transform.position, pylonA.transform.position, lazSped * Time.deltaTime);
            if(lazzer.transform.position == pylonA.transform.position)
            {
                change = true;
            }
        }
        if (!activate)
        {
            lazzer.transform.position = Vector3.MoveTowards(lazzer.transform.position, pylonB.transform.position, lazSped * Time.deltaTime);
            if (lazzer.transform.position == pylonB.transform.position)
            {
                change = false;
            }
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
