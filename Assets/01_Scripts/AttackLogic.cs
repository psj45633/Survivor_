using UnityEngine;

public class AoEAttackLogic : IAttackLogic
{
    public void Attack(Transform origin, IDamageable target)
    {
        Debug.Log("범위 공격");

        Collider2D[] hits = Physics2D.OverlapCircleAll(origin.position, Player.Instance.Stats.Range);

        foreach(var hit in hits)
        {
            if(hit.TryGetComponent(out IDamageable dmg))
            {
                dmg.TakeDamage(Player.Instance.Stats.Attack);
            }
        }
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

public class AreaDOTAttackLogic : IAttackLogic
{
    public void Attack(Transform origin, IDamageable target)
    {
        Debug.Log("몰가w 공격");

        target.TakeDamage(Player.Instance.Stats.Attack);
    }
}
