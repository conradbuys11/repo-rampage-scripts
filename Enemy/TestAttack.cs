using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{

    Animator myAnime;
    int comboCount;
    bool isAttacking;

	// Use this for initialization
	void Start ()
    {
        myAnime = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            if(comboCount >= 3)
            {
                myAnime.SetTrigger("Upper");
                isAttacking = true;
                comboCount = 0;
            }
            else
            {
                myAnime.SetTrigger("Weak");
                isAttacking = true;
                comboCount++;
            }
        }
	}

    public void CanAttack()
    {
        isAttacking = !isAttacking;
    }
}
