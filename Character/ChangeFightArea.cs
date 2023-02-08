using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFightArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy_AI_Director>())
        {
            other.SendMessage("AllowCreation");
        }
    }
}
