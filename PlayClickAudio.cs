using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClickAudio : MonoBehaviour
{
    public GameObject Cam;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.S) || 
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            Cam.GetComponent<AudioDirector>().Env22();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Cam.GetComponent<AudioDirector>().Env24();
        }
	}
}
