/* Creator: Collin McQuade
 * Date Created: 11/25/18
 * Function: Controls the Sniper prefab's functionality
 * 
 * References:
 * CG Cookie - Unity Training Animate a Line, Retrieved from:
 * https://www.youtube.com/watch?v=Bqcu94VuVOI
 * Blackthornprod Shooting/Following/Retreating, Retrieved from:
 * https://www.youtube.com/watch?v=_Z1t7MNk0c4
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper_Manager : MonoBehaviour
{
    private LineRenderer snipeLaser;
    private Ray playerScan;
    private RaycastHit playHit;

    private float decrement;
    private float playerDis;
    private float shotTime;
    public float startShotTime;
    public float sightInSpeed = 12;

    public Transform sniperPos;
    public Transform playerPos;
    private Transform thePlayer;

    private Vector3 blastPos;

    public GameObject SniperRound;
    

    public Material mat1, mat2;

	void Start ()
    {
        blastPos = new Vector3(1, 1, 1);
        snipeLaser = GetComponent<LineRenderer>();

        playerDis = Vector3.Distance(sniperPos.position, playerPos.position);

        thePlayer = GameObject.FindGameObjectWithTag("Player").transform;
        //attackRange = Vector3.Distance(thePlayer.transform.position, sniperPos.transform.position);
    }
	

	void Update ()
    {
        //I think the moving hitbox bug has something to do with this...
        Vector3 sPos = sniperPos.position;
        Vector3 pPos = playerPos.position;
        Vector3 v = pPos - sPos;

        playerScan = new Ray(sPos, v);

       if (Physics.Raycast(playerScan, out playHit, 50))
       {
           if (playHit.collider.transform.tag == "Player")
           {
                snipeLaser.enabled = true;
                snipeLaser.SetPosition(0, sniperPos.position);
                snipeLaser.startWidth = 0.30f;
                snipeLaser.endWidth = 0.30f;
                LockPlayer();
            }
        }
        else
        {
            snipeLaser.enabled = false;
        }
      
    }

    public void LockPlayer()
    {
        Vector3 sPos = sniperPos.position;
        Vector3 pPos = playerPos.position;

        if (decrement < playerDis)
        {
            decrement += .1f / sightInSpeed;
            float Q = Mathf.Lerp(0, playerDis, decrement);
            Vector3 tick = Q * Vector3.Normalize(pPos - sPos) + sPos;
            snipeLaser.SetPosition(1, tick);
        }
        if(shotTime <= 0)
        {
            shotTime = startShotTime;
            StartCoroutine(FireDelay());
        }
        else
        {
            shotTime -= Time.deltaTime;           
        }
    }

    public void FireAtPlayer()
    {
      Instantiate(SniperRound, sniperPos.position + blastPos, Quaternion.identity);
      snipeLaser.material = mat1;
    }

    IEnumerator FireDelay()
    {       
        snipeLaser.material = mat2;
        yield return new WaitForSeconds(3f);
        FireAtPlayer();
    }
}
