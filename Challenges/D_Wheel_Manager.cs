/* Creator: Collin McQuade
 * Date Created: 11/25/18
 * Function: Controls the Payload prefab's functionality
 * 
 * References:
 * 
 * Notes: Will need to get instantiated before setting up in the map, otherwise 
 * it will start moving on Awake or it will Destroy itself before ever reaching the player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Wheel_Manager : MonoBehaviour
{
    private int wheelSpeed;
    private int planeSpeed;
    private int way;

    private float playDis;
    private float intStr = 150;

    private Ray floorDet;
    private RaycastHit floorHit;

    private Vector3 floorDir;
    public Vector3 ForceDir;

    private Transform playPos;
    public Transform wheelPos;

	
	void Start ()
    {
        playPos = GameObject.FindGameObjectWithTag("Player").transform;

        Rigidbody myRigidbody = gameObject.GetComponent<Rigidbody>();
        myRigidbody.AddForce(ForceDir * intStr);

        way = Random.Range(0, 3);
	}
	
	
	void Update ()
    {
        PlayerDisCheck();
        DirSwitch();
        BounceCheck();
	}

    //The bounce function
    public void WheelBounce()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(planeSpeed, 3000, wheelSpeed));
    }

    //Uses ray to check if it is close enought to the ground to bounce again.
    void BounceCheck()
    {
        Ray floorDet = new Ray(transform.position.normalized, floorDir);
        {
            Vector3 down = transform.TransformDirection(floorDir);
            if(Physics.Raycast(transform.position, Vector3.down, out floorHit, 1.5f))
            {
                if(floorHit.collider.tag == "Ground")
                {
                    WheelBounce();
                }
            }
        }
    }

    //Function that destroys the game object after some distance away from the player.
    void PlayerDisCheck()
    {
        playDis = Vector3.Distance(playPos.transform.position, wheelPos.transform.position);
        if (playDis >= 75)
        {
            Destroy(gameObject);
        }
    }

    void DirSwitch()
    {
        switch (way)
        {
            case 2:
                {
                    planeSpeed = 800;
                    wheelSpeed = 50;
                    break;
                }
            case 1:
                {
                    planeSpeed = -800;
                    wheelSpeed = -50;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
