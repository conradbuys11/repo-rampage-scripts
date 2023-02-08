using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialHitboxes : MonoBehaviour {

    public Hitbox[] specialHitboxes;

    private void Awake()
    {
        specialHitboxes = GetComponentsInChildren<Hitbox>();
    }

    public Hitbox[] GetHitboxes()
    {
        return specialHitboxes;
    }
}
