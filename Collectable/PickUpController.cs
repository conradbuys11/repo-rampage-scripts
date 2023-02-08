using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpController : MonoBehaviour
{
    static PickUp[] levelPickUps;
    static int pickedUp;
    static Text collectables;
	// Use this for initialization
	void Start ()
    {
        //collectables = GameObject.Find("Collectable Text").GetComponent<Text>();
        levelPickUps = FindObjectsOfType<PickUp>();
        //collectables.text = pickedUp + "/" + levelPickUps.Length;
        print(pickedUp + "/" + levelPickUps.Length);
        if(collectables == null)
        {
            throw new System.Exception("PICK UPS UI!!!!!!!");
        }
    }

    public static void GotOne(PickUp pickUp)
    {
        for(int i = 0; i < levelPickUps.Length; i++)
        {
            if(levelPickUps[i] != null)
            {
                if (pickUp == levelPickUps[i])
                {
                    pickedUp++;
                    //collectables.text = pickedUp + "/" + levelPickUps.Length;
                    print(pickedUp + "/" + levelPickUps.Length);
                    if (pickedUp == levelPickUps.Length)
                    {
                        print("Collected all");
                        //collectables.text = "Complete";
                    }
                    levelPickUps[i] = null;
                    return;
                }
            }
        }
        

    }
}
