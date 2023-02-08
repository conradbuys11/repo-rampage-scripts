using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Archer : EnemyBase {

    public GameObject projectile;

    public override void MoveTowardsIdleLocation()
    {
        if (idleLocation == Vector3.zero)
        {
            GetIdle();
        }
        base.MoveTowardsIdleLocation();
    }

    void GetIdle()
    {
        idleLocation = myPlayer.transform.position + new Vector3(Random.Range(-6, 4), Random.Range(-4, 4), 0);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsCurrentState<EnemyKOState>() && !IsNextState<EnemyKOState>())
        {
            if (other.tag == "Player" && !IsCurrentState<EnemyRecoilState>() && !IsCurrentState<EnemyStunnedState>())
            {
                if (!IsCurrentState<EnemyPursueState>() && !IsNextState<EnemyPursueState>())
                {
                    ChangeState<EnemyPursueState>();
                }
            }
        }
    }

    public override void Attack()
    {
        GameObject myProjectile;
        if(transform.position.x > myPlayer.transform.position.x)
        {
            myProjectile = Instantiate(projectile, transform.position + new Vector3(-1, 0, 0), transform.rotation);
            myProjectile.GetComponent<Rigidbody>().AddForce(-500, 0, 0);
        }
        else if(transform.position.x < myPlayer.transform.position.x)
        {
            myProjectile = Instantiate(projectile, transform.position + new Vector3(1, 0, 0), transform.rotation);
            myProjectile.GetComponent<Rigidbody>().AddForce(500, 0, 0);
        }
    }

    public override Vector3 CheckDistance()
    {
        float front;
        float back;
        Vector3 target;
        front = Vector3.Distance(transform.position, myPlayer.transform.position + new Vector3(5, 0, 0));
        back = Vector3.Distance(transform.position, myPlayer.transform.position + new Vector3(-5, 0, 0));
        if (front > back)
        {
            target = myPlayer.transform.position + new Vector3(-5, 0, 0);
        }
        else
        {
            target = myPlayer.transform.position + new Vector3(5, 0, 0);
        }
        return target;
    }
}
