using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool LevelPickUp;

    float acceleration = .2f;
    float maxSpeed = 20;
    float pauseTime = 1;

    float speed;
    
    GameObject playerRef;
    PlayerStatus playerStatRef;

    Vector3 pushVector;
    RepoDropMaster repoRef;
    

    void Start()
    {
        playerRef = FindObjectOfType<PlayerPushbox>().gameObject;
        playerStatRef = FindObjectOfType<PlayerStatus>();
        Rotate rotate = gameObject.AddComponent<Rotate>();
        repoRef = FindObjectOfType<RepoDropMaster>();
        rotate.rotation = new Vector3(0, 45, 0);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, playerRef.transform.position) <= 12)
        {
            speed += acceleration;
            Mathf.Clamp(speed, 0, maxSpeed);
            transform.position = Vector3.MoveTowards(transform.position, playerRef.transform.position + new Vector3(0, 1.5f, 0), speed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (LevelPickUp)
            {
                repoRef.LevelRepoUI();
            }
            else
            {
                repoRef.ExtraRepoUI();
            }

            Destroy(gameObject);
        }
    }
}
