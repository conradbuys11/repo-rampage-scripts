using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHitboxes : MonoBehaviour {

    public Hitbox[] lightHitboxes;

    void Awake()
    {
        lightHitboxes = GetComponentsInChildren<Hitbox>();
    }

    public Hitbox[] GetHitboxes()
    {
        return lightHitboxes;
    }
}
