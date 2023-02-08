using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    [SerializeField]
    float acceleration;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float pauseTime;

    float speed;

    float timer;

    GameObject playerRef;
    PlayerStatus playerStatRef;

    Vector3 pushVector;
    
    void Start ()
    {
        playerRef = FindObjectOfType<PlayerPushbox>().gameObject;
        playerStatRef = FindObjectOfType<PlayerStatus>();
        pushVector = new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11)) * 10;
        GetComponent<Rigidbody>().AddForce(pushVector);
        GetComponent<BoxCollider>().enabled = false;
	}
	
	void FixedUpdate ()
    {
        timer += Time.deltaTime;
        if(timer > pauseTime)
        {
            speed += acceleration;
            Mathf.Clamp(speed, 0, maxSpeed);
            transform.position = Vector3.MoveTowards(transform.position, playerRef.transform.position + new Vector3(0, 1.5f, 0), speed * Time.deltaTime);
            GetComponent<BoxCollider>().enabled = true;
            
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStatRef.health += 1;
            Destroy(gameObject);
        }
    }
}
