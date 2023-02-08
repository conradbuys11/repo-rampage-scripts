using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Death : MonoBehaviour , IDamagable
{

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            //print("Whatever touched me is dead now");
            //Insert death for enemy and player
            other.gameObject.GetComponent<IDamagable>().TakeDamage(5000);
        }
    }

    public void TakeDamage(float dmg)
    {

    }
    
    public void TakeEnviroDamage(int damage)
    {

    }

    public int GivePoints(float pnt)
    {
        return 0;
    }
}
