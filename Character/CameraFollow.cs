using UnityEngine;
using System.Collections;


/* Purpose: Made for camera follow of the player.
 * Creator: Trae Moews
 * Date: Oct 23, 2018
 * Contents: Values for camera zoom and player follow distance
 * Refrences: N/A
 */


public class CameraFollow : MonoBehaviour
{
    PlayerPushbox playerRef;
    static Animator myAnim;

    //Corrects offset to the Y axis
    private int correction = 8;
    [HideInInspector]
    public float xOffset =  13;
    [HideInInspector]
    public float yOffset = 9;
    [HideInInspector]
    public float zOffset = -23;
    [HideInInspector]
    public float camSpeed = 2;
    [HideInInspector]
    public float clippingPlane = .3f;

    float timer = 0;
    static bool cinematicView;
    public bool obstructed;
    bool freeze;

    Camera playerCamera;

	// Use this for initialization
	void Start ()
    {
        playerRef = FindObjectOfType<PlayerPushbox>();
        playerCamera = GetComponent<Camera>();
        myAnim = GetComponent<Animator>();
        Time.timeScale = 1;

        if (GameObject.FindGameObjectWithTag("Boss"))
        {
            playerRef.bossFreeze = true;
        }
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if(Time.timeScale <= .2f & !freeze)
        {
            StartCoroutine(FreezeFrame());
            freeze = true;
        }
        if (cinematicView)
        {
            if (playerRef.transform.eulerAngles.y < 180)
            {
                Vector3 targetPoint = new Vector3(playerRef.transform.position.x + xOffset / 5, playerRef.transform.position.y + yOffset / 2, playerRef.transform.position.z - 8);
               
                transform.position = Vector3.Lerp(transform.position, targetPoint, 8 * Time.deltaTime);
            }
            else
            {
                Vector3 targetPoint = new Vector3(playerRef.transform.position.x + -xOffset / 5, playerRef.transform.position.y + yOffset / 2, playerRef.transform.position.z - 8);
                
                transform.position = Vector3.Lerp(transform.position, targetPoint, 8 * Time.deltaTime);
            }
        }
        else
        {
            if (playerRef.transform.eulerAngles.y < 180)
            {
                Vector3 targetPoint = new Vector3(playerRef.transform.position.x + xOffset, playerRef.transform.position.y + yOffset, zOffset);

                if (obstructed)
                {
                    targetPoint = new Vector3 (WallCheck.Obstruction().x, playerRef.transform.position.y + yOffset, zOffset );
                }
                transform.position = Vector3.Lerp(transform.position, targetPoint, camSpeed * Time.deltaTime);
            }
            else
            {
                Vector3 targetPoint = new Vector3(playerRef.transform.position.x + -xOffset, playerRef.transform.position.y + yOffset, zOffset);

                if (obstructed)
                {
                    targetPoint = new Vector3(WallCheck.Obstruction().x, playerRef.transform.position.y + yOffset, zOffset);
                }

                transform.position = Vector3.Lerp(transform.position, targetPoint, camSpeed * Time.deltaTime);
            }
        }
        
    }

    public static void CameraShake()
    {
        myAnim.SetTrigger("Shake");
        Time.timeScale = .1f;
    }

    public static void CinematicPosition()
    {
        cinematicView = !cinematicView;
    }

    public static bool GetCinematicView()
    {
        return cinematicView;
    }

    IEnumerator FreezeFrame()
    {
        yield return new WaitForSeconds(.01f);
        Time.timeScale = 1;
        freeze = false;
    }

    
}
