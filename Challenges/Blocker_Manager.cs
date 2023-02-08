using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker_Manager : MonoBehaviour
{
    public Animator blockAnim;

    private Transform flamePos;

    public GameObject Fire;

	void Start ()
    {
        flamePos = this.gameObject.transform.GetChild(1).transform;
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            blockAnim.SetTrigger("IsBlocking");
            StartCoroutine(FlameDelay());
        }
    }

    IEnumerator FlameDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(Fire, flamePos.position, flamePos.rotation);
    }
}
