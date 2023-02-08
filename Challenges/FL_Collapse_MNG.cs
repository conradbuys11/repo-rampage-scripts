using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FL_Collapse_MNG : MonoBehaviour
{
    private Animator anim;

    public GameObject weAllFall;
    public GameObject shatter;

	void Start ()
    {
        //GetComponentInChildren<Animator>();
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            anim.SetTrigger("FL_Fall");
            Invoke("DestroyUs", 3);
        }
    }

    void DestroyUs()
    {
        Instantiate(shatter, transform.position, transform.rotation);
        Destroy(weAllFall);
    }
}
