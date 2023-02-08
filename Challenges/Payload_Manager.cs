/* Creator: Collin McQuade
 * Date Created: 11/25/18
 * Function: Controls the Payload prefab's functionality
 * 
 * References:
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload_Manager : MonoBehaviour
{
    private Transform playPos;
    public Transform payloadPos;
    public Transform goalPos;

    private Ray playScan;
    private RaycastHit playHit;

    private float driveDis;
    private float payloadSpeed;

    public bool station;

	void Start ()
    {
        //payloadPos = GameObject.FindObjectOfType<Payload_Manager>().transform;
        playPos = GameObject.FindGameObjectWithTag("Player").transform;
        
        payloadSpeed = 5.0f;
	}
	

	void Update ()
    {
        driveDis = Vector3.Distance(playPos.transform.position, payloadPos.transform.position);

        if(driveDis <= 12)
        {
            payloadPos.transform.position = Vector3.MoveTowards(transform.position, goalPos.transform.position, payloadSpeed * Time.deltaTime);
            if(payloadPos.transform.position == goalPos.transform.position)
            {
                station = true;
            }
        }
    }
}
