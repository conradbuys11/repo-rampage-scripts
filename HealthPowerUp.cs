/* Creator: Tim Stiles
 * Date Created: 11/4/18
 * Function: Gives an object healing properties for the player when the player touches it and then destroys itself.
 * 
 * References: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    private GameObject Canvas;
    private GameObject Cam;

	// Use this for initialization
	void Start ()
    {
        Canvas = GameObject.Find("Gameplay Canvas").gameObject;
        Cam = GameObject.Find("Player Camera").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Cam.GetComponent<AudioDirector>().Env27();
            Canvas.GetComponent<PlayerStatus>().health = Canvas.GetComponent<PlayerStatus>().health + 20;
            Destroy(gameObject);
        }
    }
}
