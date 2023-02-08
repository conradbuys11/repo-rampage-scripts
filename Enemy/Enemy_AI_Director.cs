using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_Director : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    GameObject[] enemiesSpawned;
    [SerializeField] Transform[] spawnPoints;

    GameObject spawnedThing;
    public int enemyCountAttacking;

    private void Start()
    {
        enemiesSpawned = new GameObject[enemiesToSpawn.Length];
    }

    public void KillAllEnemies()
    {
        foreach (GameObject enemies in enemiesSpawned)
        {
            enemies.SendMessage("TakeDamage", enemies.GetComponent<EnemyBase>().myCurHP);
        }
    }

    public void RemoveTheDead(EnemyBase removeMe)
    {
        Destroy(removeMe.gameObject);
        AttackPlan();
    }

    public IEnumerator HoldUp()
    {
        yield return new WaitForSeconds(4);
        AttackPlan();
    }

    void AttackPlan()
    {
        int enemyCurAttacking = 1;
        for(int i = 0; i < enemiesToSpawn.Length; i++)
        {
            if(enemiesSpawned[i] != null)
            {
                if (enemiesSpawned[i].GetComponent<EnemyBase>().IsCurrentState<EnemyIdleState>() && !enemiesSpawned[i].GetComponent<EnemyBase>().IsNextState<EnemyPursueState>() && enemyCurAttacking <= enemyCountAttacking)
                {
                    enemiesSpawned[i].GetComponent<EnemyBase>().ChangeState<EnemyPursueState>();
                    enemyCurAttacking++;
                }
            }
        }
    }

    void SpawnEnemies()
    {
        //Instantiate(meleeEnemies, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.Euler(0,0,0), transform).GetComponent<Enemy_Melee>();
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            if(enemiesToSpawn[i] != null)
            {
                enemiesSpawned[i] = Instantiate(enemiesToSpawn[i], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.Euler(0, 0, 0), transform);
            }
        }
        Invoke("AttackPlan", 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponentInParent<PlayerPushbox>())
        {
            GetComponent<BoxCollider>().enabled = false;
            SpawnEnemies();
        }
    }
}
