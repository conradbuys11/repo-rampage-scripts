using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    int spawnChance;
    int spawnAmount;
    int playerHealth;
    public GameObject healCube;

    
    public int assignedDrop;



    private void OnDestroy()
    {
        if(assignedDrop != 0)
        {
            Instantiate(RepoDropMaster.repoItems[assignedDrop - 1], transform.position, transform.rotation);
        }
        else
        {
            Instantiate(RepoDropMaster.repoItems[Random.Range(0, RepoDropMaster.repoItems.Length)], transform.position, transform.rotation);
        }
        
        playerHealth = FindObjectOfType<PlayerStatus>().health;

        if(playerHealth <= 15)
        {
            spawnAmount = Random.Range(10, 13);
            for (int i = 0; i < spawnAmount; i++)
            {
                Instantiate(healCube, transform.position, transform.rotation);
            }
        }
        else if (playerHealth < 29)
        {
            spawnChance = Random.Range(1, 101);
            if(spawnChance < 80)
            {
                spawnAmount = Random.Range(18, 25);
                for(int i = 0; i < spawnAmount; i++)
                {
                    Instantiate(healCube, transform.position, transform.rotation);
                }
            }
        }
        else if(playerHealth < 49)
        {
            spawnChance = Random.Range(1, 101);
            if (spawnChance < 30)
            {
                spawnAmount = Random.Range(10, 12);
                for (int i = 0; i < spawnAmount; i++)
                {
                    Instantiate(healCube, transform.position, transform.rotation);
                }
            }
        }
    }
}
