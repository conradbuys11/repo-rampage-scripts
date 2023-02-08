using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdrenalinePassive2 : MonoBehaviour
{
    public float MaxAdr;
    public float MinAdr;
    public float CurAdr;

    private bool IsFull = false;

    public Slider AdrBar;

    Hitbox[] PlaHitBox;

    private void Awake()
    {
        AdrBar = GameObject.Find("AdrenalineBar").GetComponent<Slider>();
        PlaHitBox = FindObjectOfType<PlayerPushbox>().GetComponentsInChildren<Hitbox>();
    }

    // Use this for initialization
    void Start ()
    {
        MaxAdr = 100;
        MinAdr = 0;
        CurAdr = 0;

        AdrBar.maxValue = MaxAdr;
        AdrBar.minValue = MinAdr;

	}
	
	// Update is called once per frame
	void Update ()
    {
        AdrBar.value = CurAdr;
        if(IsFull == false)
        {
            CurAdr += 2.5f * Time.deltaTime;
            for (int i = 0; i < PlaHitBox.Length; i++)
            {
                PlaHitBox[i].damageModifier = 1;
            }
            if(CurAdr >= MaxAdr)
            {
                IsFull = true;
            }
        }
        if (IsFull == true)
        {
            CurAdr -= 4.20f * Time.deltaTime;
            for (int i = 0; i < PlaHitBox.Length; i++)
            {
                PlaHitBox[i].damageModifier = 4;
            }
            if(CurAdr <= MinAdr)
            {
                IsFull = false; 
            }
        }
    }
}
