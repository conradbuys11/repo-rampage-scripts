using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_KungFu_SmokeBomb : MonoBehaviour
{
    public GameObject Smoke;
    private Transform smokePos;


    // Use this for initialization
    void Start ()
    {
        smokePos = this.gameObject.transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.activeInHierarchy)
        {
         Instantiate(Smoke, smokePos.position, smokePos.rotation);
        }
		
	}
}
