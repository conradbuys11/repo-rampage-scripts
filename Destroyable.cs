/* Creator: Tim Stiles
 * Date Created: 11/3/18
 * Function: Objects with this script take damage from the player and eventually break then instantiates a healing item.
 * 
 * References:
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public float ObjectHP;
    private GameObject Hitbox1;
    private GameObject Hitbox2;
    private GameObject Cam;

    public GameObject Healer;
    public float HitCooldown;

    void Awake()
    {
        //Hitbox1 = FindObjectOfType<Hitbox>().gameObject;
        //Hitbox2 = FindObjectOfType<StrongHitbox>().gameObject;
    }
    // Use this for initialization
    void Start ()
    {
        ObjectHP = 10;

        Cam = GameObject.Find("Player Camera").gameObject;
	}

    void Update()
    {
        if (ObjectHP <= 0)
        {
            Instantiate(Healer, transform.position, transform.rotation);
            Cam.GetComponent<AudioDirector>().Env26();
            gameObject.SetActive(false); 
        }

        HitCooldown = HitCooldown - Time.deltaTime;
    }

    // Update is called once per frame
    void OnTriggerStay (Collider other)
    {
        if (other.GetComponent<Hitbox>() && HitCooldown <= 0)
        {
            Cam.GetComponent<AudioDirector>().Env25();
            //ObjectHP = ObjectHP - Hitbox1.GetComponent<LightHitbox>().attackDamage;
            HitCooldown = .2f;
        }

        //if (other.GetComponent<Hitbox>().IsCurrentState<HitboxOpenState>())
        //{
        //    Cam.GetComponent<AudioDirector>().Env25();
        //    ObjectHP = ObjectHP - Hitbox2.GetComponent<StrongHitbox>().attackDamage;
        //}
    }
}
