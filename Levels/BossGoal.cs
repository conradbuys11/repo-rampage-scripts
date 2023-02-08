using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGoal : MonoBehaviour
{
    Level_Manager bossToNext;
    public int BossHp;

	void Start ()
    {
        bossToNext = GameObject.FindObjectOfType<Level_Manager>();
	}
	

	void Update ()
    {
        BossHPCheck();

        if (Input.GetKeyDown(KeyCode.P))
        {
            BossHp = BossHp - 1;
        }
	}

    public void BossHPCheck()
    {
        if(bossToNext.BossLevel == true && BossHp <= 0)
        {
            bossToNext.Level_Goal = true;
        }
    }
}
