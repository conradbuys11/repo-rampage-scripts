using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Powerup : MonoBehaviour
{
    public GameObject Invisible;

    PlayerPushbox damagePlus;

    private int multiplyer = 2;

    public float upTime = 10;
    private float maxUp;
    private float ranUp;

    public bool PlusUltra;
    public bool RandBeserk;

	void Start ()
    {
        if (!RandBeserk)
        {
            maxUp = upTime;
        }

        if (RandBeserk)
        {
            ranUp = Random.Range(8, 18);
            maxUp = ranUp;
        }
        
        damagePlus = FindObjectOfType<PlayerPushbox>();
	}
	

	void Update ()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime * 10);

        if (PlusUltra)
        {
            AttackUpDuration();         
        }
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlusUltra = true;
            damagePlus.super.SetActive(true);
            foreach(Hitbox hitbox in damagePlus.getAllHitboxes())
            {
                hitbox.damageModifier = 2;
            }           
            Invisible.SetActive(false);
        }
    }

    public void AttackUpDuration()
    {
        upTime -= Time.deltaTime;
        if(upTime <= 0)
        {
            PlusUltra = false;
            upTime = maxUp;
            damagePlus.super.SetActive(false);
            foreach (Hitbox hitbox in damagePlus.getAllHitboxes())
            {
                hitbox.damageModifier = 1;
            }
            Destroy(gameObject);
        }
    }
}
