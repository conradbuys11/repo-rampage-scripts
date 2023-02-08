using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSuperParent : MonoBehaviour
{

    PlayerPushbox myPlayer;
    Rigidbody myRB;
    public Vector3 debug;
    RaycastHit rayHit;

    private Transform vecPoint;

    private void Awake()
    {
        myPlayer = GetComponentInChildren<PlayerPushbox>();
        myRB = GetComponent<Rigidbody>();
        vecPoint = this.gameObject.transform.GetChild(0).GetChild(4).transform;
    }

    void Start ()
    {
		
	}

    void Update()
    {
        debug = myRB.velocity;
        //Debug.DrawRay(vecPoint.position, Vector3.down, Color.blue, .05f);
        if (myPlayer.AreWeFalling())
        {
            if (Physics.Raycast(vecPoint.position, Vector3.down, out rayHit, 0.05f) && rayHit.collider.gameObject.layer == LayerMask.NameToLayer("BoundaryPushbox"))
            {
                myPlayer.Land();
            }
        }
    }

    public bool CheckBelow()
    {
        //Debug.DrawRay(vecPoint.position, Vector3.down, Color.blue, .05f);
        if (Physics.Raycast(vecPoint.position, Vector3.down, out rayHit, 0.05f) && rayHit.collider.gameObject.layer == LayerMask.NameToLayer("BoundaryPushbox"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
