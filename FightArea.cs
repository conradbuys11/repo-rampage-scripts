using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightArea : MonoBehaviour
{
    private GameObject walls;
    public GameObject Battle_Barrier;
    public GameObject Fight_Zone;
    public GameObject myFight;

    PlayerStatus resetCheck;
    EAID ttlEnem; 
    CameraFollow battleCam;
    Barrier bounds;

    public int TotalEnemy;
 
    public bool In_Battle;
    public bool lossSight;
    public bool enemReset;

    private Vector3 thiswayPos;

    void Start()
    {
        resetCheck = FindObjectOfType<PlayerStatus>();
        battleCam = FindObjectOfType<CameraFollow>();
        bounds = FindObjectOfType<Barrier>();
        In_Battle = false;
        lossSight = false;
    }

    IEnumerator FightAreaDestroy()
    {
        yield return new WaitForSeconds(1f);
        lossSight = false;
        myFight.SetActive(false);
    }

    void Update()
    {
        if (TotalEnemy <= 0)
        {
            if (FindObjectOfType<FightAndMoveOn>())
            {
                FindObjectOfType<FightAndMoveOn>().inFight = false;
            }
            In_Battle = false;
            Battle_Barrier.SetActive(false);
        }
      
        if(lossSight && resetCheck.loss)
        {
            resetCheck.loss = false;
            enemReset = true;
            FightResetting();
            TotalEnemy = 0;
            Battle_Barrier.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerPushbox>())
        {
            ttlEnem = GetComponentInChildren<EAID>();
            TotalEnemy = ttlEnem.enemiesForTheFight.Length;
            In_Battle = true;
            lossSight = true;
            Battle_Barrier.SetActive(true);
            

            //FindObjectOfType<FightAndMoveOn>().inFight = true;
            ttlEnem.GetComponent<BoxCollider>().enabled = false;
            myFight.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void FightEvent()
    {
        if (TotalEnemy <= 0)
        {
            FindObjectOfType<FightAndMoveOn>().inFight = false;
            In_Battle = false;
            Battle_Barrier.SetActive(false);
        }
    }

    public void FightResetting()
    {
        if (enemReset)
        {
            TotalEnemy = ttlEnem.enemiesForTheFight.Length;
            myFight.GetComponent<BoxCollider>().enabled = true;
            ttlEnem.GetComponent<BoxCollider>().enabled = true;
            In_Battle = false;
            FindObjectOfType<FightAndMoveOn>().inFight = false;
            enemReset = false;
        }
    }
}
