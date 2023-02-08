using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFireStatus<T>
{
    void TakeDot(T damage, T duration);
    IEnumerator FireDot(T damage, T duration);
}

public interface IFrostStatus<T>
{
    void TakeSlow(T percentage, T duration);
    IEnumerator FrostSlow(T slowPercentage, T duration);
}

public interface ILightningStatus<T>
{
    void TakeStun(T duration);
    IEnumerator LightningStun(T duration);
}

public interface IPoisonStatus<T>
{
    void TakePoi(T damage);
    IEnumerator PoisonDDot(T damage);
}
