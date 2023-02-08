/* Creator: Tim Stiles
 * Date Created: 11/10/18
 * Function: Gives functionality to instantiated throwable items/ the stun grenade to check if they hit things or reach the end of their life cycle and do something on distruction (typically
 * play a sound).
 * 
 * References:
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenade : MonoBehaviour
{
    public float LifeTime;
    public GameObject FallenItem;
    private Vector3 ItemPos;
    public float ProDamage = 5;
    public bool dealDot;
    float dotDamage = 8;
    float dotDuration = 3;

    // Use this for initialization
    void Start ()
    {
        LifeTime = 0.3f;
        ItemPos = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        LifeTime = LifeTime - Time.deltaTime;

        ItemPos = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
        SelfDestruct();
	}

    void SelfDestruct()
    {
        if (LifeTime <= 0)
        {
            EndLife();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject Cam = GameObject.Find("Player Camera").gameObject;

        if (other.GetComponent<IDamagable>() != null)
        {
            Cam.GetComponent<AudioDirector>().EnemyLanded();
            other.GetComponent<EnemyBase>().pointMod = 1.25f;
            other.SendMessage("GetHitBy", ProDamage);
            if (dealDot && other.GetComponent<IFireStatus<float>>() != null)
            {
                other.GetComponent<EnemyBase>().TakeDot(dotDamage, dotDuration);
            }
            other.GetComponent<EnemyBase>().ResetPointModifier();
            Destroy(gameObject);
        }
    }

    private void EndLife()
    {
        if (tag == "StunNade")
        {
            GameObject Cam = GameObject.Find("Player Camera").gameObject;
            Cam.GetComponent<AudioDirector>().Env29();
            Instantiate(FallenItem, transform.position, transform.rotation);
        }

        else
        {
            GameObject Cam = GameObject.Find("Player Camera").gameObject;
            Cam.GetComponent<AudioDirector>().Env30();
            Instantiate(FallenItem, ItemPos, transform.rotation);
        }
    }
}
