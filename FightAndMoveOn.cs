using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAndMoveOn : MonoBehaviour
{
    public GameObject fightImage;
    public GameObject moveOnImage;

    public bool inFight;

    float fightTimer;
    float goTimer;
	
	// Update is called once per frame
	void Update ()
    {
        if (inFight)
        {
            Fight();
        }
        else
        {
            Go();
        }
	}

    void Fight()
    {
        fightTimer += Time.deltaTime;
        moveOnImage.SetActive(false);
        if(fightTimer >= 1)
        {
            fightImage.SetActive(!fightImage.activeInHierarchy);
            fightTimer = 0;
        }

    }

    void Go()
    {
        goTimer += Time.deltaTime;
        fightImage.SetActive(false);
        if (goTimer >= 1)
        {
            moveOnImage.SetActive(!moveOnImage.activeInHierarchy);
            goTimer = 0;
        }
    }
}
