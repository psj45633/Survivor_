using UnityEngine;

public interface IAttacker
{
    void Attack(IDamageable target);
}

public interface IDamageable
{
    void TakeDamage(float damage);
}

public interface IAttackLogic
{
    void Attack(Transform origin,IDamageable target);
}