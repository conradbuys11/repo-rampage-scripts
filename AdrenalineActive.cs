using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdrenalineActive : MonoBehaviour
{
    public float maxAdrenaline;
    public float minAdrenaline;
    public float currentAdrenalin;
    public float AdrenTime;
    public float maxAdrenTime;

    public Slider AB;

    bool AdrenisAct = false;

    Hitbox[] playerHitboxes;


    //public float PreiveHealth;


    // Use this for initialization
    private void Awake()
    {
        AB = GameObject.Find("AdrenalineBar").GetComponent<Slider>();
        playerHitboxes = FindObjectOfType<PlayerPushbox>().GetComponentsInChildren<Hitbox>();
    }

    void Start ()
    {
        maxAdrenaline = 100f;
        minAdrenaline = 0f;
        currentAdrenalin = 0f;
        AdrenTime = 3f;
        maxAdrenTime = 3f;
        //PreiveHealth = FindObjectOfType<PlayerStatus>().health;
        AB.maxValue = maxAdrenaline;
        AB.minValue = minAdrenaline;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    currentAdrenalin = maxAdrenaline;
        //}

        AB.value = currentAdrenalin;

        if(AdrenisAct == false)
        {
            currentAdrenalin += 3.4f * Time.deltaTime;
         
        }
        if (Input.GetKeyDown(KeyCode.E) && currentAdrenalin >= maxAdrenaline)
        {
            AdrenisAct = true;
        }
        ActAdrenalin();

    }

    void ActAdrenalin()
    {
        if(AdrenisAct == true)
        {
            AdrenTime -= 1f * Time.deltaTime;
            currentAdrenalin = 0f;
            for(int i = 0; i < playerHitboxes.Length; i++)
            {
                playerHitboxes[i].damageModifier = 5;
            }
            //FindObjectOfType<PlayerPushbox>().GetComponentInChildren<Hurtbox>().damaModifier = 2.0f;
        }
        if(AdrenTime <= 0)
        {
            AdrenisAct = false;
            currentAdrenalin = minAdrenaline;
            AdrenTime = maxAdrenTime;
            for (int i = 0; i < playerHitboxes.Length; i++)
            {
                playerHitboxes[i].damageModifier = 1;
            }
            //FindObjectOfType<PlayerPushbox>().GetComponentInChildren<Hurtbox>().damaModifier = 1.0f;
        }

    }

    // Funtion that works with PlayerPushbox TakeDamage
    public void AdrenalinIncr()
    {
        currentAdrenalin += 1.2f;
    }

    //void AdrenActive()
    //{
    //    AdrenTime -= .5f * Time.deltaTime;
    //}
}
