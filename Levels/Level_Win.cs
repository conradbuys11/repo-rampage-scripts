using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Win : MonoBehaviour
{
    public Level_Manager TimeToGo;
	
	void Start ()
    {
        TimeToGo = FindObjectOfType<Level_Manager>();
	}

	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TimeToGo.LoadNextScene();
        }

    }
}
