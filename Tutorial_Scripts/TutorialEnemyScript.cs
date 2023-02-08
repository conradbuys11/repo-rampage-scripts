using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyScript : MonoBehaviour, IDamagable
{
    public void TakeDamage(float dmg)
    {
        Destroy(gameObject);
    }

    public int GivePoints(float mod)
    {
        return 0;
    }

    public void TakeEnviroDamage(int dmg)
    {

    }

}
