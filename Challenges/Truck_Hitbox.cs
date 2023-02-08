using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck_Hitbox : Hitbox
{

    public override void Start()
    {
    }

    protected override void OnTriggerStay(Collider other)
    {

        Hurtbox temp = other.GetComponent<Hurtbox>();
        if (!temp) { return; }
        foreach (Hurtbox box in collidedHurtboxes)
        {
            if (temp == box) { return; }
        }
        if (temp.IsCurrentState<HurtboxOpenState>() && !temp.IsNextState<HurtboxOpenState>())
        {
            if (temp.GetComponentInParent<Enemy_Thug>())
            {
                if(temp.GetComponentInParent<Enemy_Thug>().immuneToCarTimer > 0) { return; }
            }
            else if (temp.GetComponentInParent<Enemy_Bruiser>())
            {
                if(temp.GetComponentInParent<Enemy_Bruiser>().immuneToCarTimer > 0) { return; }
            }
            else if (temp.GetComponentInParent<Enemy_kungFu>())
            {
                if(temp.GetComponentInParent<Enemy_kungFu>().immuneToCarTimer > 0) { return; }
            }
            temp.GetComponentInParent<IDamagable>().TakeDamage(attackDamage * damageModifier);
            collidedHurtboxes.Add(temp);
            if (temp.GetComponentInParent<IStagger>() != null)
            {
                if (knockback)
                {
                        temp.GetComponentInParent<IStagger>().KnockBack(knockbackDistanceX, knockbackDistanceY);
                }
            }
        }

    }
}
