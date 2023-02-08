using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCoodownTimers : MonoBehaviour
{
    PlayerPushbox playerPushboxRef;

    Image abilityOneButton;
    Image abilityTwoButton;
    Image abilityThreeButton;

    Text abilityOne;
    Text abilityTwo;
    Text abilityThree;

    Text abilityOneName;
    Text abilityTwoName;
    Text abilityThreeName;

    Color abilityReady;
    Color abilityDown;

	// Use this for initialization
	void Start ()
    {
        playerPushboxRef = FindObjectOfType<PlayerPushbox>();
        abilityOne = GameObject.Find("Ability 1").transform.GetChild(0).GetComponent<Text>();
        abilityTwo = GameObject.Find("Ability 2").transform.GetChild(0).GetComponent<Text>();
        abilityThree = GameObject.Find("Ability 3").transform.GetChild(0).GetComponent<Text>();
        abilityOneName = GameObject.Find("Ability 1").transform.GetChild(1).GetComponent<Text>();
        abilityTwoName = GameObject.Find("Ability 2").transform.GetChild(1).GetComponent<Text>();
        abilityThreeName = GameObject.Find("Ability 3").transform.GetChild(1).GetComponent<Text>();
        abilityOneButton = GameObject.Find("Ability 1").GetComponent<Image>();
        abilityTwoButton = GameObject.Find("Ability 2").GetComponent<Image>();
        abilityThreeButton = GameObject.Find("Ability 3").GetComponent<Image>();
        abilityReady = Color.green;
        abilityDown = Color.red;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float ability1Cooldown = playerPushboxRef.ability1.GetCurrentCooldown();
        float ability2Cooldown = playerPushboxRef.ability2.GetCurrentCooldown();
        float ability3Cooldown = playerPushboxRef.ability3.GetCurrentCooldown();

        string name1 = playerPushboxRef.ability1.GetUIName();
        string name2 = playerPushboxRef.ability2.GetUIName();
        string name3 = playerPushboxRef.ability3.GetUIName();

        if(name1 != abilityOneName.text)
        {
            abilityOneName.text = name1;
        }
        if(name2 != abilityTwoName.text)
        {
            abilityTwoName.text = name2;
        }
        if(name3 != abilityThreeName.text)
        {
            abilityThreeName.text = name3;
        }

		if(ability1Cooldown > 0)
        {
            abilityOne.text = Mathf.Round(ability1Cooldown).ToString();
            abilityOne.fontSize = 30;
            abilityOneButton.color = abilityDown;
        }
        else
        {
            abilityOneButton.color = abilityReady;
            abilityOne.fontSize = 14;
            abilityOne.text = "Ability 1\n'I'";
        }

        if (ability2Cooldown > 0)
        {
            abilityTwo.text = Mathf.Round(ability2Cooldown).ToString();
            abilityTwo.fontSize = 30;
            abilityTwoButton.color = abilityDown;
        }
        else
        {
            abilityTwoButton.color = abilityReady;
            abilityTwo.fontSize = 14;
            abilityTwo.text = "Ability 2\n'O'";
        }

        if (ability3Cooldown > 0)
        {
            abilityThree.text = Mathf.Round(ability3Cooldown).ToString();
            abilityThree.fontSize = 30;
            abilityThreeButton.color = abilityDown;
        }
        else
        {
            abilityThreeButton.color = abilityReady;
            abilityThree.fontSize = 14;
            abilityThree.text = "Ability 3\n'P'";
        }
    }
}
