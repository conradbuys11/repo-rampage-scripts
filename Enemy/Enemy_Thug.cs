using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy_Thug : MonoBehaviour, IEnemy, IDamagable, IStagger
{
    public GameObject playerRef;
    public GameObject rangedWeapon;
    public GameObject hitPoof;
    public GameObject deathPoof;
    public Rigidbody myRB;
    public Animator myAnim;
    public EAID myEAID;
    GameObject Cam;

    Hitbox[] myHitboxes;
    Hitbox punchCombo;
    Hitbox kneeCombo;
    Hitbox hammerCombo;

    public bool beingGrabbed;
    public bool canJump;
    public bool attacking;
    public bool attackOrder;
    public bool rangedAttacker;
    bool changeLanesRear;
    bool changeLanesFront;
    bool stagger;
    bool staggerFree;

    public int health;
    public int staggerIgnoreChance;

    [HideInInspector]public float immuneToCarTimer = 2;
    public float staggerGroundCheck;
    float attackInterval;
    float jumpChance;
    float jumpCheck;
    float jumpPower;
    float stopTime;
    float speed;
    float idelTime;
    float staggerPause;

    CapsuleCollider myCollider;

    Vector3 idleLocation;
    Vector3 previousIdle;
    Vector3 wallCheckVector;

    RaycastHit wallCheckRight;
    RaycastHit wallCheckLeft;

    Transform myFoot;    

    GameObject healthDisplay;
    Image[] healthMesh;
    Text enemyNameText;
    Outline enemyNameOutline;
    Color color;
    bool healthOn;
    string enemyName;

    NavMeshAgent myNavAgent;

    private void Awake()
    {
        speed = 10;
        jumpPower = 500;
        jumpChance = 70;
        health = 50;
        idelTime = 1f;
        canJump = true;
        myHitboxes = GetComponentsInChildren<Hitbox>();
        myRB = GetComponent<Rigidbody>();
        playerRef = FindObjectOfType<PlayerPushbox>().gameObject;
        myAnim = GetComponent<Animator>();
        myFoot = transform.GetChild(2).transform;
        myCollider = GetComponentInChildren<CapsuleCollider>();
        healthDisplay = Instantiate(Resources.Load("Prefab/Enemy Health") as GameObject, FindObjectOfType<GP_Canvas>().transform);
        myNavAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        enemyName = EnemyNameBasket.GenerateName();
        Cam = FindObjectOfType<AudioDirector>().gameObject;
        healthDisplay.GetComponentInChildren<Slider>().maxValue = health;
        healthDisplay.GetComponentInChildren<Slider>().minValue = 0;
        healthMesh = healthDisplay.GetComponentsInChildren<Image>();
        enemyNameText = healthDisplay.GetComponentInChildren<Text>();
        enemyNameText.text = enemyName;
        enemyNameOutline = healthDisplay.GetComponentInChildren<Outline>();
        for (int i = 0; i < healthMesh.Length; i++)
        {
            Color color;
            color = healthMesh[i].GetComponent<Image>().color;
            color.a = 0;
            healthMesh[i].GetComponent<Image>().color = color;
        }
        enemyNameText.color = new Color(enemyNameText.color.r,enemyNameText.color.g,enemyNameText.color.b ,0);
        enemyNameOutline.effectColor = new Color(enemyNameOutline.effectColor.r, enemyNameOutline.effectColor.g, enemyNameOutline.effectColor.b, 0);


        punchCombo = myHitboxes[0];
        kneeCombo = myHitboxes[1];
        hammerCombo = myHitboxes[2];
    }
    // Update is called once per frame
    void Update()
    {
        if (immuneToCarTimer > 0)
        {
            immuneToCarTimer -= Time.deltaTime;
        }
        if (healthOn)
        {
            healthDisplay.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0,5,0));

            for (int i = 0; i < healthMesh.Length; i++)
            {
                Color color;
                color = healthMesh[i].GetComponent<Image>().color;
                color.a -= .01f;
                healthMesh[i].GetComponent<Image>().color = color;
            }
            enemyNameText.color = new Color(enemyNameText.color.r, enemyNameText.color.g, enemyNameText.color.b, enemyNameText.color.a - 0.01f);
            enemyNameOutline.effectColor = new Color(enemyNameOutline.effectColor.r, enemyNameOutline.effectColor.g, enemyNameOutline.effectColor.b, enemyNameOutline.effectColor.a - 0.01f);

            if (healthMesh[0].GetComponent<Image>().color.a <= 0)
            {
                healthOn = false;
            }
        }

        if (Vector3.Distance(transform.position, playerRef.transform.position) >= 200)
        {
            TakeDamage(1000);
        }
        
        if (!beingGrabbed)
        {
            if (stagger && !staggerFree)
            {
                staggerPause -= Time.deltaTime;
                if (staggerPause <= 0)
                {
                    RaycastHit hit;
                    Physics.Raycast(transform.position, Vector3.down, out hit, staggerGroundCheck);
                    if (hit.collider != null && hit.collider.gameObject.GetComponent<Ground>())
                    {
                        stagger = false;
                        myRB.isKinematic = true;
                        staggerPause = 0;
                    }
                }
            }

            if (attacking)
            {
                myNavAgent.isStopped = true;
            }

            if (rangedAttacker)
            {
                myNavAgent.stoppingDistance = 15;
                //get distance to player
                //move closer if not in range
                //attack if close enough
                //call different attack animations
                if (Vector3.Distance(transform.position, playerRef.transform.position) < 20 && !attacking)
                {
                    if(Mathf.Abs(playerRef.transform.position.z - transform.position.z) > 3)
                    {
                        MoveCloser();
                    }
                    AttackRanged();
                    RotateToFace();
                }
                else if (Vector3.Distance(transform.position, playerRef.transform.position) > 15 && !attacking)
                {
                    MoveCloser();
                    RotateToFace();
                }
            }
            else if (attackOrder)
            {
                if (Vector3.Distance(transform.position, playerRef.transform.position) >= 4 && !attacking)
                {
                    MoveToAttack();
                    RotateToFace();
                }
                else if(!attacking)
                {
                    AttackPrep();
                }
            }
        }      
    }

    bool WallCheckRear()
    {
        wallCheckLeft = new RaycastHit();
        Ray wallCheckRayRight = new Ray(transform.position, Vector3.back);
        Physics.Raycast(wallCheckRayRight, out wallCheckLeft, 20f);

        if (wallCheckLeft.collider != null)
        {
            if (wallCheckLeft.collider.GetComponent<Obstruction>())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }

    }

    bool WallCheckFront()
    {

        wallCheckRight = new RaycastHit();
        Ray wallCheckRayRight = new Ray(transform.position, Vector3.forward);
        Physics.Raycast(wallCheckRayRight, out wallCheckRight, 20f);

        if (wallCheckRight.collider != null)
        {
            if (wallCheckRight.collider.GetComponent<Obstruction>())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    void RotateToFace()
    {
        if (transform.position.x - playerRef.transform.position.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
            wallCheckVector = Vector3.forward;
        }
        else if (transform.position.x - playerRef.transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            wallCheckVector = Vector3.back;
        }
    }

    void AttackPrep()
    {
        myAnim.SetTrigger("Prep Attack");
        attacking = true;
    }

    void AttackLogic()
    {
        if(Vector3.Distance(transform.position, playerRef.transform.position) >= 5)
        {
            ResetAnimTriggers();
            attacking = false;
            myAnim.Play("Idle");
            return;
        }

        RotateToFace();

        int attack = Random.Range(0, 3);

        if (attack == 0)
        {
            RotateToFace();
            Attack1();
        }
        else if (attack == 1)
        {
            RotateToFace();
            Attack2();
        }
        else if (attack == 2)
        {
            RotateToFace();
            Attack3();
        }
        AttackIntervalAssign();
        
    }

    public void IsAttacking()
    {
        attacking = false;
    }

    void AttackIntervalAssign()
    {
        attackInterval = Random.Range(2f, 3f);
    }

    //adding jump lanes code
    void JumpLanes()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit);
        if (hit.transform.tag == "Rear Lane" || hit.transform.name == "Rear Lane")
        {
            transform.position += new Vector3(0, 0, -6);
        }
        else if (hit.transform.tag == "Front Lane" || hit.transform.name == "Front Lane")
        {
            transform.position += new Vector3(0, 0, 6);
        }
    }

    void MoveToAttack()
    {
        myNavAgent.isStopped = false;
        myNavAgent.SetDestination(playerRef.transform.position);
    }

    void MoveCloser()
    {
        myNavAgent.isStopped = false;
        myNavAgent.SetDestination(playerRef.transform.position);
    }

    void PlayerInDifferentLane()
    {
        //Check the thing im standing on

        //if its the rear lane
        //Check for a wall in my path to the front lane

        //if there is not a wall
        //jump to the front lane

        //if there is a wall
        //Move closer to the players x

        //if the front lane do the above but for the oposite lane

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit);

        if (hit.transform.tag == "Rear Lane" || hit.transform.name == "Rear Lane")
        {
            //ceck for wall
            if (WallCheckRear())
            {
                if (!changeLanesFront)
                {
                    StartCoroutine(DelayLaneJumpFront());
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerRef.transform.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            }
        }
        else if (hit.transform.tag == "Front Lane" || hit.transform.name == "Front Lane")
        {
            if (WallCheckFront())
            {
                if (!changeLanesRear)
                {
                    StartCoroutine(DelayLaneJumpRear());
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerRef.transform.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            }
        }
        else
        {
            throw new System.Exception("Enemy: " + gameObject.name + " is not in a proper lane");
        }
    }

    IEnumerator DelayLaneJumpFront()
    {
        changeLanesFront = true;
        yield return new WaitForSeconds(.5f);
        transform.position += new Vector3(0, 0, -6);
        changeLanesFront = false;
    }

    IEnumerator DelayLaneJumpRear()
    {
        changeLanesRear = true;
        yield return new WaitForSeconds(.5f);
        transform.position += new Vector3(0, 0, 6);
        changeLanesRear = false;
    }

    void Attack1()
    {
        myAnim.SetTrigger("PPK");
        Cam.GetComponent<AudioDirector>().EnemyAttack();
        Cam.GetComponent<AudioDirector>().ThugAttack();
    }

    void Attack2()
    {
        myAnim.SetTrigger("Triple Knee");
        Cam.GetComponent<AudioDirector>().EnemyAttack();
        Cam.GetComponent<AudioDirector>().ThugAttack();
    }

    void Attack3()
    {
        myAnim.SetTrigger("Knee Thrust Hammer Fist");
        Cam.GetComponent<AudioDirector>().EnemyAttack();
        Cam.GetComponent<AudioDirector>().ThugAttack();
    }

    void AttackRanged()
    {
        attackInterval -= Time.deltaTime;
        if (attackInterval <= 0)
        {
            myAnim.SetTrigger("Ranged Attack");

            attacking = true;
            AttackIntervalAssign();
        }
    }

    void ThrowSomething()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            GameObject temp = Instantiate(rangedWeapon, transform.position + new Vector3(-2, 0, 0), Quaternion.identity);
            temp.GetComponent<Hadouken>().OtherWay();
        }
        else if (transform.rotation.eulerAngles.y >= -90)
        {
            GameObject temp = Instantiate(rangedWeapon, transform.position + new Vector3(2, 0, 0), Quaternion.identity);
        }
    }
    
    public void EnablePunchHitbox()
    {
        punchCombo.gameObject.SetActive(true);
    }

    public void DisablePunchHitbox()
    {
        punchCombo.gameObject.SetActive(false);
    }

    public void EnableKneeHitbox()
    {
        kneeCombo.gameObject.SetActive(true);
    }

    public void DisableKneeHitbox()
    {
        kneeCombo.gameObject.SetActive(false);
    }

    public void EnableHammerHitbox()
    {
        hammerCombo.gameObject.SetActive(true);
    }

    public void DisableHammerHitbox()
    {
        hammerCombo.gameObject.SetActive(false);
    }

    public void StaggerChange()
    {
        kneeCombo.GetComponent<Hitbox>().stagger = !kneeCombo.GetComponent<Hitbox>().stagger;
    }

    void ClearHitboxList()
    {
        hammerCombo.ClearCollidedList();
        punchCombo.ClearCollidedList();
        kneeCombo.ClearCollidedList();
    }
    
    void OnDeath()
    {
        myEAID.AttackRequest(gameObject);
        Cam.GetComponent<AudioDirector>().ThugDeath();
        Destroy(gameObject);
    }

    void StaggerIgnore()
    {
        if (staggerIgnoreChance == 0)
        {
            staggerIgnoreChance = 25;
        }
        if(staggerIgnoreChance > Random.Range(1, 101))
        {
            staggerFree = true;
            RotateToFace();
            attacking = true;
            myAnim.SetTrigger("StaggerIgnoreAttack");
            staggerIgnoreChance = 0;
            if (Vector3.Distance(transform.position, playerRef.transform.position) >= 5)
            {
                ResetAnimTriggers();
                attacking = false;
                myAnim.Play("Idle");
                return;
            }
        }
        else
        {
            staggerIgnoreChance += 15;
        }
    }

    void ChangeRBKen()
    {
        myRB.isKinematic = !myRB.isKinematic;
    }

    public void StaggerFreeAttackDone()
    {
        staggerFree = false;
    }

    //********************************************************************
    //Interface functions
    //********************************************************************
    //Interface functions
    //********************************************************************
    //Interface functions
    //********************************************************************
    //Interface functions
    //********************************************************************
    //Interface functions
    //********************************************************************

    //IEnemy
    public bool IsAttack()
    {
        return attackOrder;
    }

    public void BeginAttack(bool toWar)
    {
        attackOrder = toWar;
    }

    public void Idel()
    {
        
    }

    public void SetDirector(EAID director, Text healthText)
    {
        myEAID = director;
    }


    //IDamagabe

    public void TakeDamage(float dmg)
    {
        CameraFollow.CameraShake();
        health -= Mathf.RoundToInt(dmg);
        Cam.GetComponent<AudioDirector>().EnemyHit();
        
        healthOn = true;
        healthDisplay.GetComponentInChildren<Slider>().value = health;
        for(int i = 0; i < healthMesh.Length; i++)
        {
            Color color;
            color = healthMesh[i].GetComponent<Image>().color;
            color.a = 1;
            healthMesh[i].GetComponent<Image>().color = color;
        }
        enemyNameText.color = new Color(enemyNameText.color.r, enemyNameText.color.g, enemyNameText.color.b, 1);
        enemyNameOutline.effectColor = new Color(enemyNameOutline.effectColor.r, enemyNameOutline.effectColor.g, enemyNameOutline.effectColor.b, 1);

        if (health <= 0)
        {
            Destroy(healthDisplay);
            Instantiate(deathPoof, transform.position, Quaternion.Euler(Vector3.zero));
            OnDeath();
        }
        else
        {
            GameObject poof = Instantiate(hitPoof, transform);
            poof.transform.position += new Vector3(0, 1, -2);
        }
    }

    public void TakeEnviroDamage(int damage)
    {
        health -= damage;
        GameObject poof = Instantiate(hitPoof, transform);
        poof.transform.position += new Vector3(0, 1, -2);
        if (health <= 0)
        {
            Destroy(healthDisplay);
            Instantiate(deathPoof, transform.position, Quaternion.Euler(Vector3.zero));
            OnDeath();
        }
    }

    public int GivePoints(float mod)
    {
        return 0;
    }

    //IStagger 

    public void Stagger(float x)
    {
        myAnim.SetTrigger("Stagger");
    }

    public void KnockBack(float x, float y)
    {
        myAnim.SetTrigger("Knockback");
        if (!staggerFree)
        {
            if (myRB.isKinematic)
            {
                myRB.isKinematic = false;
                stagger = true;
            }
        }
        staggerPause = 1;
        myRB.AddForce(x, y, 0);
    }

    public void DisableAllHitboxes()
    {
        foreach(Hitbox hitbox in myHitboxes)
        {
            hitbox.ClearCollidedList();
            hitbox.gameObject.SetActive(false);
        }
    }

    public void StahpVelocity()
    {
        myRB.velocity = Vector3.zero;
    }

    bool CheckIfStagger()
    {
        if (myAnim.GetCurrentAnimatorClipInfo(0).ToString() == "Stagger" || myAnim.GetCurrentAnimatorClipInfo(0).ToString() == "Knockback")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAnimTriggers()
    {
        myAnim.ResetTrigger("Punch");
        myAnim.ResetTrigger("Knee Thrust");
        myAnim.ResetTrigger("Hammer Fist");
        myAnim.ResetTrigger("Triple Knee");
        myAnim.ResetTrigger("Knee Thrust Hammer Fist");
        myAnim.ResetTrigger("PPK");
        myAnim.ResetTrigger("StaggerIgnoreAttack");
    }

    public void DisablePlayerCollision()
    {
        Physics.IgnoreCollision(myCollider, FindObjectOfType<PlayerPushbox>().GetComponent<BoxCollider>(), true);
    }

    public void EnablePlayerCollision()
    {
        Physics.IgnoreCollision(myCollider, FindObjectOfType<PlayerPushbox>().GetComponent<BoxCollider >(), false);
    }

    public void Falling()
    {
        RaycastHit rayHit;
        Debug.DrawRay(myFoot.position, Vector3.down, Color.blue, 0.2f);
        if (Physics.Raycast(myFoot.position, Vector3.down, out rayHit, 0.2f) && rayHit.transform.gameObject.layer == LayerMask.NameToLayer("BoundaryPushbox"))
        {
            myAnim.SetTrigger("GroundHit");
        }
    }

    public void SetInvincibilityOn()
    {
        GetComponentInChildren<Hurtbox>().ChangeState<HurtboxHitState>();
    }

    public void SetInvincibilityOff()
    {
        GetComponentInChildren<Hurtbox>().ChangeState<HurtboxOpenState>();
    }

    public void EnterGrab()
    {
        DisableAllHitboxes();
        ResetAnimTriggers();
        beingGrabbed = true;
        attacking = false;
        myRB.isKinematic = false;
        DisablePlayerCollision();
        myAnim.Play("EnemyGrabbed");
    }

    public void ExitGrab()
    {
        beingGrabbed = false;
        myRB.isKinematic = true;
        EnablePlayerCollision();
        myAnim.Play("Idle");
    }

    public void DuringGrab()
    {

    }
}
