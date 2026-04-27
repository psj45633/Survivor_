using UnityEngine;

public class AoEAttackLogic : IAttackLogic
{
    public void Attack(Transform origin, IDamageable target)
    {
        Debug.Log("범위 공격");
    }
}

public class ProjectileAttackLogic : IAttackLogic
{
    public void Attack(Transform origin, IDamageable target)
    {
        Debug.Log("투사체 생성");
    }
}

public class ChainAttackLogic : IAttackLogic
{
    public void Attack(Transform origin, IDamageable target)
    {
        Debug.Log("연쇄 공격");
    }
}

public class HitscanAttackLogic : IAttackLogic
{
    public void Attack(Transform origin, IDamageable target)
    {
        Debug.Log("즉발 공격");
    }
}
