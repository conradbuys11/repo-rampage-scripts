using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruiserHurtbox : MonoBehaviour
{
    private Animator BruiserAnim;
    private GameObject Bruiser;

	// Use this for initialization
	void Start ()
    {
        Bruiser = FindObjectOfType<Enemy_Bruiser>().gameObject;
        BruiserAnim = Bruiser.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TookDamage();
        }
    }

    void TookDamage()
    {
        BruiserAnim.SetTrigger("Ouchy");
    }
}
