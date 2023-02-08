using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    
    PlayerStatus playerRef;
	// Use this for initialization
	void Start ()
    {
        playerRef = FindObjectOfType<PlayerStatus>();
    }

    public void BuyItem(string thingIWant)
    {
        playerRef.SendMessage(thingIWant);
    }
}
