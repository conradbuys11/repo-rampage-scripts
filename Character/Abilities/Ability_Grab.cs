using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Grab : AbilityBase {

    public Ability_Grab()
    {
        uiName = "Grab";
        animatorName = "Grab Try";
        maxCooldown = 8;
    }

}
