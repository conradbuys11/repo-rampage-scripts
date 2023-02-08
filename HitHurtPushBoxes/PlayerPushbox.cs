using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPushbox : MonoBehaviour, IDamagable, IFireStatus<float>, IFrostStatus<float>, ILightningStatus<float>, IStagger
{

    //Different attacks & their hitboxes.
    [Header("Hitboxes & Attached Components")]
    [SerializeField] public Hurtbox myHurtbox;
    Hitbox[] myLightHitboxes;
    Hitbox[] myHeavyHitboxes;
    Hitbox[] mySpecialHitboxes;
    Hitbox[] allMyHitboxes;
    [SerializeField] public Animator myAnimator;
    public Rigidbody myRB;
    public GameObject hadouken;
    public GameObject super;
    public AudioPlayer myAudio;
    float gravityEnhancer = 0;
    bool inBackground = false;
    bool allowedToDoThings = true;
    bool rightTriggerPressed = false;
    bool invincibilityFrames = false;
    bool immuneToStaggerKnockback = false;
    bool grabAttack = false;
    GameObject thingIGrabbed;

    Coroutine bladestorm;
    Coroutine stagger;
    Coroutine coDot;
    Coroutine coSlow;
    Coroutine coStun;

    bool abilitySet1 = true;
    public AbilityBase ability1;
    public AbilityBase ability2;
    public AbilityBase ability3;

    Ability_Hadouken abilityHadouken = new Ability_Hadouken();
    Ability_Falcon abilityFalcon = new Ability_Falcon();
    Ability_Tipper abilityTipper = new Ability_Tipper();
    Ability_Bladestorm abilityBladestorm = new Ability_Bladestorm();
    Ability_Dash abilityDash = new Ability_Dash();
    Ability_Grab abilityGrab = new Ability_Grab();
    //WHEN ADDING A NEW ABILITY, ADD IT TO ALL ABILITIES
    public AbilityBase[] allAbilities;

    [Header("Player Ability Stats")]
    public bool canUseAbility1 = true;
    public float ability1Timer = 0;
    public bool canUseAbility2 = true;
    public float ability2Timer = 0;
    public bool canUseAbility3 = true;
    public float ability3Timer = 0;

    float rollCooldown = 0;
    bool canRoll = true;

    [Header("Player Move Stats")]
    //public float moveVertical;
    public float playerDashVelocity = 60;
    public float moveHorizontal;
    public float speed;
    public float maxSpeed;
    float stopSpeed;
    public float recoilTime;
    Vector3 moveDirection;

    [Header("Some leveling stats.")]
    public float myHP;
    public float maxHP;
    public int money;
    public int level;
    public int myStr;
    public int myDef;
    public int mySpe;
    public int exp;
    public float nextXPLevel;
    public int gameLevel;
    public int checkpointOn;

    PlayerStatus stopMove;
    public bool isAlive;
    public bool isFalling = false;

    public bool bossFreeze = false;

    public bool isArmed;

    public void AllowInput(bool temp)
    {
        allowedToDoThings = temp;
    }

    public void Awake()
    {       
        myHurtbox = GetComponentInChildren<Hurtbox>();
        allMyHitboxes = GetComponentsInChildren<Hitbox>();
        myAnimator = GetComponentInChildren<Animator>();
        myRB = GetComponentInParent<Rigidbody>();
        myAudio = GetComponent<AudioPlayer>();

        ability1 = abilityHadouken;
        ability2 = abilityDash;
        ability3 = abilityGrab;

        stopMove = FindObjectOfType<PlayerStatus>();
        isAlive = true;
    }

    private void Start()
    {
        allAbilities = new AbilityBase[] { abilityBladestorm, abilityTipper, abilityDash, abilityHadouken, abilityFalcon, abilityGrab };
        myLightHitboxes = GetComponentInChildren<LightHitboxes>().GetHitboxes();
        myHeavyHitboxes = GetComponentInChildren<HeavyHitboxes>().GetHitboxes();
        mySpecialHitboxes = GetComponentInChildren<SpecialHitboxes>().GetHitboxes();
    }

    private void Update()
    {
        if (allowedToDoThings)
        {
            foreach (AbilityBase ability in allAbilities)
            {
                if (ability.GetCurrentCooldown() > 0)
                {
                    ability.DecrementCooldown(Time.deltaTime);
                }
            }

            if (Input.GetAxisRaw("Right Trigger") == 0 && rightTriggerPressed == true)
                rightTriggerPressed = false;

            if (!canRoll)
            {
                rollCooldown -= Time.deltaTime;
                if (rollCooldown <= 0)
                {
                    canRoll = true;
                    rollCooldown = 0;
                }
            }

            if (!canUseAbility1)
            {
                if (ability1.GetCurrentCooldown() <= 0) //cooldown
                {
                    //Sound effect?
                    canUseAbility1 = true;
                    ability1.SetCooldownTo0();
                }
            }
            if (!canUseAbility2)
            {
                if (ability2.GetCurrentCooldown() <= 0) //cooldown
                {
                    //Sound effect?
                    canUseAbility2 = true;
                    ability2.SetCooldownTo0();
                }
            }
            if (!canUseAbility3)
            {
                if (ability3.GetCurrentCooldown() <= 0) //cooldown
                {
                    //Sound effect?
                    canUseAbility3 = true;
                    ability3.SetCooldownTo0();
                }
            }
        }
        //if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton6))
        //{
        //    if (abilitySet1)
        //    {
        //        ability1 = abilityFalcon;
        //        if (ability1.GetCurrentCooldown() <= 0) //cooldown
        //        {
        //            canUseAbility1 = true;
        //            ability1.SetCooldownTo0();
        //        }
        //        else
        //        {
        //            canUseAbility1 = false;
        //        }
        //        ability3 = abilityTipper;
        //        if (ability3.GetCurrentCooldown() <= 0) //cooldown
        //        {
        //            canUseAbility3 = true;
        //            ability3.SetCooldownTo0();
        //        }
        //        else
        //        {
        //            canUseAbility3 = false;
        //        }
        //        abilitySet1 = false;
        //    }
        //    else
        //    {
        //        ability1 = abilityHadouken;
        //        if (ability1.GetCurrentCooldown() <= 0) //cooldown
        //        {
        //            canUseAbility1 = true;
        //            ability1.SetCooldownTo0();
        //        }
        //        else
        //        {
        //            canUseAbility1 = false;
        //        }
        //        ability3 = abilityBladestorm;
        //        if (ability3.GetCurrentCooldown() <= 0) //cooldown
        //        {
        //            canUseAbility3 = true;
        //            ability3.SetCooldownTo0();
        //        }
        //        else
        //        {
        //            canUseAbility3 = false;
        //        }
        //        abilitySet1 = true;
        //    }
        //}
        if(gravityEnhancer > 75)
        {
            gravityEnhancer = 75;
        }
    }

    public void ResetGravityEnhancer()
    {
        gravityEnhancer = 0;
    }

    public void ChangeHP(float change)
    {
        myHP += change;
    }

    public float GetHP()
    {
        return myHP;
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
        for(int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(duration / 3);
            TakeDamage(damage / 4);
        }
    }

    public void TakeSlow(float percentage, float duration)
    {
        if (coSlow != null)
        {
            StopCoroutine(coSlow);
        }
        coDot = StartCoroutine(FrostSlow(percentage, duration));
    }

    public IEnumerator FrostSlow(float slowPercentage, float duration)
    {
        speed *= slowPercentage;
        yield return new WaitForSeconds(duration);
        speed = maxSpeed;
    }

    public void TakeStun(float duration)
    {
        if (coStun != null)
        {
            StopCoroutine(coStun);
        }
        coStun = StartCoroutine(LightningStun(duration));
    }

    public IEnumerator LightningStun(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    void SmallDashForward()
    {
        if (transform.rotation.eulerAngles.y == 90)
        {
            myRB.velocity = new Vector3(20, 0, 0);
        }
        else
        {
            myRB.velocity = new Vector3(-20, 0, 0);
        }
    }

    public void OpenLightHitbox()
    {
        SmallDashForward();
        EnableHitbox(myLightHitboxes[0]);
    }

    public void CloseLightHitbox()
    {
        DisableHitbox(myLightHitboxes[0]);
        myRB.velocity = Vector3.zero;
    }

    public void OpenLightHitbox2()
    {
        SmallDashForward();
        EnableHitbox(myLightHitboxes[1]);
    }

    public void CloseLightHitbox2()
    {
        DisableHitbox(myLightHitboxes[1]);
        myRB.velocity = Vector3.zero;
    }

    public void OpenLightHitbox3()
    {
        SmallDashForward();
        EnableHitbox(myLightHitboxes[2]);
    }

    public void CloseLightHitbox3()
    {
        DisableHitbox(myLightHitboxes[2]);
        myRB.velocity = Vector3.zero;
    }

    public void OpenLightHitbox4()
    {
        SmallDashForward();
        EnableHitbox(myLightHitboxes[3]);
    }

    public void OpenLightHitbox5()
    {
        SmallDashForward();
        EnableHitbox(myLightHitboxes[4]);
    }

    public void CloseLightHitbox4()
    {
        DisableHitbox(myLightHitboxes[3]);
        myRB.velocity = Vector3.zero;
    }

    public void CloseLightHitbox5()
    {
        DisableHitbox(myLightHitboxes[4]);
        myRB.velocity = Vector3.zero;
    }

    public void OpenMidairLightHitbox1()
    {
        EnableHitbox(myLightHitboxes[5]);
    }

    public void CloseMidairLightHitbox1()
    {
        DisableHitbox(myLightHitboxes[5]);
    }

    public void OpenMidairLightHitbox2()
    {
        EnableHitbox(myLightHitboxes[6]);
    }

    public void CloseMidairLightHitbox2()
    {
        DisableHitbox(myLightHitboxes[6]);
    }

    public void OpenStrongHitbox()
    {
        SmallDashForward();
        EnableHitbox(myHeavyHitboxes[0]);
    }

    public void CloseStrongHitbox()
    {
        DisableHitbox(myHeavyHitboxes[0]);
        myRB.velocity = Vector3.zero;
    }

    public void OpenMidairStrongHitbox1()
    {
        EnableHitbox(myHeavyHitboxes[1]);
    }

    public void CloseMidairStrongHitbox1()
    {
        DisableHitbox(myHeavyHitboxes[1]);
    }

    public void OpenStrongComboHitbox()
    {
        SmallDashForward();
        EnableHitbox(myHeavyHitboxes[2]);
    }

    public void CloseStrongComboHitbox()
    {
        DisableHitbox(myHeavyHitboxes[2]);
        myRB.velocity = Vector3.zero;
    }

    public void SpawnHadouken()
    {
        myAudio.HadoukenSound();
        if (transform.rotation.eulerAngles.y == 90)
        {
            GameObject temp = Instantiate(hadouken, transform.parent.transform.position + new Vector3(2, 3, 0), Quaternion.identity);
        }
        else if (transform.rotation.eulerAngles.y >= -90)
        {
            GameObject temp = Instantiate(hadouken, transform.parent.transform.position + new Vector3(-2, 3, 0), Quaternion.identity);
            temp.GetComponent<Hadouken>().OtherWay();
        }
    }

    public void OpenTipperHitbox()
    {
        mySpecialHitboxes[0].gameObject.SetActive(true);
        mySpecialHitboxes[1].gameObject.SetActive(true);
    }

    public void CloseTipperHitbox()
    {
        mySpecialHitboxes[0].ClearCollidedList();
        mySpecialHitboxes[0].gameObject.SetActive(false);
        mySpecialHitboxes[1].ClearCollidedList();
        mySpecialHitboxes[1].gameObject.SetActive(false);
    }

    public void OpenBladestormHitbox()
    {
        EnableHitbox(mySpecialHitboxes[2]);
    }

    public void CloseBladestormHitbox()
    {
        DisableHitbox(mySpecialHitboxes[2]);
    }

    public void OpenFalconPunchHitbox()
    {
        SmallDashForward();
        EnableHitbox(mySpecialHitboxes[3]);
    }

    public void CloseFalconPunchHitbox()
    {
        DisableHitbox(mySpecialHitboxes[3]);
        myRB.velocity = Vector3.zero;
    }

    public void OpenDashHitbox()
    {
        EnableHitbox(mySpecialHitboxes[4]);
    }

    public void CloseDashHitbox()
    {
        DisableHitbox(mySpecialHitboxes[4]);
    }

    public void OpenGrabHitbox()
    {
        EnableHitbox(mySpecialHitboxes[5]);
    }

    public void CloseGrabHitbox()
    {
        DisableHitbox(mySpecialHitboxes[5]);
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

    public void DisableAllHitboxes()
    {
        foreach(Hitbox hitbox in allMyHitboxes)
        {
            hitbox.ClearCollidedList();
            hitbox.gameObject.SetActive(false);
        }
    }

    public void GetHitBy(float damage)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        if (!invincibilityFrames)
        {
            myAudio.GetComponent<AudioPlayer>().Thwap();
            FindObjectOfType<PlayerStatus>().TakeDamage(damage);
        }
    }

    public void TakeEnviroDamage(int damage)
    {
        
    }

    public int GivePoints(float mod)
    {
        return 0;
    }
    

    //public void Block()
    //{
    //    if (!myHurtbox.IsCurrentState<HurtboxHitState>() && !myHurtbox.IsNextState<HurtboxHitState>())
    //    {
    //        myHurtbox.ChangeState<HurtboxHitState>();
    //    }
    //    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
    //    {
    //        myAnimator.SetBool("Is_Block", false);
    //    }
    //    //Did you mean to have one here too?
    //    //if (!Input.GetKey(KeyCode.RightShift))
    //    //{
    //    //    myAnimator.SetBool("Is_Block", false);
    //    //    ChangeState<CharacterIdleState>();
    //    //}
    //}

    public void CreateSave()
    {
        Save tempSave = new Save(); //set every save data point based on its value in this script
        tempSave.maxHP = maxHP;
        tempSave.curHP = myHP;
        tempSave.exp = exp;
        tempSave.money = money;
        tempSave.strength = myStr;
        tempSave.defense = myDef;
        tempSave.speed = mySpe;
        tempSave.nextXPLevel = nextXPLevel;
        tempSave.gameLevel = gameLevel;
        tempSave.checkpoint = checkpointOn;
        //Vector3 temp = FindObjectOfType<CharacterControllerNew>().transform.position;
        //tempSave.position = new Vector3Serialze(temp.x, temp.y, temp.z);
        SaveSystem.SaveGame(tempSave);
    }

    public void SetInvincibility(bool b)
    {
        immuneToStaggerKnockback = b;
        invincibilityFrames = b;
    }

    public void PlayerMovementControl()
    {
        if (bossFreeze == false)
        {
            if (isAlive && allowedToDoThings)
            {
                //Trae's movement code
                moveHorizontal = Input.GetAxisRaw("Horizontal");
                //moveVertical = Input.GetAxisRaw("Vertical");
                if(moveHorizontal > 0.5)
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    myAnimator.SetBool("Is_Move", true);
                    moveDirection = new Vector3(moveHorizontal, myRB.velocity.y, 0).normalized;
                }
                else if(moveHorizontal < -0.5)
                {
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    myAnimator.SetBool("Is_Move", true);
                    moveDirection = new Vector3(moveHorizontal, myRB.velocity.y, 0).normalized;
                }
                else
                {
                    myAnimator.SetBool("Is_Move", false);
                    moveDirection = new Vector3(0, myRB.velocity.y, 0).normalized;
                }
                myRB.velocity = moveDirection * speed;
            }
        }
    }

    public void Idle()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton9) || (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.N)))
        {
            List<GameObject> enemies = new List<GameObject>();
            EAID[] eaids = FindObjectsOfType<EAID>();
            for(int i = 0; i < eaids.Length; i++)
            {
                for(int q = 0; q < eaids[i].spawnedEnemies.Length; q++)
                {
                    if(eaids[i].spawnedEnemies[q] != null)
                        enemies.Add(eaids[i].spawnedEnemies[q]);
                }
            }
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponentInChildren<IDamagable>().TakeDamage(1000);
            }
            enemies.Clear();
        }

        if (allowedToDoThings)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (Time.timeScale <= .3f)
                {
                    Time.timeScale = 1;
                }
                else
                {
                    Time.timeScale = .2f;
                }
            }

            if (GetComponentInParent<SuperSuperParent>().CheckBelow())
            {
                myAnimator.SetBool("Falling", true);
                isFalling = true;
            }

            else if ((Input.GetAxis("RightStickY") > 0.7 || Input.GetAxisRaw("Vertical") > 0.7) && !inBackground)
            {
                LayerMask layerMask = 1 << 10 | 1 << 2; //ignore hitbox layer && ignoreraycast layer with raycast
                layerMask = ~layerMask;
                RaycastHit rayHit;
                if (!Physics.Raycast(transform.position + Vector3.up, Vector3.forward, out rayHit, 20, layerMask))
                {
                    Transform myParent = GetComponentInParent<SuperSuperParent>().GetComponent<Transform>();
                    myParent.position = new Vector3(myParent.position.x, myParent.position.y, 0);
                    inBackground = true;
                }
                else
                {
                    Debug.Log(rayHit.transform.name);
                }
            }

            else if ((Input.GetAxis("RightStickY") < -0.7 || Input.GetAxisRaw("Vertical") < -0.7) && inBackground)
            {
                LayerMask layerMask = 1 << 10 | 1 << 2; //ignore hitbox layer && ignoreraycast layer with raycast
                layerMask = ~layerMask;
                RaycastHit rayHit;
                if (!Physics.Raycast(transform.position + Vector3.up, Vector3.back, out rayHit, 20, layerMask)) 
                {
                    Transform myParent = GetComponentInParent<SuperSuperParent>().GetComponent<Transform>();
                    myParent.position = new Vector3(myParent.position.x, myParent.position.y, -6);
                    inBackground = false;
                }
                else
                {
                    Debug.Log(rayHit.transform.name);
                }
            }

            else if ((Input.GetAxis("RightStickX") > 0.7 || Input.GetKeyDown(KeyCode.E)) && canRoll)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                myAnimator.Play("Roll");
            }

            else if ((Input.GetAxis("RightStickX") < -0.7 || Input.GetKeyDown(KeyCode.Q)) && canRoll)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                myAnimator.Play("Roll");
            }

            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                myAnimator.SetTrigger("JumpPressed");
            }
             //
            else if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.JoystickButton2)) && canUseAbility1)
            {
                myAnimator.Play(ability1.GetAnimatorName());
                ability1.SetCooldown();
                canUseAbility1 = false;
            }
            else if ((Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.JoystickButton3)) && canUseAbility2)
            {
                immuneToStaggerKnockback = true;
                myAnimator.Play(ability2.GetAnimatorName());
                ability2.SetCooldown();
                Vector3 temp;
                if (transform.rotation.eulerAngles.y == 90)
                {
                    temp = Vector3.right;
                }
                else
                {
                    temp = Vector3.left;
                }
                StartCoroutine(DashAttack(temp));
                canUseAbility2 = false;
            }
            else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton1)) && canUseAbility3)
            {
                myAnimator.Play(ability3.GetAnimatorName());
                ability3.SetCooldown();
                canUseAbility3 = false;
            }
            else if (Input.GetKeyDown(KeyCode.K) || (Input.GetAxisRaw("Right Trigger") >= 0.3 && !rightTriggerPressed))
            {
                rightTriggerPressed = true;
                myAnimator.Play("Heavy1");
            }
            else if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                myAnimator.Play("Light1");
            }
            else
            {
                PlayerMovementControl();
            }
            //if (myRB.velocity == Vector3.zero)
            //{
            //    myAnimator.SetBool("Is_Move", false);
            //}
            //else
            //{
            //    myAnimator.SetBool("Is_Move", true);
            //}
        }
    }

    public IEnumerator DashAttack(Vector3 direction)
    {
        myAudio.DashSound();
        myAudio.HeavyGrunt();
        yield return new WaitForSeconds(0.06666f);
        Physics.IgnoreLayerCollision(9, 13, true);
        myRB.useGravity = false;
        myRB.velocity = direction * playerDashVelocity; //adjust so X goes farther maybe?
        OpenDashHitbox();
        yield return new WaitForSeconds(0.166666f);
        CloseDashHitbox();
        yield return new WaitForSeconds(0.166666f); //end of animation. maybe just make this a new function?
        myRB.useGravity = true;
        Physics.IgnoreLayerCollision(9, 13, false);
        if (GetComponentInParent<SuperSuperParent>().CheckBelow())
        {
            myAnimator.Play("Player3_0_Jump_Idle");
        }
        else
        {
            myAnimator.Play("Player3_0Battle_Idle");
        }
        immuneToStaggerKnockback = false;
    }

    public void Attacking()
    {
        if (allowedToDoThings)
        {
            if (Input.GetKeyDown(KeyCode.K) || (Input.GetAxisRaw("Right Trigger") >= 0.3 && !rightTriggerPressed))
            {
                myAnimator.SetTrigger("HeavyPressed");
                myAnimator.ResetTrigger("LightPressed");
                myAnimator.ResetTrigger("RollLeft");
                myAnimator.ResetTrigger("RollRight");
            }
            else if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                myAnimator.SetTrigger("LightPressed");
                myAnimator.ResetTrigger("HeavyPressed");
                myAnimator.ResetTrigger("RollLeft");
                myAnimator.ResetTrigger("RollRight");
            }
            else if (Input.GetAxis("RightStickX") > 0.7 || Input.GetKeyDown(KeyCode.E))
            {
                myAnimator.SetTrigger("RollRight");
                myAnimator.ResetTrigger("HeavyPressed");
                myAnimator.ResetTrigger("RollLeft");
                myAnimator.ResetTrigger("LightPressed");
            }
            else if (Input.GetAxis("RightStickX") < -0.7 || Input.GetKeyDown(KeyCode.Q))
            {
                myAnimator.SetTrigger("RollLeft");
                myAnimator.ResetTrigger("HeavyPressed");
                myAnimator.ResetTrigger("RollRight");
                myAnimator.ResetTrigger("LightPressed");
            }
        }
    }

    public void ResetAnimTriggers() //this is called when exiting attacks, at the beginning of returning to idle
    {
        myAnimator.ResetTrigger("LightPressed");
        myAnimator.ResetTrigger("HeavyPressed");
        myAnimator.ResetTrigger("HitEnemy");
    }

    public void HitEnemy()
    {
        myAnimator.SetTrigger("HitEnemy");
    }

    public void Stagger(float x)
    {
        if (!immuneToStaggerKnockback)
        {
            myAudio.PlayerHurt();
            myAnimator.SetTrigger("Stagger");
            myRB.velocity = new Vector3(0, myRB.velocity.y, 0);
            myRB.AddForce(x, 0, 0);
        }
    }

    public void KnockBack(float x, float y)
    {
        if (!immuneToStaggerKnockback)
        {
            myAudio.PlayerHurt();
            myAnimator.SetTrigger("Knockback");
            myRB.AddForce(x, y, 0);
        }
    }

    public void ResetStaggerAndKnockback()
    {
        myAnimator.ResetTrigger("Knockback");
        myAnimator.ResetTrigger("Stagger");
        myAnimator.ResetTrigger("GroundHit");
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<Ground>() != null && isFalling)
    //    {
    //        myAnimator.SetTrigger("GroundHit");
    //        isFalling = false;
    //    }
    //}

    public void GrabbingSomething(GameObject something)
    {
        thingIGrabbed = something;
        thingIGrabbed.GetComponent<IEnemy>().EnterGrab();
    }

    public void GrabState()
    {
        if(thingIGrabbed == null)
        {
            ResetStaggerAndKnockback();
            ResetAnimTriggers();
            myAnimator.Play("Player3_0Battle_Idle");
        }
        else if (Input.GetKeyDown(KeyCode.K) || (Input.GetAxisRaw("Right Trigger") >= 0.3 && !rightTriggerPressed) || Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            grabAttack = true;
        }
    }

    public Hitbox[] getAllHitboxes()
    {
        return allMyHitboxes;
    }

    public void GrabAttack()
    {
        if (grabAttack)
        {
            myAudio.BladestormSound();
            thingIGrabbed.GetComponent<IDamagable>().TakeDamage(3);
        }
        grabAttack = false;
    }

    public void ResetGrabbedThing()
    {
        if(thingIGrabbed != null)
        {
            thingIGrabbed.GetComponent<IEnemy>().ExitGrab();
        }
        thingIGrabbed = null;
    }

    public void WhileJumping()
    {
        if (allowedToDoThings)
        {
            isFalling = true;
            myAnimator.SetBool("Falling", true);
            //Trae's movement code
            moveHorizontal = Input.GetAxis("Horizontal");
            gravityEnhancer += Time.deltaTime * 2;
            //moveVertical = Input.GetAxisRaw("Vertical");
            if (moveHorizontal > 0.5)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                myRB.velocity = new Vector3(moveHorizontal * speed, myRB.velocity.y - gravityEnhancer, 0);
            }
            else if (moveHorizontal < -0.5)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                myRB.velocity = new Vector3(moveHorizontal * speed, myRB.velocity.y - gravityEnhancer, 0);
            }
            else
            {
                myRB.velocity = new Vector3(0, myRB.velocity.y - gravityEnhancer, 0);
            }
            if ((Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.JoystickButton3)) && canUseAbility2)
            {
                myAnimator.Play(ability2.GetAnimatorName());
                ability2.SetCooldown();
                Vector3 temp;
                if (transform.rotation.eulerAngles.y == 90)
                {
                    temp = Vector3.right;
                }
                else
                {
                    temp = Vector3.left;
                }
                StartCoroutine(DashAttack(temp));
                canUseAbility2 = false;
            }
            else if (Input.GetKeyDown(KeyCode.K) || (Input.GetAxisRaw("Right Trigger") >= 0.3 && !rightTriggerPressed))
            {
                myAnimator.Play("MidairHeavy1");
            }
            else if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                myAnimator.Play("MidairLight1");
            }
        }
    }
        

    public void WhileMidairAttacking()
    {
        if (!myAnimator.GetBool("GroundHit") && allowedToDoThings){
            //Trae's movement code
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            gravityEnhancer += Time.deltaTime * 2;
            //moveVertical = Input.GetAxisRaw("Vertical");
            if (moveHorizontal > 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                myRB.velocity = new Vector3(moveHorizontal * speed, myRB.velocity.y - gravityEnhancer, 0);
            }
            else if (moveHorizontal < 0)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                myRB.velocity = new Vector3(moveHorizontal * speed, myRB.velocity.y - gravityEnhancer, 0);
            }
            else
            {
                myRB.velocity = new Vector3(0, myRB.velocity.y - gravityEnhancer, 0);
            }
            if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                myAnimator.SetTrigger("LightPressed");
            }
        }        
    }

    public void WhileKnockback()
    {
        if (allowedToDoThings)
        {
            isFalling = true;
            myAnimator.SetBool("Falling", true);
            gravityEnhancer += Time.deltaTime * 2;
            if (myRB.velocity.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else if (myRB.velocity.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            myRB.velocity = new Vector3(myRB.velocity.x, myRB.velocity.y - gravityEnhancer, 0);
        }        
    }

    public IEnumerator Jump()
    {
        myAudio.JumpGrunt();
        myRB.AddForce(0, 1300, 0);
        yield return new WaitForSeconds(0.3f);
        isFalling = true;
        myAnimator.SetBool("Falling", true);
    }

    public bool AreWeFalling()
    {
        return isFalling;
    }

    public void Land()
    {
        myRB.velocity = new Vector3(0, myRB.velocity.y, 0);
        gravityEnhancer = 0;
        myAnimator.SetTrigger("GroundHit");
        isFalling = false;
        myAnimator.SetBool("Falling", false);
    }

    public IEnumerator BladestormMovement()
    {
        yield return new WaitForSeconds(0.1f);
        if (transform.rotation.eulerAngles.y == 90)
            {
                myRB.velocity = new Vector3(10, myRB.velocity.y,0);
            }
            else if(transform.rotation.eulerAngles.y >= -90)
            {
                myRB.velocity = new Vector3(-10, myRB.velocity.y, 0);
            }
        yield return new WaitForSeconds(1.35f);
        myRB.velocity = new Vector3(0,myRB.velocity.y,0);
    }

    public void InvincibilityOn()
    {
        myHurtbox.ChangeState<HurtboxHitState>();
    }

    public void InvincibilityOff()
    {
        myHurtbox.ChangeState<HurtboxOpenState>();
    }

    public void Roll()
    {
        myRB.velocity = GetDirection() * 45;
    }

    public void PutRollOnCooldown()
    {
        canRoll = false;
        rollCooldown = 1;
    }

    public Vector3 GetDirection()
    {
        if(transform.rotation == Quaternion.Euler(0, 90, 0))
        {
            return Vector3.right;
        }
        else
        {
            return Vector3.left;
        }
    }

    public void RotateLeft()
    {
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    public void RotateRight()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
