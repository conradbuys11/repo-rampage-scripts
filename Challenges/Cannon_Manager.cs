using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Manager : MonoBehaviour
{
    public GameObject artillery;

    public Ray threatDet;
    public RaycastHit threatAttk;

    private int shotsFire;
    private int clip;

    private float timer = 6.0f;
    private float maxTimer;
    private float delay;

    private Transform flashPoint;

    public bool AlwaysOn;

    void Start()
    {
        maxTimer = timer;
        shotsFire = 2;
        flashPoint = this.gameObject.transform.GetChild(2).GetChild(0).transform;
        delay = 1.0f;
    }


    void Update()
    {
        if (AlwaysOn)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                //BurstFire();
                FiringLine();
                timer = maxTimer;
            }
        }
    }

    public void BurstFire()
    {
        //for (int clip = 0; clip <= shotsFire; clip++)
        //{
        //    InvokeRepeating("FiringLine", 1f, 2f);
        //}

    }

    void FiringLine()
    {
        Instantiate(artillery, flashPoint.position, flashPoint.rotation);
    }
}
