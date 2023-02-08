using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;



public class Enemy_kungFu : MonoBehaviour, IEnemy, IDamagable, IStagger
{
    public GameObject hitPoof;
    public GameObject deathPoof;

    string enemyName;
    Animator myAnim;
    GameObject playerRef;
    GameObject BackPos;

    Rigidbody myRB;
    public EAID myEAID;
    GameObject Cam;

    bool canJump;
    public bool attacking;
    bool attackOrder;

    public Hitbox[] attacks;

    private Ray Above;
    RaycastHit wallCheckRight;
    RaycastHit wallCheckLeft;
    private RaycastHit PlayHit;
    public Transform EnPos;
    float TopCast = 4.35f;

    float attackInterval;
    float jumpChance;
    float jumpCheck;
    float jumpPower;
    float stopTime;
    float speed;
    float hitPoints;
    float idelTime;

    [HideInInspector] public float immuneToCarTimer = 2;
    public bool beingGrabbed = false;

    Vector3 idleLocation;
    Vector3 previousIdle;

    Text myHealthText;

    GameObject healthDisplay;
    Image[] healthMesh;
    Text enemyNameText;
    Outline enemyNameOutline;
    Color color;
    bool healthOn;

    NavMeshAgent myNavAgent;

    private void Awake()
    {
        speed = 13.5f;
        jumpPower = 500;
        jumpChance = 58;
        hitPoints = 45;
        canJump = true;
        playerRef = FindObjectOfType<PlayerPushbox>().gameObject;
        //BackPos = FindObjectOfType<backPos>
        myAnim = GetComponent<Animator>();
        myRB = GetComponent<Rigidbody>();
        healthDisplay = Instantiate(Resources.Load("Prefab/Enemy Health") as GameObject, FindObjectOfType<GP_Canvas>().transform);
        myNavAgent = GetComponent<NavMeshAgent>();

    }
    
    void Start()
    {
        Cam = FindObjectOfType<AudioDirector>().gameObject;

        enemyName = EnemyNameBasket.GenerateName();
        healthDisplay.GetComponentInChildren<Slider>().maxValue = hitPoints;
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
    }

    void Update()
    {
        if (immuneToCarTimer > 0)
        {
            immuneToCarTimer -= Time.deltaTime;
        }
        if (healthOn)
        {

            healthDisplay.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 3, 0));

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
            if (Physics.Raycast(EnPos.position, Vector3.up, out PlayHit, TopCast))
            {
                if (PlayHit.collider.tag == "Player")
                {
                    UpAttack();
                }
            }
            else if (attackOrder)
            {
                if (canJump)
                {
                    StartCoroutine(JumpMaybe());
                }
                if (Vector3.Distance(transform.position, playerRef.transform.position) > 13 && !attacking)
                {

                    MoveCloser();
                    RotateToFace();
                }
                else if (Vector3.Distance(transform.position, playerRef.transform.position) >= 6 && !attacking)
                {

                    MoveToAttack();
                    RotateToFace();
                }
                else
                {
                    AttackLogic();
                    RotateToFace();
                }
            }
        }
    }   

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

    void AttackLogic()
    {
        if (attackInterval <= 0)
        {
            int attack = Random.Range(0, 2);
            if (attack == 0)
            {
                IsAttacking();
                AttackChop();
            }
            else if (attack == 1)
            {
                IsAttacking();
                AttackKick();
            }
            else if (attack == 2)
            {
                IsAttacking();
                MoveBehind();
            }

            AttackIntervalAssign();
            
        }
        else if (!attacking)
        {
            attackInterval -= Time.deltaTime;
        }
    }

    public void IsAttacking()
    {
        attacking = false;
    }

    void AttackIntervalAssign()
    {
        attackInterval = Random.Range(2f, 4f);
    }

    void MoveToAttack()
    {
        myNavAgent.SetDestination(playerRef.transform.position);
    }

    void MoveCloser()
    {
        myNavAgent.SetDestination(playerRef.transform.position);
    }

    void PlayerInDifferetLane()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit);

        if (hit.transform.tag == "Rear Lane" || hit.transform.name == "Rear Lane")
        {
            if (WallCheckRear())
            {
                transform.position += new Vector3(0, 0, -6);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerRef.transform.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            }
        }
        else if(hit.transform.tag == "Front Lane" || hit.transform.name == "Front Lane")
        {
            if (WallCheckFront())
            {
                transform.position += new Vector3(0, 0, 6);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerRef.transform.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            }
        }
    }
    
    void MoveBehind()
    {
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3()
        AttackSmokeBomb();
    }

    void AttackChop()
    {
        int comboChance = Random.Range(1, 60); //101);
        if (comboChance >= 1)
        {
            myAnim.SetTrigger("HeadChop");
            //call funtion based off of attack
        }
    }

    void AttackKick()
    {
        int comboChance = Random.Range(1, 65); // 101);
        if (comboChance >= 1)
        {
            myAnim.SetTrigger("RoundKick");
        }

    }

    void AttackSmokeBomb()
    {
        int comboChance = Random.Range(1, 65); // 101);
        if (comboChance >= 1)
        {
            myAnim.SetTrigger("SmokeBomb");
        }
    }

    void JumpAttack()
    {
        myAnim.SetTrigger("UpAttack");
    }

    void UpAttack()
    {
        myAnim.SetTrigger("UpAttack");
        PlayHit = new RaycastHit();
    }

    void OnDeath()
    {
        myEAID.AttackRequest(gameObject);
        GetComponent<AudioPlayer>().QuickEnemyDeath();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            hitPoints -= 1;
        }
    }

    IEnumerator JumpMaybe()
    {
        canJump = false;
        yield return new WaitForSeconds(4f);
        jumpCheck = Random.Range(1, 101);
       
        if (jumpCheck <= jumpChance)

        {

            canJump = true;
        }
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
        Cam.GetComponent<AudioDirector>().EnemyHit();

        CameraFollow.CameraShake();
        hitPoints -= Mathf.RoundToInt(dmg);
        healthOn = true;
        healthDisplay.GetComponentInChildren<Slider>().value = hitPoints;
        for (int i = 0; i < healthMesh.Length; i++)
        {
            Color color;
            color = healthMesh[i].GetComponent<Image>().color;
            color.a = 1;
            healthMesh[i].GetComponent<Image>().color = color;
        }
        enemyNameText.color = new Color(enemyNameText.color.r, enemyNameText.color.g, enemyNameText.color.b, 1);
        enemyNameOutline.effectColor = new Color(enemyNameOutline.effectColor.r, enemyNameOutline.effectColor.g, enemyNameOutline.effectColor.b, 1);

        if (hitPoints <= 0)
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
        hitPoints -= damage;
        GameObject poof = Instantiate(hitPoof, transform);
        poof.transform.position += new Vector3(0, 1, -2);
        if (hitPoints <= 0)
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
        myRB.AddForce(x, 0, 0);
    }

    public void KnockBack(float x, float y)
    {
        myAnim.SetTrigger("Knockback");
        myRB.AddForce(x, y, 0);
    }

    void ClearAttacks()
    {
        foreach(Hitbox At in attacks)
        {
            At.ClearCollidedList();
        }
    }

    void DisableAllHitboxes()
    {
        foreach(Hitbox hitbox in attacks)
        {
            hitbox.gameObject.SetActive(false);
        }
    }

    public void EnterGrab()
    {
        DisableAllHitboxes();
        beingGrabbed = true;
        myAnim.Play("Grabbed");
    }

    public void ExitGrab()
    {
        beingGrabbed = false;
        myAnim.Play("Idle");
    }

    public void DuringGrab()
    {
    }
    
}
