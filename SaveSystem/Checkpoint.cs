using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject CheckpointPanel;
    private GameObject Cam;
    
    public Vector3 playerSpawnLocation; //where our player will spawn

    public bool newSpawnpos;

    PlayerStatus deadCheck;

    
    private void Awake()
    {
        CheckpointPanel = GameObject.Find("NadaButton");
    }


    void Start ()
    {
        playerSpawnLocation = FindObjectOfType<PlayerPushbox>().transform.position;
        deadCheck = FindObjectOfType<PlayerStatus>();
        CheckpointPanel.SetActive(false);
        Cam = GameObject.Find("Player Camera").gameObject;
    }
	

	void Update ()
    {

    }

    private void RemoveCheckpointPannel()
    {
        if (CheckpointPanel.activeInHierarchy)
        {
            StartCoroutine(PannelReset());
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CheckpointPanel.SetActive(true);
            Cam.GetComponent<AudioDirector>().Env32();
            RemoveCheckpointPannel();
            playerSpawnLocation = other.transform.position;
            deadCheck.checkPos = this;
            FindObjectOfType<CheckLoad>().LocalSet();

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void Respawning()
    {
            deadCheck.livesCount -= 1;
            deadCheck.Die.SetTrigger("Is_KnockedBack");
            deadCheck.StartCoroutine(deadCheck.DeathDelay());       
    }

    public IEnumerator PannelReset() 
    {
        yield return new WaitForSeconds(1);
        CheckpointPanel.SetActive(false);
    }
}
