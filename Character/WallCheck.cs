using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    CameraFollow cameraRef;
    PlayerPushbox playerRef;
    static RaycastHit hit;
    // Use this for initialization
    void Start ()
    {
        cameraRef = FindObjectOfType<CameraFollow>();
        playerRef = FindObjectOfType<PlayerPushbox>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(playerRef.transform.eulerAngles.y < 120)
        {
            if (Physics.Raycast(transform.position + new Vector3(0, 2.5f, 0), Vector3.right, out hit, 10f))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("BoundaryPushbox"))
                {
                    cameraRef.obstructed = true;
                }
            }
            else if (cameraRef.obstructed)
            {
                cameraRef.obstructed = false;
            }
        }
        else
        {
            if (Physics.Raycast(transform.position + new Vector3(0, 2.5f, 0), Vector3.left, out hit, 10f))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("BoundaryPushbox"))
                {
                    cameraRef.obstructed = true;
                }
            }
            else if (cameraRef.obstructed)
            {
                cameraRef.obstructed = false;
            }
        }
        
	}

    public static Vector3 Obstruction()
    {
        Vector3 obstructionPoint = hit.point;
        return obstructionPoint;
    }
}
