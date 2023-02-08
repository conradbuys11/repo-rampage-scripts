using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonV_Hitbox : Hitbox
{
    GameObject Cam;
	
	public override void Start ()
    {
        Cam = FindObjectOfType<AudioDirector>().gameObject;
	}
	

	void Update ()
    {
		
	}

    protected override void OnTriggerStay(Collider other)
    {
        if ((gameObject.tag == "Player" && other.gameObject.tag == "Enemy") || (gameObject.tag == "Enemy" && other.gameObject.tag == "Player"))
        {
            Hurtbox temp = other.GetComponent<Hurtbox>();
            if (!temp) { return; }
            foreach (Hurtbox box in collidedHurtboxes)
            {
                if (temp == box) { return; }
            }
            if (temp.IsCurrentState<HurtboxOpenState>() && !temp.IsNextState<HurtboxOpenState>() && poi)
            {
                Cam.GetComponent<AudioDirector>().Env33();
                temp.GetComponentInParent<IDamagable>().TakeDamage(attackDamage * damageModifier);
                collidedHurtboxes.Add(temp);
                StartCoroutine(PoisonDot(temp));
            }
        }

    }

    IEnumerator PoisonDot(Hurtbox temp)
    {
        List<Hurtbox> templist = collidedHurtboxes;
        yield return new WaitForSeconds(1);
        foreach(Hurtbox box in collidedHurtboxes.ToArray())
        {
            if(temp == box)
            {
                collidedHurtboxes.Remove(box);
            }
        }
    }
}
