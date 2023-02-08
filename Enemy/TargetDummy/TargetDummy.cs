using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour, IDamagable, IStagger
{

    Animator myAnimator;
    Rigidbody myRB;
    Transform myFoot; //i use this as an origin point to check if we hit the ground. this is where our raycast originates from
    Hurtbox myHurtbox; //this reference is for making our character invincible on hitting the ground
    Renderer myRenderer;

    float myCurHP;
    float myMaxHP = 99999;

    private void Awake()
    {
        myAnimator = gameObject.GetComponent<Animator>();
        myRB = gameObject.GetComponent<Rigidbody>();
        myFoot = transform.GetChild(2).transform; //fill in which child it is, in this guy's case, it is the 3rd child (remember, 0 counts as #1!)
        myHurtbox = gameObject.GetComponentInChildren<Hurtbox>();
        Renderer myRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    // Use this for initialization
    void Start () {
        myCurHP = myMaxHP;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Idle() //idle's update
    {
        //do things here
    }

    //public void SetKnockbackColor()
    //{
    //    myRenderer.material.color = new Color(0.5849056f, 0.5849056f, 0.5849056f, 0);
    //}

    public void WhileKnockback() //knockback's update
    {
        RaycastHit rayHit;
        Debug.DrawRay(myFoot.position, Vector3.down, Color.blue, 0.5f);
        if (Physics.Raycast(myFoot.position, Vector3.down, out rayHit, 0.5f) && rayHit.collider.transform.GetComponent<Ground>())
        {
            myAnimator.SetTrigger("GroundHit");
        }
        //if our raycast hits the ground, start the getup process
    }

    public void InvincibilityOn()
    {
        myHurtbox.ChangeState<HurtboxHitState>();
        //make it so that we can't be hit when we're getting up
    }

    public void InvincibilityOff()
    {
        myHurtbox.ChangeState<HurtboxOpenState>();
        //make it so that we can be hit again after getting up
    }

    public void TakeDamage(float damage)
    {
        myCurHP -= damage;
        if(myCurHP <= 0)
        {
            //do death things
        }
    }

    public void TakeEnviroDamage(int damage)
    {

    }

    public int GivePoints(float something)
    {
        return 0;
    }

    public void Stagger(float x)
    {
        myAnimator.SetTrigger("Stagger");
        myRB.AddForce(x, 0, 0);
    }

    public void KnockBack(float x, float y)
    {
        myAnimator.SetTrigger("Knockback");
        myRB.AddForce(x, y, 0);
    }

    public void ResetAnimTriggers()
    {
        //you want to call this function to reset any animation triggers you want to get rid of
        //ex. get hit but have 14 attack triggers queued up? want to reset that?
        //just put myAnimator.ResetTrigger("insertTriggerInMyAnimatorHere"); for each trigger you want to reset
        //i kinda just put a call for this function everywhere in the player. use it where you think it's appropriate.
    }

    public void ResetAttackHitboxes()
    {
        //this function tells our character to cancel any hitboxes it currently has active.
        //when it's attacking but gets hit in the middle of its attack, it won't turn off its hitbox.
        //assemble all your Hitboxes into an array - a good way to do this is this way:
        //Hitbox[] myHitboxes; (put this at the top before all your functions)
        //in Awake():
        //myHitboxes = GetComponentsInChildren<Hitbox>();
        //this will automatically get every Hitbox your character has.

        //in this function, you want to put something along these lines:
        //foreach(Hitbox hitbox in myHitboxes)
        //{
        //hitbox.ClearCollidedList();
        //hitbox.gameObject.SetActive(false);
        //}

        //that will turn off all your hitboxes and make sure they can hit the player the next time that hitbox comes out.
    }
}
