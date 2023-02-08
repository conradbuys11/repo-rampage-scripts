using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHitbox : Hitbox {

    PlayerPushbox myPlayer;

    public override void Awake()
    {
        base.Awake();
        myPlayer = GetComponentInParent<PlayerPushbox>();
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInParent<Enemy_Thug>() || other.GetComponentInParent<Enemy_Bruiser>() || other.GetComponentInParent<Enemy_kungFu>())
        {
            Hurtbox temp = other.GetComponent<Hurtbox>();
            if (temp.IsCurrentState<HurtboxOpenState>() && !temp.IsNextState<HurtboxOpenState>())
            {
                CameraFollow.CinematicPosition();
                if (other.GetComponentInParent<Enemy_Thug>())
                {
                    myPlayer.GrabbingSomething(other.GetComponentInParent<Enemy_Thug>().gameObject);
                    myPlayer.myAnimator.Play("Grab");
                    gameObject.SetActive(false);
                }
                else if (other.GetComponentInParent<Enemy_Bruiser>())
                {
                    myPlayer.GrabbingSomething(other.GetComponentInParent<Enemy_Bruiser>().gameObject);
                    myPlayer.myAnimator.Play("Grab");
                    gameObject.SetActive(false);
                }
                else if (other.GetComponentInParent<Enemy_kungFu>())
                {
                    myPlayer.GrabbingSomething(other.GetComponentInParent<Enemy_kungFu>().gameObject);
                    myPlayer.myAnimator.Play("Grab");
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
