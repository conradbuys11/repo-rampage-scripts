using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hadouken : Hitbox {

    public float projectileSpeed = 4f;
    public float lifeTime = 4f; //duration in seconds
    float timer = 0;

    public override void Start()
    {
    }

    public void OtherWay()
    {
        projectileSpeed *= -1;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if(timer >= lifeTime)
        {
            Destroy(gameObject);
        }
        transform.position += new Vector3(projectileSpeed / 60, 0, 0);
	}
}
