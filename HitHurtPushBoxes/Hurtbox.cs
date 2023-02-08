using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : FSGDN.StateMachine.MachineBehaviour {

    //Reference to the Pushbox this hurtbox is attached to.
    PlayerPushbox myPushbox;

    EnemyBase myEnemy;

    LevelOneBoss myBossLevel1;

    //Collider of the Hurtbox.
    BoxCollider myCollider;

    //What color our gizmo (explained later) will be at any given time.
    Color assignedColor;

    //Colors for hurtbox being open (aka active), and being hit.
    public Color openColor;
    public Color hitColor;

    public bool canBeHit;

    public float damaModifier = 1;

    public string myState;


    private void Awake()
    {
        myPushbox = GetComponentInParent<PlayerPushbox>();
        myEnemy = GetComponentInParent<EnemyBase>();
        myBossLevel1 = GetComponentInParent<LevelOneBoss>();
        myCollider = GetComponent<BoxCollider>();
    }

    public override void AddStates()
    {
        AddState<HurtboxOpenState>();
        AddState<HurtboxHitState>();
        
        SetInitialState<HurtboxOpenState>();
    }

    public override void Update()
    {
        base.Update();
        myState = GetHurtboxState();
    }

    //When you want to see what state the Hurtbox is currently in.
    public string GetHurtboxState()
    {
        return currentState.ToString();
    }

    //When we collide with an opposing hitbox.
    //private void OnTriggerEnter(Collider other)
    //{
    //    //If this hurtbox's affiliation (i.e. player) is different than the hitbox's.
    //    if(gameObject.tag != other.gameObject.tag)
    //    {
    //        //Do damage
    //        //Knock back?
    //    }
    //}

    private void OnDrawGizmos()
    {
        if (!myCollider) { return; }
        Gizmos.color = assignedColor;
        Gizmos.DrawCube(myCollider.center, myCollider.size);
    }

    public void ChangeGizmoColor(Color myColor)
    {
        assignedColor = myColor;
    }

    public void GetHitBy(int damage)
    {
        //if(IsNextState<HurtboxHitState>()) { return; }
        //ChangeState<HurtboxHitState>();
        if(myPushbox != null)
        {
            myPushbox.GetHitBy(damage * damaModifier);
        }
        else if(myEnemy != null)
        {
            myEnemy.GetHitBy(damage);
        }
        else if(myBossLevel1 != null)
        {
            myBossLevel1.TakeDamage(damage);
        }
    }

    //IEnumerator GotHit()
    //{
    //    yield return new WaitForSeconds(1);
    //    ChangeState<HurtboxOpenState>();
    //}

}
