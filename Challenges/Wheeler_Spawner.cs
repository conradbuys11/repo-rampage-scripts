using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheeler_Spawner : MonoBehaviour
{
    public D_Wheel_Manager Direction;

    public Transform spawnPos;
    private Transform playerPos;
    private Transform myPos;

    private float playerDis;
    public float timer;
    private float mxTimer;

    public GameObject Wheeler;
	
	void Start ()
    {
        mxTimer = timer;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        myPos = gameObject.transform;
	}
	
	
	void Update ()
    {
        playerDis = Vector3.Distance(playerPos.transform.position, myPos.transform.position);
        if(playerDis <= 45)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                SpawnShooter();
            }
        }
	}

    void SpawnShooter()
    {
        Instantiate(Wheeler, spawnPos.transform.position, spawnPos.transform.rotation);
        timer = mxTimer;
    }
}
