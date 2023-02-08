using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee : EnemyBase, IDamagable
{
    public float attackDamage;

    public override void Attack()
    {
        base.Attack();
        if(myPlayer.myHurtbox.IsCurrentState<HurtboxOpenState>())
            myPlayer.GetHitBy(attackDamage);
    }

    public override void MoveTowardsIdleLocation()
    {
        if(idleLocation == Vector3.zero)
        {
            GetIdle();
        }
        base.MoveTowardsIdleLocation();
    }

    void GetIdle()
    {
        idleLocation = myPlayer.transform.position + new Vector3(Random.Range(-6, 7), Random.Range(-4, 7), 0);
    }

}
