using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase {

    public float maxCooldown;
    public float currentCooldown = 0;
    public string uiName;
    protected string animatorName;

    public void SetCooldown()
    {
        currentCooldown = maxCooldown;
    }
    
    public void SetPartialCooldown(float timeSoFar)
    {
        currentCooldown = maxCooldown - timeSoFar;
    }

    public float GetCurrentCooldown()
    {
        return currentCooldown;
    }

    public void DecrementCooldown(float time)
    {
        currentCooldown -= time;
    }

    public void SetCooldownTo0()
    {
        currentCooldown = 0;
    }

    public string GetAnimatorName()
    {
        return animatorName;
    }

    public string GetUIName()
    {
        return uiName;
    }

}
