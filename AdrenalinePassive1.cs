using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdrenalinePassive1 : MonoBehaviour
{
    public float miNAdren;
    public float maXAdren;
    public float cuRAdren;
    public float AdrenStage1;
    public float AdrenStage2;
    public float AdrenStage3;
    public float AdrenStage4;
    public float AdrenStage5;

    public Slider AdrenBar;

    public Hitbox[] PlayHitB;

    private void Awake()
    {
        AdrenBar = GameObject.Find("AdrenalineBar").GetComponent<Slider>();
        PlayHitB = FindObjectOfType<PlayerPushbox>().GetComponentsInChildren<Hitbox>();
    }
    // Use this for initialization
    void Start ()
    {
        miNAdren = 0;
        maXAdren = 100;
        cuRAdren = 0;
        AdrenStage1 = 20;
        AdrenStage2 = 40;
        AdrenStage3 = 60;
        AdrenStage4 = 80;
        AdrenStage5 = 93.5f;

        AdrenBar.maxValue = maXAdren;
        AdrenBar.minValue = miNAdren;
	}
	
	// Update is called once per frame
	void Update ()
    {
        AdrenBar.value = cuRAdren;
        //FindObjectOfType<PlayerPushbox>().GetComponentInChildren<Hurtbox>().damaModifier = 6.5f;

        cuRAdren -= 2f * Time.deltaTime;
        if (cuRAdren <= 0)
        {
            cuRAdren = miNAdren;
        }
        AdrenRate();
	}

    void AdrenRate()
    {
        if (cuRAdren >= AdrenStage5)
        {
            for (int i = 0; i < PlayHitB.Length; i++)
            {
                PlayHitB[i].damageModifier = 12;
            }
        }
        else if (cuRAdren >= AdrenStage4)
        {
            for(int i = 0; i < PlayHitB.Length; i++)
            {
                PlayHitB[i].damageModifier = 9;
            }
        }
        else if(cuRAdren >= AdrenStage3)
        {
            for(int i = 0; i < PlayHitB.Length; i++)
            {
                PlayHitB[i].damageModifier = 6;
            }
        }
        else if(cuRAdren >= AdrenStage2)
        {
            for(int i = 0; i < PlayHitB.Length; i++)
            {
                PlayHitB[i].damageModifier = 3;
            }
        }
        else if(cuRAdren >= AdrenStage1)
        {
            for(int i = 0; i < PlayHitB.Length; i++)
            {
                PlayHitB[i].damageModifier = 2;
            }
        }
    }

    public void AdrenHit()
    {
        cuRAdren += 5.8f;
    }
}
