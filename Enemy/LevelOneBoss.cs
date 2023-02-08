using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOneBoss : MonoBehaviour, IFireStatus<float>, IFrostStatus<float>
{
    public float hhHealth;
    public int B1points;

    private Animator hhAnim;
    PlayerPushbox thePlayer;
    Level_Manager bossDefeat;

    public Slider hh;
    private int recoilChance;
    private int chargeChance;
    public float pointMod;
    public float moveSpeed;
    public float moveSpeedMax;

    public bool isCharge;

    Hurtbox myHurtbox;
    Hitbox basicHB;
    Hitbox strongHB;
    Hitbox chargeHB;
    Hitbox[] myHitboxes;

    Coroutine coDot;
    Coroutine coSlow;

    void Awake()
    {
        myHurtbox = GetComponentInChildren<Hurtbox>();
        basicHB = GetComponentInChildren<HH_BasicHB>();
        strongHB = GetComponentInChildren<HH_StrongHB>();
        chargeHB = GetComponentInChildren<HH_ChargeHB>();
        myHitboxes = GetComponentsInChildren<Hitbox>();
    }

	void Start ()
    {
        bossDefeat = FindObjectOfType<Level_Manager>();

        hhHealth = 80;
        
        hhAnim = GetComponent<Animator>();

        isCharge = false;
	}
	
    public void TurnOffHitboxes()
    {
        foreach(Hitbox hitbox in myHitboxes)
        {
            hitbox.ClearCollidedList();
            hitbox.gameObject.SetActive(false);
        }
    }
	
    public void TakeDot(float damage, float duration)
    {
        if(coDot != null)
        {
            StopCoroutine(coDot);
        }
        coDot = StartCoroutine(FireDot(damage, duration));
    }

    public IEnumerator FireDot(float damage, float duration)
    {
        for(int i = 0; i < 4; i++)
        {
            TakeDamage(damage / 4);
            yield return new WaitForSeconds(duration / 3);
        }
    }

    public void TakeSlow(float percentage, float duration)
    {
        if(coSlow != null)
        {
            StopCoroutine(coSlow);
        }
        coDot = StartCoroutine(FrostSlow(percentage, duration));
    }

    public IEnumerator FrostSlow(float slowPercentage, float duration)
    {
        moveSpeed *= slowPercentage;
        yield return new WaitForSeconds(duration);
        moveSpeed = moveSpeedMax;
    }

    public IEnumerator BossVictory()
    {
        Destroy(myHurtbox);
        GivePoints(1);
        yield return new WaitForSeconds(5f);
        bossDefeat.LoadNextScene();
    }

	void Update ()
    {
        hh.value = hhHealth;
        if (hhHealth <= 0)
        {
            hhAnim.SetTrigger("Is_Dead");
        }
        ChargeChance();
	}

    //If I take damage... do this once, until I take damage again...
    //public void StaggerChance()
    //{
    //    recoilChance = Random.Range(0, 5);

    //    //Set this up so that the basic attack from the character will have a lower chance
    //    //and the Strong attack will have a higher chance...
    //    if(recoilChance == 4)
    //    {
    //        hhAnim.SetTrigger("Is_Recoil");
    //    }
    //}

    //Chance for calling the bosses charge when health reaches half
    public void ChargeChance()
    {
        if(hhHealth <= 25)
        {
            chargeChance = Random.Range(0, 500);
            if(chargeChance == 499)
            {
                hhAnim.SetBool("Is_Charging", true);
                isCharge = true;
                if(hhAnim.GetBool("Is_Move") == true)
                {
                    hhAnim.SetBool("Is_Move", false);
                }
            }
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        hhAnim.SetBool("Is_Charging", false);
        CloseChargeHP();
    }

    public void TakeDamage(float damage)
    {
        hhHealth -= damage;
        if(hhHealth <= 0)
        {
            //Add death stuff here
            hhAnim.SetTrigger("Is_Dead");
            StartCoroutine(BossVictory());
        }
        else if (Random.Range(1, 5) <= damage)
        {
            hhAnim.SetTrigger("Is_Recoil");
        }
        Invoke("OpenHurtbox", 0.3f);
    }

    public void OpenHurtbox()
    {
        myHurtbox.ChangeState<HurtboxOpenState>();
    }

    public int GivePoints(float mod)
    {
        
     
  // FindObjectOfType<GP_Canvas>().ScoreDisplay(B1points);
        return B1points;
    }

    public void EnableHitbox(Hitbox temp)
    {
        temp.gameObject.SetActive(true);
    }

    public void DisableHitbox(Hitbox temp)
    {
        temp.ClearCollidedList();
        temp.gameObject.SetActive(false);
    }

    public void OpenBasicHB()
    {
        EnableHitbox(basicHB);
    }
    public void CloseBasicHB()
    {
        DisableHitbox(basicHB);
    }
    public void OpenStrong()
    {
        EnableHitbox(strongHB);
    }
    public void CloseStrongHB()
    {
        DisableHitbox(strongHB);
    }
    public void OpenChargeHP()
    {
        EnableHitbox(chargeHB);
    }
    public void CloseChargeHP()
    {
        DisableHitbox(chargeHB);
    }
}
