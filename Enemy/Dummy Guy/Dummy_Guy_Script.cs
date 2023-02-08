using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy_Guy_Script : MonoBehaviour, IEnemy, IDamagable, IStagger
{
    public GameObject playerRef;
    public GameObject rangedWeapon;
    public GameObject hitPoof;
    public GameObject deathPoof;
    public Rigidbody myRB;
    public Animator myAnim;
    public EAID myEAID;

    //Hitbox[] myHitboxes;
    //Hitbox punchCombo;
    //Hitbox kneeCombo;
    //Hitbox hammerCombo;

    public bool canJump;
    public bool attacking;
    public bool attackOrder;
    public bool rangedAttacker;
    bool changeLanesRear;
    bool changeLanesFront;
    bool stagger;

    public int health;
    public int staggerIgnoreChance;

    public float staggerGroundCheck;
    float attackInterval;
    float jumpChance;
    float jumpCheck;
    float jumpPower;
    float stopTime;
    float speed;
    float idelTime;
    float staggerPause;

    Vector3 idleLocation;
    Vector3 previousIdle;
    Vector3 wallCheckVector;

    RaycastHit wallCheckRight;
    RaycastHit wallCheckLeft;

    Transform myFoot;


    public void EnterGrab()
    {
    }

    public void ExitGrab()
    {
    }

    public void DuringGrab()
    {
    }

    private void Awake()
    {
        speed = 10;
        jumpPower = 500;
        jumpChance = 70;
        health = 30;
        idelTime = 4f;
        canJump = true;
        //myHitboxes = GetComponentsInChildren<Hitbox>();
        myRB = GetComponent<Rigidbody>();
        playerRef = FindObjectOfType<PlayerPushbox>().gameObject;
        myAnim = GetComponent<Animator>();
        myFoot = transform.GetChild(2).transform;
    }

    private void Start()
    {
        //punchCombo = myHitboxes[0];
        //kneeCombo = myHitboxes[1];
        //hammerCombo = myHitboxes[2];
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, playerRef.transform.position) >= 200)
        {
            TakeDamage(1000);
        }
        if (stagger)
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

        if (attackOrder)
        {
            if (Vector3.Distance(transform.position, playerRef.transform.position) > 8 && !attacking)
            {
                MoveCloser();
                RotateToFace();
            }
            else if (Vector3.Distance(transform.position, playerRef.transform.position) >= 3 && !attacking)
            {
                MoveToAttack();
                RotateToFace();
            }
            else
            {
                AttackLogic();
            }
        }//Add to others
        //else if (rangedAttacker)
        //{
        //    //get distance to player
        //    //move closer if not in range
        //    //attack if close enough
        //    //call different attack animations
        //    if (Vector3.Distance(transform.position, playerRef.transform.position) < 20 && !attacking)
        //    {
        //        AttackRanged();
        //        if (Mathf.Abs(playerRef.transform.position.z - transform.position.z) > 3)
        //        {
        //            PlayerInDifferentLane();
        //        }
        //        RotateToFace();
        //    }
        //    else if (Vector3.Distance(transform.position, playerRef.transform.position) > 15 && !attacking)
        //    {
        //        MoveCloser();
        //        RotateToFace();
        //    }
        //}
        else
        {
            if (idleLocation != Vector3.zero)
            {
                if (transform.position.x - idleLocation.x > .5f || transform.position.x - idleLocation.x < -.5f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(idleLocation.x, transform.position.y, idleLocation.z), speed * Time.deltaTime);
                }
                else
                {
                    idelTime -= Time.deltaTime;
                    if (idelTime <= 0)
                    {
                        RaycastHit hit;
                        Physics.Raycast(transform.position, Vector3.down, out hit);
                        Debug.DrawRay(transform.position, Vector3.down, Color.red, 1000.0f);

                        if (hit.transform.tag == "Rear Lane" || hit.transform.name == "Rear Lane")
                        {
                            int doLaneSwap = Random.Range(1, 101);
                            if (doLaneSwap > 75)
                            {
                                if (WallCheckRear())
                                {
                                    transform.position += new Vector3(0, 0, -6);
                                }
                                else
                                {
                                    previousIdle = idleLocation;
                                }
                            }
                            else
                            {
                                previousIdle = idleLocation;
                            }
                        }
                        else if (hit.transform.tag == "Front Lane" || hit.transform.name == "Front Lane")
                        {
                            int doLaneSwap = Random.Range(1, 101);
                            if (doLaneSwap > 75)
                            {
                                if (WallCheckFront())
                                {
                                    transform.position += new Vector3(0, 0, 6);
                                }
                                else
                                {
                                    previousIdle = idleLocation;
                                }
                            }
                            else
                            {
                                previousIdle = idleLocation;
                            }
                        }
                        RaycastHit checkForPlayer;
                        Physics.Raycast(transform.position, (idleLocation - transform.position), out checkForPlayer, Vector3.Distance(transform.position, idleLocation));
                        if (checkForPlayer.collider != null && checkForPlayer.transform.gameObject.tag == "Player")
                        {
                            idleLocation = previousIdle;
                        }
                        idelTime = Random.Range(1f, 4f);
                    }
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
            wallCheckVector = Vector3.forward;
        }
        else if (transform.position.x - playerRef.transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            wallCheckVector = Vector3.back;
        }
    }

    void AttackLogic()
    {
        if (attackInterval <= 0)
        {
            int attack = Random.Range(0, 3);
            //myAnim.SetTrigger("Prep Attack");
            if (attack == 0)
            {
                attacking = true;
                RotateToFace();
                StartCoroutine(AttackReturn());
            }
        }
        else
        {
            attackInterval -= Time.deltaTime;
        }
    }

    IEnumerator AttackReturn()
    {
        yield return new WaitForSeconds(3.0f);
        attacking = false;
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
        
    }

    void MoveCloser()
    {
        
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
        
    }

    void Attack2()
    {
        
    }

    void Attack3()
    {
        
    }

    void AttackRanged()
    {
        
    }
    

    void OnDeath()
    {
        myEAID.AttackRequest(gameObject);
        Destroy(gameObject);
    }

    void StaggerIgnore()
    {
        if (staggerIgnoreChance == 0)
        {
            staggerIgnoreChance = 25;
        }
        if (staggerIgnoreChance > Random.Range(1, 101))
        {
            myAnim.SetTrigger("StaggerIgnoreAttack");
            staggerIgnoreChance = 0;
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
        if (health <= 0)
        {
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
        CameraFollow.CameraShake();
        health -= Mathf.RoundToInt(damage);
        if (health <= 0)
        {
            Instantiate(deathPoof, transform.position, Quaternion.Euler(Vector3.zero));
            OnDeath();
        }
        else
        {
            GameObject poof = Instantiate(hitPoof, transform);
            poof.transform.position += new Vector3(0, 1, -2);
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
        if (myRB.isKinematic)
        {
            myRB.isKinematic = false;
            stagger = true;
        }

        staggerPause = 1;
        myRB.AddForce(x, 0, 0);
    }

    public void KnockBack(float x, float y)
    {
        myAnim.SetTrigger("Knockback");
        myRB.AddForce(x, y, 0);
    }

    //public void DisableAllHitboxes()
    //{
    //    foreach (Hitbox hitbox in myHitboxes)
    //    {
    //        hitbox.ClearCollidedList();
    //        hitbox.gameObject.SetActive(false);
    //    }
    //}

    public void StahpVelocity()
    {
        myRB.velocity = Vector3.zero;
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<SuperSuperParent>() && !CheckIfStagger())
    //    {
    //        myRB.velocity = new Vector3(0, myRB.velocity.y, 0);
    //    }
    //}

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
    }

    public void Falling()
    {
        RaycastHit rayHit;
        Debug.DrawRay(myFoot.position, Vector3.down, Color.blue, 0.2f);
        if (Physics.Raycast(myFoot.position, Vector3.down, out rayHit, 0.2f) && rayHit.collider.transform.GetComponent<Ground>())
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
}
