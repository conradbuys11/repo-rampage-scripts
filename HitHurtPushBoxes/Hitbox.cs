using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

    //Collider of the hitbox.
    public BoxCollider myCollider;

    //How much damage this hitbox's attack will do.
    public int attackDamage;

    public PlayerPushbox myPushbox;

    //Did we hit something during an attack? (Contributes to combo count.)
    [HideInInspector]public bool hitSomething;

    //Reference to canvas.
    protected GP_Canvas myCanvas;

    public Transform myHurtbox;

    public int damageModifier = 1;

    [Header("Attack Effect")]
    public bool stagger = false;
    public float staggerDistance;
    public bool knockback = false;
    public float knockbackDistanceX;
    public float knockbackDistanceY;

    //Does this attack have any status effects?
    [Header("Status Effects?")]
    public bool dot;
    public bool slow;
    public bool stun;
    public bool poi;

    [Header("Only applicable if status enabled")]
    [Range(0.0f, 15.0f)]
    public float dotDamage;
    [Range(0.0f, 15.0f)]
    public float dotDuration;
    [Range(0.0f, 1.0f)]
    public float slowAmount;
    [Range(0.0f, 15.0f)]
    public float slowDuration;
    [Range(0.0f, 15.0f)]
    public float stunDuration;

    protected List<Hurtbox> collidedHurtboxes = new List<Hurtbox>();

    public virtual void Awake()
    {
        myCollider = GetComponent<BoxCollider>();
        myCanvas = FindObjectOfType<GP_Canvas>();
        myPushbox = FindObjectOfType<PlayerPushbox>();
    }

    public virtual void Start()
    {
        if (GetComponent<Hadouken>() == null)
            myHurtbox = GetComponentInParent<Rigidbody>().GetComponentInChildren<Hurtbox>().transform;
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if ((gameObject.tag == "Player" && other.gameObject.tag == "Enemy") || (gameObject.tag == "Enemy" && other.gameObject.tag == "Player"))
        {
            Hurtbox temp = other.GetComponent<Hurtbox>();
            if (!temp) { return; }
            foreach (Hurtbox box in collidedHurtboxes)
            {
                if (temp == box) { return; }
            }
            if (temp.IsCurrentState<HurtboxOpenState>() && !temp.IsNextState<HurtboxOpenState>())
            {
                temp.GetComponentInParent<IDamagable>().TakeDamage(attackDamage * damageModifier);
                collidedHurtboxes.Add(temp);
                if (temp.GetComponentInParent<IStagger>() != null)
                {
                    if(GetComponent<Hadouken>() != null)
                    {
                        if (transform.position.x > temp.transform.position.x)
                            temp.GetComponentInParent<IStagger>().Stagger(-staggerDistance);
                        else
                            temp.GetComponentInParent<IStagger>().Stagger(staggerDistance);
                    }
                    else if (knockback)
                    {
                        if(myHurtbox.transform.position.x > temp.transform.position.x)
                            temp.GetComponentInParent<IStagger>().KnockBack(-knockbackDistanceX, knockbackDistanceY);
                        else
                            temp.GetComponentInParent<IStagger>().KnockBack(knockbackDistanceX, knockbackDistanceY);
                    }
                    else if (stagger)
                    {
                        if (myHurtbox.transform.position.x > temp.transform.position.x)
                            temp.GetComponentInParent<IStagger>().Stagger(-staggerDistance);
                        else
                            temp.GetComponentInParent<IStagger>().Stagger(staggerDistance);
                    }

                }
                if(temp.GetComponentInParent<IFireStatus<float>>() != null && dot)
                {
                    temp.GetComponentInParent<IFireStatus<float>>().TakeDot(dotDamage, dotDuration);
                }
                if(temp.GetComponent<IFrostStatus<float>>() != null && slow)
                {
                    temp.GetComponentInParent<IFrostStatus<float>>().TakeSlow(slowAmount, slowDuration);
                }
                if (GetComponentInParent<PlayerPushbox>())
                {
                    GetComponentInParent<PlayerPushbox>().HitEnemy();
                }
            }
        }
    }

    //public void OnTriggerStay(Collider other)
    //{
    //    if ((gameObject.tag == "Player" && other.gameObject.tag == "Enemy") || (gameObject.tag == "Enemy" && other.gameObject.tag == "Player"))
    //    {
    //        Hurtbox temp = other.GetComponent<Hurtbox>();
    //        if (!temp) { return; }
    //        foreach (Hurtbox box in collidedHurtboxes)
    //        {
    //            if (temp == box) { return; }
    //        }
    //        if (temp.IsCurrentState<HurtboxOpenState>() && !temp.IsNextState<HurtboxOpenState>())
    //        {
    //            temp.GetComponentInParent<IDamagable>().TakeDamage(attackDamage * damageModifier);
    //            collidedHurtboxes.Add(temp);
    //        }
    //        if (temp.GetComponentInParent<IPoisonStatus<float>>() != null && poi)
    //        {
    //            temp.GetComponentInParent<IPoisonStatus<float>>().TakePoi(dotDamage);
    //        }
    //    }

    //}

    public void ClearCollidedList()
    {
        collidedHurtboxes.Clear();
        hitSomething = false;
    }

    public void EnableHitbox()
    {
        gameObject.SetActive(true);
    }

    public void DisableHitbox()
    {
        gameObject.SetActive(false);
    }

}

