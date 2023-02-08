using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : FSGDN.StateMachine.MachineBehaviour, IDamagable, IFireStatus<float>, IFrostStatus<float>, ILightningStatus<float>
{
    float attackDistance;
    protected PlayerPushbox myPlayer;
    FightArea enemCount;
    public Renderer myRenderer;
    Animator myAnim;
    StatusDisk myStatusDisc;

    BoxCollider myPushbox;
    public Hurtbox myHurtbox;
    Hitbox myHitbox;

    public string myName;
    public float myMaxHP;
    public float myCurHP;
    public float moveSpeed;
    public float moveSpeedMax;
    public float despawnTime;
    public float recoilTime = 1;
    public float waitToAttackTime;
    public float postAttackTime;
    public float pointMod;

    protected Coroutine coAttacking;
    protected Coroutine coDot;
    protected Coroutine coSlow;
    protected Coroutine coStun;

    //COLORS
    public Color recoilStunColor = new Color(0.4245283f, 0.4245283f, 0.4245283f, 1);
    public Color idleColor = new Color(0, 1, 1, 1);
    public Color pursueColor = new Color(1, 1, 0, 1);
    public Color prepAttackColor = new Color(0.6f, 0.6f, 0, 1);
    public Color attackColor = Color.red;
    public Color deadColor = Color.black;

    bool attacking;

    public int pointReward;
    public string myState;

    public GameObject floatingDamageNumber;
    public Vector3 idleLocation;
    public Vector3 overrideIdleLocation;

    private GameObject Cam;

    public FSGDN.StateMachine.State prevState;

    void ChangeTOPursue()
    {
        ChangeState<EnemyPursueState>();
    }

    private void Awake()
    {
        myStatusDisc = GetComponentInChildren<StatusDisk>();
        myPlayer = FindObjectOfType<PlayerPushbox>();
        myPushbox = GetComponent<BoxCollider>();
        myRenderer = GetComponent<Renderer>();
        myAnim = GetComponent<Animator>();
        myHurtbox = GetComponentInChildren<Hurtbox>();
        //myName = EnemyNameBasket.GenerateEnemyName();
        Cam = GameObject.Find("Player Camera").gameObject;
    }

    public override void AddStates()
    {
        AddState<EnemyIdleState>();
        AddState<EnemyRecoilState>();
        AddState<EnemyKOState>();
        AddState<EnemyAttackingState>();
        AddState<EnemyBlockState>();
        AddState<EnemyPursueState>();
        AddState<EnemyStunnedState>();

        SetInitialState<EnemyIdleState>();
        prevState = currentState;
    }

    public override void Update()
    {
        base.Update();
        myState = currentState.ToString();
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeState<EnemyPursueState>();
        }
    }

    public void TakeDot(float damage, float duration)
    {
        if (coDot != null)
        {
            StopCoroutine(coDot);
        }
        coDot = StartCoroutine(FireDot(damage, duration));
    }

    public IEnumerator FireDot(float damage, float duration)
    {
        myStatusDisc.Dotted();
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(duration / 3);
            TakeDamage(damage / 4);
        }
        myStatusDisc.EndStatus();
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
        myStatusDisc.Slowed();
        moveSpeed *= slowPercentage;
        yield return new WaitForSeconds(duration);
        moveSpeed = moveSpeedMax;
        myStatusDisc.EndStatus();
    }

    public void TakeStun(float duration)
    {
        if(coStun != null)
        {
            StopCoroutine(coStun);
        }
        coStun = StartCoroutine(LightningStun(duration));
    }

    public IEnumerator LightningStun(float duration)
    {
        if(myCurHP > 0)
        {
            attacking = false;
            prevState = currentState;
            if (coAttacking != null)
            {
                StopCoroutine(coAttacking);
            }
            ChangeState<EnemyStunnedState>();
            yield return new WaitForSeconds(duration);
            if (!IsCurrentState<EnemyKOState>() && !IsNextState<EnemyKOState>())
                if (prevState.ToString() == "EnemyIdleState")
                    ChangeState<EnemyIdleState>();
                else
                    ChangeState<EnemyPursueState>();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        Physics.IgnoreCollision(collision.collider, myPushbox);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if(!IsCurrentState<EnemyKOState>() && !IsNextState<EnemyKOState>() && !IsNextState<EnemyPursueState>() && !IsCurrentState<EnemyRecoilState>() && !IsNextState<EnemyRecoilState>() && !IsCurrentState<EnemyStunnedState>() && !IsNextState<EnemyStunnedState>())
        {
            if (Vector3.Distance(transform.position, CheckDistance()) <= 3)
            {
                if(transform.position.x == CheckDistance().x)
                {
                    if (other.tag == "Player" && !attacking)
                    {
                        coAttacking = StartCoroutine(AttackTimer());
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsCurrentState<EnemyKOState>() && !IsNextState<EnemyKOState>())
        {
            if (other.tag == "Player" && attacking && !IsCurrentState<EnemyRecoilState>() && !IsCurrentState<EnemyStunnedState>())
            {
                if (!IsCurrentState<EnemyPursueState>() && !IsNextState<EnemyPursueState>())
                {
                    ChangeState<EnemyPursueState>();
                }
                if(coAttacking != null)
                    StopCoroutine(coAttacking);
                attacking = false;
            }
        }
    }

    public virtual IEnumerator AttackTimer()
    {
        attacking = true;
        if (!IsCurrentState<EnemyAttackingState>() && !IsNextState<EnemyAttackingState>() && !IsCurrentState<EnemyKOState>() && !IsNextState<EnemyKOState>() && !IsNextState<EnemyRecoilState>() && !IsCurrentState<EnemyRecoilState>())
        {
            ChangeState<EnemyAttackingState>();
        }
        else { yield return null; }
        yield return new WaitForSeconds(waitToAttackTime);
        Attack();
        myRenderer.material.color = attackColor;
        yield return new WaitForSeconds(postAttackTime);
        attacking = false;
        //if (!IsNextState<EnemyRecoilState>() && !IsNextState<EnemyKOState>() && !IsNextState<EnemyAttackingState>())
        //    ChangeState<EnemyPursueState>();
    }

    public float GetHP()
    {
        return myCurHP;
    }

    public virtual void Block()
    {

    }

    public virtual IEnumerator Recoil()
    {
        prevState = currentState;
        attacking = false;
        if (coAttacking != null)
            StopCoroutine(coAttacking);
        ChangeState<EnemyRecoilState>();
        yield return new WaitForSeconds(recoilTime);
        EndRecoil();
    }

    public void EndRecoil()
    {
        if (!IsCurrentState<EnemyKOState>() && !IsNextState<EnemyKOState>())
        {
            if (prevState.ToString() == "EnemyIdleState")
            {
                ChangeState<EnemyAttackingState>();
            }
            else if (prevState.ToString() == "EnemyPursueState" || prevState.ToString() == "EnemyAttackingState")
            {
                ChangeState<EnemyPursueState>();
            }
        }
    }

    public void KnockedOut()
    {
        StopAllCoroutines();
        Destroy(myPushbox);
        if(GetComponentInParent<FightArea>() != null)
        {
            enemCount = GetComponentInParent<FightArea>();
            enemCount.TotalEnemy--;
        }
        GivePoints(pointMod);
        StartCoroutine(Despawn());
    }

    public IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTime + 0.75f);
        if (GetComponentInParent<Enemy_AI_Director>() != null)
            GetComponentInParent<Enemy_AI_Director>().RemoveTheDead(this);
        Destroy(gameObject);
    }

    public virtual void MoveTowardsIdleLocation()
    {
        if(overrideIdleLocation == Vector3.zero)
        {

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, idleLocation, moveSpeed * Time.deltaTime);
        }
    }

    public virtual void MoveTowardsPlayer()
    {
        if(attacking) { return; }
        if(transform.position.x - myPlayer.transform.position.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
            transform.position = Vector3.MoveTowards(transform.position, CheckDistance(), moveSpeed * Time.deltaTime);
        }
        else if(transform.position.x - myPlayer.transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = Vector3.MoveTowards(transform.position, CheckDistance(), moveSpeed * Time.deltaTime);
        }
    }

    public virtual Vector3 CheckDistance()
    {
        float front;
        float back;
        Vector3 target;
        front = Vector3.Distance(transform.position, myPlayer.transform.position + new Vector3(5, 0, 0));
        back = Vector3.Distance(transform.position, myPlayer.transform.position + new Vector3(-5, 0, 0));
        if(front > back)
        {
            target = myPlayer.transform.position + new Vector3(-2f, 0, 0);
        }
        else
        {
            target = myPlayer.transform.position + new Vector3(2f, 0, 0);
        }
        return target;
    }

    public virtual void Attack()
    {
        Debug.Log("Attack");
        //set anim bool for attacking
        //set state?
    }

    public virtual void GetHitBy(int damage)
    {
        if (!IsCurrentState<EnemyKOState>() && !IsNextState<EnemyKOState>())
        {
            TakeDamage(damage);
            if (myCurHP > 0 && !IsNextState<EnemyStunnedState>() && !IsNextState<EnemyRecoilState>() && !IsNextState<EnemyKOState>() && !IsNextState<EnemyIdleState>())
            {
                attacking = false;
                if (coAttacking != null)
                    StopCoroutine(coAttacking);
                prevState = currentState;
                ChangeState<EnemyRecoilState>();
                Invoke("EndRecoil", recoilTime);
            }
        }

    }

    public virtual void TakeDamage(float damage)
    {
        GameObject createdNumber;
        myCurHP -= damage;
        createdNumber = Instantiate(floatingDamageNumber);
        createdNumber.transform.position = transform.position + new Vector3(0, 1, 0);
        createdNumber.GetComponent<TextMesh>().text = damage.ToString();
        Cam.GetComponent<AudioDirector>().EnemyHit();

        if(myCurHP <= 0)
        {
            KnockedOut();
            ChangeState<EnemyKOState>();
        }
    }

    public void TakeEnviroDamage(int damage)
    {

    }

    public virtual int GivePoints(float mod)
    {
        int score = Mathf.RoundToInt(pointReward * mod);
    //FindObjectOfType<GP_Canvas>().ScoreDisplay(score);
        return score;
    }

    public void ResetPointModifier()
    {
        pointMod = 1;
    }
}
