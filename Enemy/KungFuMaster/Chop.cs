using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OntrigggerEnter(Collider other)
    {
        Vector3 direction = new Vector3(0, 0, 0);

        if (other.GetComponentInParent<PlayerPushbox>())
        {
           // Debug.Log("onTrigg");

            if (other.transform.position.x >= transform.position.x)
            {
                direction = new Vector3(-100, 0, 0);
            }
            else
            {
                direction = new Vector3(100, 0, 0);
            }
           // Debug.Log("take Damage");

            other.GetComponentInParent<PlayerPushbox>().TakeDamage(1f);
            other.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
 
}
