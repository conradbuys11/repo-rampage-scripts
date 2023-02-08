using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy12 : MonoBehaviour
{
    private Animator EnemyAnim;
    private GameObject DamageBox;

    private void Awake()
    {
        DamageBox = GameObject.Find("AttackArea").gameObject;
        EnemyAnim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start ()
    {
        DamageBox.SetActive(false);

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (EnemyAnim.GetBool("ActivateAttack") == false && Input.GetKeyDown(KeyCode.Z))
        {
            EnemyAnim.SetBool("ActivateAttack", true);
        }
    }

    public void Attack12()
    {
        DamageBox.SetActive(true);
    }

    public void AttackFinsih()
    {
        DamageBox.SetActive(false);
        EnemyAnim.SetBool("ActivateAttack", false);
    }
}
