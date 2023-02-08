using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy_Bruiser : MonoBehaviour, IEnemy, IDamagable, IStagger
{
    public GameObject hitPoof;
    public GameObject deathPoof;

    GameObject playerRef;
    Animator myAnim;
    CapsuleCollider myCollider;
    GameObject Cam;
    Component useAudio;

    string enemyName;
    bool canJump;
    bool arialAttack;
    public bool attacking;
    public bool Blocking;
    public bool Countering;
    [HideInInspector] public float immuneToCarTimer = 2;
    bool immuneToStaggerKnockback = false;
    bool beingGrabbed = false;
    public bool jumping = false;
    bool changeLanesRear;
    bool changeLanesFront;

    public Hitbox[] attacks;

    Coroutine isJumping;
    Coroutine pain;
    Coroutine Defense;
    float moveRange;
    float atkRange;
    float jumpRange;
    [HideInInspector] public bool knockback = false;

    bool attackOrder;

    public float attackInterval;
    float jumpChance;
    float jumpCheck;
    float jumpPower;
    float stopTime;
    float speed;
    public float health;
    private float prevHP;

    float Zpos;
    [SerializeField]
    float blockedAttacks;

    Vector3 idelLocation;
    EAID myEAID;
    Rigidbody myRB;
    public float idelTime;

    Vector3 idleLocation;
    Vector3 previousIdle;
    Vector3 wallCheckVector;
    RaycastHit wallCheckRight;
    RaycastHit wallCheckLeft;

    public GameObject[] bodyparts;
    public GameObject myFoot;
    [HideInInspector]public Color32 baseColor = new Color32(255, 129, 0, 255);
    Color32 hurtColor = new Color32(255, 20, 0, 255);

    GameObject healthDisplay;
    Image[] healthMesh;
    Text enemyNameText;
    Outline enemyNameOutline;
    Color color;
    bool healthOn;

    NavMeshAgent myNavAgent;

    private void Awake()
    {
        attacks = GetComponentsInChildren<Hitbox>();
        speed = 5;
        jumpPower = 1200;
        jumpChance = 70;
        health = 60;

        moveRange = 10;
        atkRange = 4.5f;
        jumpRange = 11;

        idelTime = 4f;
        blockedAttacks = 0;

        prevHP = health;
        canJump = true;
        playerRef = FindObjectOfType<PlayerPushbox>().gameObject;
        myAnim = GetComponent<Animator>();
        myRB = GetComponent<Rigidbody>();
        myCollider = GetComponentInChildren<CapsuleCollider>();
        healthDisplay = Instantiate(Resources.Load("Prefab/Enemy Health") as GameObject, FindObjectOfType<GP_Canvas>().transform);
        myNavAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        enemyName = EnemyNameBasket.GenerateName();
        Cam = FindObjectOfType<AudioDirector>().gameObject;
        useAudio = GetComponent<AudioDirector>();
        healthDisplay.GetComponentInChildren<Slider>().maxValue = health;
        healthDisplay.GetComponentInChildren<Slider>().minValue = 0;
        enemyNameText = healthDisplay.GetComponentInChildren<Text>();
        enemyNameText.text = enemyName;
        enemyNameOutline = healthDisplay.GetComponentInChildren<Outline>();
        healthMesh = healthDisplay.GetComponentsInChildren<Image>();
        for (int i = 0; i < healthMesh.Length; i++)
        {
            Color color;
            color = healthMesh[i].GetComponent<Image>().color;
            color.a = 0;
            healthMesh[i].GetComponent<Image>().color = color;
        }
        enemyNameText.color = new Color(enemyNameText.color.r, enemyNameText.color.g, enemyNameText.color.b, 0);
        enemyNameOutline.effectColor = new Color(enemyNameOutline.effectColor.r, enemyNameOutline.effectColor.g, enemyNameOutline.effectColor.b, 0);
        Zpos = transform.position.z;
    }

    //Changes color back to the bruiser's original color
    public void ColorOff()
    {
        foreach (GameObject bodypart in bodyparts)
            bodypart.GetComponent<Renderer>().material.color = baseColor;
    }

    //Changes color of the bruiser to blue to represent that it is in the blocking state
    public void ColorOn()
    {
        foreach (GameObject bodypart in bodyparts)
            bodypart.GetComponent<Renderer>().material.color = Color.blue;
    }

    //Changes the bruiser's color to grey to represent that it is being staggered
    public void ColorGrey()
    {
        foreach(GameObject bodypart in bodyparts)
        {
            bodypart.GetComponent<Renderer>().material.color = new Color(0.58491f, 0.58491f, 0.58491f);
        }
    }

    public void AttackTell()
    {
        foreach (GameObject bodypart in bodyparts)
        {
            bodypart.GetComponent<Renderer>().material.color = Color.yellow;
        }
    }
    

    public void Idle()
    {

        if (Vector3.Distance(transform.position, playerRef.transform.position) >= 200)
        {
            TakeDamage(100000000);
        }

        if (attacking)
        {
            myNavAgent.isStopped = true;
        }
        if (attackOrder)
        {
            //Move to player
            if (Vector3.Distance(transform.position, playerRef.transform.position) > moveRange && attacking == false)
            {

                MoveCloser();
                RotateToFace();
            }
            //Attacks the player
            else if (Vector3.Distance(transform.position, playerRef.transform.position) <= atkRange)
            {
                AttackLogic();
                RotateToFace();
            }
            //Moves in for attack on player
            else if (transform.position.x - playerRef.transform.position.x <= moveRange ||
                     transform.position.x - playerRef.transform.position.x >= -moveRange &&
                     attacking == false)
            {
                

                MoveToAttack();
                RotateToFace();

                //Jumps to player
                if (playerRef.transform.position.y - transform.position.y > -2.5f &&
                    (transform.position.x - playerRef.transform.position.x <= jumpRange ||
                    transform.position.x - playerRef.transform.position.x >= -jumpRange) &&
                    attacking == false &&
                    canJump)
                {
                    //if (isJumping == null)
                    isJumping = StartCoroutine(JumpMaybe());
                    canJump = false;
                }
                

            }

            //Attacks when player is close to them while in the air and on a similar y plane
            if (playerRef.transform.position.y - transform.position.y <= 1 &&
                     playerRef.transform.position.y - transform.position.y >= -6 &&
                    (transform.position.x - playerRef.transform.position.x <= atkRange &&
                     transform.position.x - playerRef.transform.position.x >= -atkRange) && !canJump && !arialAttack)
            {
                StartCoroutine(AerialAttack());
            }
        }
    }    

    // Update is called once per frame
    void Update()
    {
        if(immuneToCarTimer > 0)
        {
            immuneToCarTimer -= Time.deltaTime;
        }
        if (jumping)
        {
            RaycastHit rayHit;
            Debug.DrawRay(myFoot.transform.position, Vector3.down, Color.blue, .35f);
            if (Physics.Raycast(myFoot.transform.position, Vector3.down, out rayHit, 0.35f) && rayHit.collider.transform.GetComponent<Ground>())
            {
                jumping = false;
                canJump = true;
                EnablePlayerCollision();
            }

        }
        if (healthOn)
        {
            healthDisplay.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 5, 0));

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
        Health();
        CounterAttack();

        if (!attacking && !beingGrabbed)
        {
            attackInterval -= Time.deltaTime;
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

    //Checks if there is an object blocking the rear lane for a transition from front to back lane
    bool WallCheckRear()
    {

        wallCheckLeft = new RaycastHit();
        Ray wallCheckRayRight = new Ray(transform.position, Vector3.back);
        Physics.Raycast(wallCheckRayRight, out wallCheckLeft, 20f);
        Debug.DrawRay(transform.position, Vector3.back, Color.blue, 4f);

        if (wallCheckLeft.collider != null)
        {
            if (wallCheckLeft.collider.GetComponent<Undashable>())
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

    //Checks the if there is an object blocking the front lane for a transition from back to front lane
    bool WallCheckFront()
    {
        wallCheckRight = new RaycastHit();
        Ray wallCheckRayRight = new Ray(transform.position, Vector3.forward);
        Physics.Raycast(wallCheckRayRight, out wallCheckRight, 20f);
        Debug.DrawRay(transform.position, Vector3.forward, Color.blue, 4f);

        if (wallCheckRight.collider != null)
        {
            if (wallCheckRight.collider.GetComponent<Undashable>())
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

    void ActivateHitbox(Hitbox temp)
    {
        temp.gameObject.SetActive(true);
    }

    void DeactivateHitbox(Hitbox temp)
    {
        temp.gameObject.SetActive(false);
    }

    public void ActivatePunch()
    {
        ActivateHitbox(attacks[0]);
    }

    public void DeactivatePunch()
    {
        DeactivateHitbox(attacks[0]);
    }

    public void ActivateKick()
    {
        ActivateHitbox(attacks[1]);
    }

    public void DeactivateKick()
    {
        DeactivateHitbox(attacks[1]);
    }

    public void ActivateOtherThing()
    {
        ActivateHitbox(attacks[2]);
    }

    public void DeactivateOtherThing()
    {
        DeactivateHitbox(attacks[2]);
    }

    // Sets the Hitboxes inactive after a cooresponding animation finishes
    public void DeactivateAllHitboxes()
    {
        foreach(Hitbox farts in attacks)
        {
            farts.ClearCollidedList();
            farts.gameObject.SetActive(false);
        }
    }

    //Rotates the bruiser towards the player's position
    void RotateToFace()
    {
        if (transform.position.x - playerRef.transform.position.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }

        else if (transform.position.x - playerRef.transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    //Randomly selects an attack or defensive action through a rng
    void AttackLogic()
    {
        if (attackInterval <= 0)
        {
            int attack = Random.Range(1, 18);
            attacking = true;
     
            if (attack >= 1 && attack <= 6)
            {
                Attack1();
            }

            else if (attack > 6 && attack <= 10)
            {
                Attack2();
            }

            else if (attack >= 11 && attack < 14)
            {
                Attack3();
            }
     
            else if (attack >= 14 && attack <= 18)
            {
                Blocking = true;
                immuneToStaggerKnockback = true;
                Block();
            }
     
            AttackIntervalAssign();
        }
    }

    public void IsAttacking()
    {
        attacking = !attacking;
    }

    //The range of time in which the bruiser can make another action
    void AttackIntervalAssign()
    {
        attackInterval = Random.Range(1.0f, 1.6f);
    }

    //Slows the brusiser down by half and moves it in close for an attack
    void MoveToAttack()
    {
        myNavAgent.isStopped = false;
        myNavAgent.SetDestination(playerRef.transform.position);
        //stopTime -= Time.deltaTime;

        //if (stopTime >= 0)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerRef.transform.position.x, transform.position.y, transform.position.z), (speed / 2) * Time.deltaTime);
        //}
        //else
        //{
        //    if (stopTime <= -1)
        //    {
        //        stopTime = Random.Range(1, 3);
        //    }
        //}
    }

    IEnumerator AerialAttack()
    {
        if (attackInterval <= 0)
        {
            arialAttack = true;
            myAnim.SetTrigger("QuickAttack");
            Cam.GetComponent<AudioDirector>().EnemyAttack();
            AttackIntervalAssign();
            yield return new WaitForSeconds(1f);
            arialAttack = false;
        }
    }

    IEnumerator JumpMaybe()
    {
        if (myRB.isKinematic == true)
        {
            DisablePlayerCollision();
        }

        myRB.AddForce(Vector3.up * jumpPower);
        Cam.GetComponent<AudioDirector>().EnemyJump();
        yield return new WaitForSeconds(0.2f);
        jumping = true;
    }

        //Moves the bruiser at faster speed to get in close to the player
    void MoveCloser()
    {
        myNavAgent.isStopped = false;
        myNavAgent.SetDestination(playerRef.transform.position);

        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerRef.transform.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
    }

    void Attack1()
    {
        StartCoroutine(QuickAttack());
    }

    IEnumerator QuickAttack()
    {
        Countering = true;
        AttackTell();
        yield return new WaitForSeconds(0.35f);
        ColorOff();
        Cam.GetComponent<AudioDirector>().EnemyAttack();
        Cam.GetComponent<AudioDirector>().BruiserAttack();
        myAnim.SetTrigger("QuickAttack");
        yield return new WaitForSeconds(0.35f);
        attacking = false;
        Countering = false;
    }

    void Attack2()
    {
        StartCoroutine(StrongAttack());
    }

    IEnumerator StrongAttack()
    {
        Countering = true;
        AttackTell();
        Cam.GetComponent<AudioDirector>().BruiserWindUp();
        yield return new WaitForSeconds(1.25f);
        ColorOff();
        Cam.GetComponent<AudioDirector>().EnemyAttack();
        Cam.GetComponent<AudioDirector>().BruiserAttack();
        myAnim.SetTrigger("StrongAttack");
        yield return new WaitForSeconds(.65f);
        attacking = false;
        Countering = false;
    }

    void Attack3()
    {
        StartCoroutine(KickAttack());
    }

    IEnumerator KickAttack()
    {
        Countering = true;
        AttackTell();
        yield return new WaitForSeconds(0.7f);
        ColorOff();
        Cam.GetComponent<AudioDirector>().EnemyAttack();
        Cam.GetComponent<AudioDirector>().BruiserAttack();
        myAnim.SetTrigger("KickAttack");
        yield return new WaitForSeconds(.42f);
        attacking = false;
        Countering = false;
    }

    void TookDamage()
    {
        if(pain == null)
        {
            pain = StartCoroutine(Pain());
        }

        else
        {
            StopCoroutine(pain);
            pain = StartCoroutine(Pain());
        }
    }

    //Timer to restrict the damage feedback freequency
    IEnumerator Pain()
    {
        if (!knockback)
        {
            foreach (GameObject bodypart in bodyparts)
                bodypart.GetComponent<Renderer>().material.color = hurtColor;
        }

        else { yield return null; }
        yield return new WaitForSeconds(0.3f);

        if (!knockback)
        {
            foreach (GameObject bodypart in bodyparts)
                bodypart.GetComponent<Renderer>().material.color = baseColor;
        }
    }

    void Block()
    {
        myAnim.SetTrigger("OldSpice");
        Defense = StartCoroutine(BlockDuration());
    }

    IEnumerator BlockDuration()
    {
        ColorOn();
        yield return new WaitForSeconds(.65f);
        ColorOff();
        immuneToStaggerKnockback = false;
        Blocking = false;
        attacking = false;
        blockedAttacks = 0;
    }

    void Defensive()
    {
        if (Blocking)
        {
            if (Defense != null)
            {
                StopCoroutine(Defense);
            }

            Cam.GetComponent<AudioDirector>().EnemyBlock();
            Defense = StartCoroutine(BlockDuration());
            blockedAttacks++;  
        }
    }

    void CounterAttack()
    {
        if (blockedAttacks >= 4)
        {
            int counterAttack = Random.Range(1, 4);
            attacking = true;

            if (counterAttack == 1)
            {
                Attack1();
                blockedAttacks = 0;
            }
            else if (counterAttack == 2)
            {
                Attack2();
                blockedAttacks = 0;
            }
            else
            {
                Attack3();
                blockedAttacks = 0;
            }
        }

        else if (Countering)
        {
            blockedAttacks = 0;
        }
    }

    //Checks to see if damage was received
    void Health()
    {
        if (health < prevHP)
        {
            TookDamage();
            Cam.GetComponent<AudioDirector>().EnemyHit();
            prevHP = health;         
        }
    }

    void OnDeath()
    {
        if(myEAID != null)
        {
            myEAID.AttackRequest(gameObject);
        }

        Cam.GetComponent<AudioDirector>().BruiserDeath();
        Destroy(gameObject);
    }

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

        Defensive();

        if (!Blocking)
        {
            CameraFollow.CameraShake();
            health -= Mathf.RoundToInt(dmg);
            healthOn = true;
            healthDisplay.GetComponentInChildren<Slider>().value = health;
            for (int i = 0; i < healthMesh.Length; i++)
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

    }

    public void KnockBack(float x, float y)
    {
        if (!Blocking || !immuneToStaggerKnockback)
        {
            if (myRB.isKinematic == true)
            {
                DisablePlayerCollision();
            }

            myAnim.SetTrigger("Knockback");
            myRB.AddForce(x, y, 0);
        } 
    }

    public void ResetStaggerKnockbackTriggers()
    {
        myAnim.ResetTrigger("Stagger");
        myAnim.ResetTrigger("Knockback");
        myAnim.ResetTrigger("GroundHit");
    }

    public void ClearHitbox()
    {
        foreach (Hitbox h in attacks)
        {
            h.ClearCollidedList();
        }
    }

    float timer;

    public void WhileKnockback()
    {
        RaycastHit rayHit;
        Debug.DrawRay(myFoot.transform.position, Vector3.down, Color.blue, .15f);
        timer += Time.deltaTime;

        if (Physics.Raycast(myFoot.transform.position, Vector3.down, out rayHit, 0.15f) && rayHit.collider.transform.GetComponent<Ground>() || timer >= 1)
        {
            myAnim.SetTrigger("GroundHit");
            timer = 0;
        }
    }

    public void DisablePlayerCollision()
    {
        myRB.isKinematic = false;
        Physics.IgnoreCollision(myCollider, FindObjectOfType<PlayerPushbox>().GetComponent<BoxCollider>(), true);
    }

    public void EnablePlayerCollision()
    {
        myRB.isKinematic = true;
        Physics.IgnoreCollision(myCollider, FindObjectOfType<PlayerPushbox>().GetComponent<BoxCollider>(), false);
    }

    public void EnterGrab()
    {
        DeactivateAllHitboxes();
        beingGrabbed = true;
        attacking = false;
        DisablePlayerCollision();
        myAnim.Play("EnemyGrabbed");
    }

    public void ExitGrab()
    {
        beingGrabbed = false;
        EnablePlayerCollision();
        myAnim.Play("Idle");
    }

    public void DuringGrab()
    {

    }
}

