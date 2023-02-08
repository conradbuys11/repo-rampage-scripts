using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherProjectile : Hitbox {

    [Range(0, 10)]
    public float despawnTimer;

    void Update()
    {
        despawnTimer -= Time.deltaTime;
        if(despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Hurtbox temp = other.GetComponent<Hurtbox>();
    //        if (!temp) { return; }
    //        if (temp.IsCurrentState<HurtboxOpenState>() && !temp.IsNextState<HurtboxOpenState>())
    //        {
    //            temp.transform.GetComponentInParent<PlayerPushbox>();
    //            if (dot)
    //            {
    //                temp.transform.GetComponentInParent<PlayerPushbox>().TakeDot(dotDamage, dotDuration);
    //            }
    //            if (slow)
    //            {
    //                temp.transform.GetComponentInParent<PlayerPushbox>().TakeSlow(slowAmount, slowDuration);
    //            }
    //            if (stun)
    //            {
    //                temp.transform.GetComponentInParent<PlayerPushbox>().TakeStun(stunDuration);
    //            }
    //            temp?.GetHitBy(7);
    //            Destroy(gameObject);
    //        }
    //        Destroy(gameObject);
    //    }
    //}
}
