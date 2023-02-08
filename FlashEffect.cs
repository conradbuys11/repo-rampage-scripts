using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    private float LifeTime;
    List<Collider> myCollisions;

	// Use this for initialization
	void Start ()
    {
        myCollisions = new List<Collider>();
        LifeTime = 0.2f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        LifeTime = LifeTime - Time.deltaTime;

        if (LifeTime <= 0)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyBase>())
        {
            foreach(Collider collider in myCollisions)
            {
                if(other == collider) { return; }
            }
            if (other.gameObject.GetComponent<EnemyBase>().IsCurrentState<CharacterStunnedState>() == false && other.gameObject.GetComponent<EnemyBase>().IsNextState<CharacterStunnedState>() == false)
            {
                other.gameObject.GetComponent<EnemyBase>().TakeStun(3);
                
            }
        }
    }
}
