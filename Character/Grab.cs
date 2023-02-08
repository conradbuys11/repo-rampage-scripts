using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    bool grab;
    bool whoopen;
    GameObject thingIGrabbed;
    PlayerPushbox playerRef;

	void Start ()
    {
        playerRef = FindObjectOfType<PlayerPushbox>();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Semicolon) && grab)
        {
            playerRef.AllowInput(true);

            if (thingIGrabbed.GetComponent<Enemy_Thug>())
            {
                thingIGrabbed.GetComponent<Enemy_Thug>().enabled = true;
            }
            else if (thingIGrabbed.GetComponent<Enemy_Bruiser>())
            {
                thingIGrabbed.GetComponent<Enemy_Bruiser>().enabled = true;
            }
            else if (thingIGrabbed.GetComponent<Enemy_kungFu>())
            {
                thingIGrabbed.GetComponent<Enemy_kungFu>().enabled = true;
            }

            CameraFollow.CinematicPosition();
            grab = false;
        }
        else if (Input.GetKeyDown(KeyCode.Semicolon) && thingIGrabbed != null)
        {
            playerRef.AllowInput(false);

            if (thingIGrabbed.GetComponent<Enemy_Thug>())
            {
                thingIGrabbed.GetComponent<Enemy_Thug>().enabled = false;
            }
            else if (thingIGrabbed.GetComponent<Enemy_Bruiser>())
            {
                thingIGrabbed.GetComponent<Enemy_Bruiser>().enabled = false;
            }
            else if (thingIGrabbed.GetComponent<Enemy_kungFu>())
            {
                thingIGrabbed.GetComponent<Enemy_kungFu>().enabled = false;
            }

            if (!CameraFollow.GetCinematicView())
            {
                CameraFollow.CinematicPosition();
            }
            grab = true;
        }
        
        if (grab)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.K))
            {
                if (!whoopen)
                {
                    StartCoroutine(Whoopen());
                }
            }
            if(thingIGrabbed != null)
            {
                if(transform.rotation.y > 0)
                {
                    if (thingIGrabbed.GetComponent<Enemy_kungFu>())
                    {
                        thingIGrabbed.transform.position = transform.position + new Vector3(1, 1, 0);
                    }
                    else
                    {
                        thingIGrabbed.transform.position = transform.position + new Vector3(1, 0, 0);
                    }
                }
                else
                {
                    if (thingIGrabbed.GetComponent<Enemy_kungFu>())
                    {
                        thingIGrabbed.transform.position = transform.position + new Vector3(1, 1, 0);
                    }
                    else
                    {
                        thingIGrabbed.transform.position = transform.position + new Vector3(-1, 0, 0);
                    }
                }
            }
            else
            {
                playerRef.AllowInput(true);
                CameraFollow.CinematicPosition();
                grab = false;
            }
        }
	}

    IEnumerator Whoopen()
    {
        whoopen = true;
        thingIGrabbed.GetComponent<IDamagable>().TakeDamage(4);
        yield return new WaitForSecondsRealtime(.1f);
        whoopen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && !grab)
        {
            if (other.GetComponentInParent<Enemy_Thug>())
            {
                thingIGrabbed = other.GetComponentInParent<Enemy_Thug>().gameObject;
            }
            else if (other.GetComponentInParent<Enemy_Bruiser>())
            {
                thingIGrabbed = other.GetComponentInParent<Enemy_Bruiser>().gameObject;
            }
            else if (other.GetComponentInParent<Enemy_kungFu>())
            {
                thingIGrabbed = other.GetComponentInParent<Enemy_kungFu>().gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && !grab)
        {
            thingIGrabbed = null;
        }
    }
}
