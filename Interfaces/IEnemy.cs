using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IEnemy
{
    bool IsAttack();

    void BeginAttack(bool toWar);

    void Idel();

    void SetDirector(EAID director, Text healthText);

    void EnterGrab();

    void ExitGrab();

    void DuringGrab();
}
