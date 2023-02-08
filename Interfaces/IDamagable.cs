using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float damage);

    int GivePoints(float mod);

    void TakeEnviroDamage(int damage);
}
