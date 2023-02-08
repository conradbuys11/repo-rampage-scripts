using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckLoad : MonoBehaviour
{
    int activeScene;
    public GameObject locationNode;
    Checkpoint loadThis;

    void Start ()
    {
        loadThis = FindObjectOfType<Checkpoint>();
    }
	
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void LocalSet()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        
        PlayerPrefs.SetInt("CurrentScene", activeScene);
        PlayerPrefs.SetFloat("NodeX", FindObjectOfType<PlayerPushbox>().transform.position.x);
        PlayerPrefs.SetFloat("NodeY", FindObjectOfType<PlayerPushbox>().transform.position.y);
        PlayerPrefs.SetFloat("NodeZ", FindObjectOfType<PlayerPushbox>().transform.position.z);
        loadThis.newSpawnpos = false;
        //print(activeScene);
    }

    //public void LastCheckandLevel()
    //{
    //    SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentScene"));
    //}

    public void ResetCheckpoint()
    {
        PlayerPrefs.SetFloat("NodeX", 0);
        PlayerPrefs.SetFloat("NodeY", 0);
        PlayerPrefs.SetFloat("NodeZ", 0);
    }
}
