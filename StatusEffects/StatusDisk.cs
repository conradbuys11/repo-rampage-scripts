using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDisk : MonoBehaviour {

    Renderer myRenderer;
    public Material noStatus;
    public Material slowed;
    public Material dotted;

	// Use this for initialization
	void Start () {
        myRenderer = GetComponent<Renderer>();
        myRenderer.material = noStatus;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Slowed()
    {
        myRenderer.material = slowed;
    }

    public void Dotted()
    {
        myRenderer.material = dotted;
    }

    public void EndStatus()
    {
        myRenderer.material = noStatus;
    }
}
