using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyHitboxes : MonoBehaviour {

    public Hitbox[] heavyHitboxes;

    void Awake()
    {
        heavyHitboxes = GetComponentsInChildren<Hitbox>();
    }

    public Hitbox[] GetHitboxes()
    {
        return heavyHitboxes;
    }
}
