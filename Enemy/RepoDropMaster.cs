using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepoDropMaster : MonoBehaviour
{
    public static Object[] repoItems;
    static GameObject[] levelRepoItems;
    PickUp[] levelSpecificRepo;
    Text LevelRepo;
    int levelRepoCollected;
    Text extraRepo;
    int extraRepoCollected;

	// Use this for initialization
	void Start ()
    {
        levelSpecificRepo = FindObjectsOfType<PickUp>();
        levelRepoItems = new GameObject[levelSpecificRepo.Length];
        for(int i = 0; i < levelSpecificRepo.Length; i++)
        {
            levelRepoItems[i] = levelSpecificRepo[i].gameObject;
        }
        
        repoItems = Resources.LoadAll("_Repo Items");
        LevelRepo = GameObject.Find("Level Repo").GetComponent<Text>();
        extraRepo = GameObject.Find("Extra Repo").GetComponent<Text>();
        LevelRepo.text = "0/" + levelRepoItems.Length;
        extraRepo.text = "0";
	}
	
    public void LevelRepoUI()
    {
        levelRepoCollected++;
        LevelRepo.text = levelRepoCollected.ToString() + "/" + levelRepoItems.Length.ToString();
        if(levelRepoCollected == levelRepoItems.Length)
        {
            LevelRepo.text = "Completed!";
        }
    }


    public void ExtraRepoUI()
    {
        extraRepoCollected++;
        extraRepo.text = extraRepoCollected.ToString();
    }
}
