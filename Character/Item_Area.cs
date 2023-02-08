using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Area : MonoBehaviour
{
    //public GameObject[] items;
    public Vector3 handRef;

	void Start ()
    {
        //handRef = gameObject.transform.position;
	}
	
	void LateUpdate ()
    {
        handRef = gameObject.transform.position;
    }
}
