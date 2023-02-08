using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashbang : Hitbox {

    float lifeTime = 0.2f;

    public void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
