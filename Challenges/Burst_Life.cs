using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst_Life : MonoBehaviour
{


	void Start ()
    {
		
	}
	

	void Update ()
    {
        StartCoroutine(BurstLife());
	}

    IEnumerator BurstLife()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
