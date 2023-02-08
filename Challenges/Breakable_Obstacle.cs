using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_Obstacle : MonoBehaviour
{
    public int Obstacle_Health;
    public int itemBreakPoints;

    public GameObject burst;

	void Start ()
    {
		
	}


    void Update()
    {
        if (Obstacle_Health <= 0)
        {
            Instantiate(burst, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Obstacle_Health = Obstacle_Health - 1;
        }
    }
}
