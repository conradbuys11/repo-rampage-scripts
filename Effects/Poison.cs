using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    float timer;
    IDamagable me;

    private void Start()
    {
        me = GetComponent<IDamagable>();
    }

    void Update ()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            me.TakeEnviroDamage(1);
            timer = 1;
        }
	}
}
