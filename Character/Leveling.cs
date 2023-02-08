using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leveling : MonoBehaviour {

    [Tooltip("What level we currently are.")]
    [SerializeField] int level;

    [Tooltip("Max HP value.")]
    [SerializeField] int myHP;

    [Tooltip("Strength value. Affects attack strength.")]
    [SerializeField] int myStr;

    [Tooltip("How much XP we currently have.")]
    [SerializeField] int myXP;

    [Tooltip("How much XP we need for next level.")]
    [SerializeField] int xpNeeded;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //To be called when calculating XP needed for next level.
    void CalculateExperienceNeeded()
    {
        //Placeholder formula.
        xpNeeded = (level * 20) - 14;
    }

    //To be called when experience is gained.
    //Requires an int value of how much experience was gained.
    void GainExperience(int xpGained)
    {
        myXP += xpGained;
        if(myXP >= xpNeeded)
        {
            level++;
            CalculateExperienceNeeded();
        }
    }

    //To be called when level goes up.
    void LevelUp()
    {
        //Placeholder HP formula.
        myHP = level * 3;

        //Placeholder Str formula.
        myStr = level + 1 - 1;
    }
}
