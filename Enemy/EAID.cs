using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EAID : MonoBehaviour
{

    bool testBool;
    [Header("How many waves?")]
    [Tooltip("Amount of waves spawned once the player clears all present enemie")]
    public int waveCount;

    [Header("What is spawning")]
    [Tooltip("Array size determens enemy count spawned, Array element is what's spawned")]
    public GameObject[] enemiesForTheFight;

    [HideInInspector] public GameObject[] spawnedEnemies;
    GameObject[] attackingEnemies;

    [Header("How many enemies attack")]
    [Tooltip("Number of enemies that will attack the player once spawned.")]
    public int iEnemiesAttacking;
    
    
    
    
    //Remove all this junk
    [Header("Dont Change")]
    public Transform[] spawnPoints;
    public Transform[] frontIdle;
    public Transform[] rearIdle;

    List<GameObject> attackingEnemiesList = new List<GameObject>();
    List<GameObject> idelEnemiesList = new List<GameObject>();

    int iEnemiesCurAttacking;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<PlayerPushbox>())
        {
            SpawnEnemies();

            GetComponent<BoxCollider>().enabled = false;
        }
    }

    void Awake()
    {
        spawnedEnemies = new GameObject[enemiesForTheFight.Length];
    }
    

    public void DoAttack()
    {
        for (int i = 0; i < spawnedEnemies.Length; i++)
        {
            if(i == 1 && spawnedEnemies[1].GetComponent<Enemy_Thug>())
            {
                attackingEnemiesList.Add(spawnedEnemies[1]);
                attackingEnemiesList[1].GetComponent<Enemy_Thug>().rangedAttacker = true;
            }
            else 
            {
                attackingEnemiesList.Add(spawnedEnemies[i]);
                attackingEnemiesList[i].GetComponent<IEnemy>().BeginAttack(true);
            }
        }
    }

    public void AttackRequest(GameObject enemy)
    {
        if (idelEnemiesList.Contains(enemy))
        {
            idelEnemiesList.Remove(enemy);
        }
        else if(idelEnemiesList.Count > 0)
        {
            attackingEnemiesList.Remove(enemy);
            idelEnemiesList[0].GetComponent<IEnemy>().BeginAttack(true);
            attackingEnemiesList.Add(idelEnemiesList[0]);
            idelEnemiesList.Remove(idelEnemiesList[0]);
        }
        else
        {
            attackingEnemiesList.Remove(enemy);
        }
        if(waveCount > 0 && attackingEnemiesList.Count == 0 && idelEnemiesList.Count == 0)
        {
            SpawnEnemies();
        }
        else if(attackingEnemiesList.Count <= 0)
        {
            gameObject.GetComponentInParent<FightArea>().Battle_Barrier.SetActive(false);
            gameObject.GetComponentInParent<FightArea>().In_Battle = false;
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesForTheFight.Length; i++)
        {
            int spawnAt = Random.Range(0, spawnPoints.Length);
            spawnedEnemies[i] = Instantiate(enemiesForTheFight[i], spawnPoints[spawnAt]);
            spawnedEnemies[i].GetComponent<IEnemy>().SetDirector(this, null);
            spawnedEnemies[i].transform.parent = null;
            spawnedEnemies[i].transform.position += new Vector3(i * 3, 0, 0);
        }
        DoAttack();
        waveCount--;
    }

}
