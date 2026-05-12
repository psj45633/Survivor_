using System.Collections.Generic;
using UnityEngine;

public class AoEAttackLogic : IAttackLogic
{
    public void Attack(Transform origin, IDamageable target)
    {
        Debug.Log("범위 공격");

        Collider2D[] hits = Physics2D.OverlapCircleAll(origin.position, Player.Instance.Stats.Range);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out IDamageable dmg))
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
        if (target == null) return;

        Debug.Log("투사체 생성");
        GameObject obj = GameManager.Instance.pool.Get(0);
        Projectile projectile = obj.GetComponent<Projectile>();

        Vector2 originPos = origin.position;
        Vector2 targetPos = target.Transform.position;
        Vector2 dir = targetPos - originPos;
                
        projectile.Init(origin.position, dir, Player.Instance.Stats.Attack);
    }
}

public class ChainAttackLogic : IAttackLogic
{
    private const int chainCount = 5;
    private const float chainRange = 4f;

    public void Attack(Transform origin, IDamageable target)
    {
        Debug.Log("연쇄 공격");

        if (target == null) return;

        HashSet<IDamageable> hitTargets = new();

        IDamageable currentTarget = target;

        CreateLightning(origin.position, currentTarget.Transform.position);

        for (int i = 0; i < chainCount; i++)
        {
            if (currentTarget == null)
                break;

            currentTarget.TakeDamage(Player.Instance.Stats.Attack);
            //Debug.Log(Player.Instance.Stats.Attack);

            hitTargets.Add(currentTarget);

            IDamageable nextTarget = FindNextTarget(currentTarget, hitTargets);

            if (nextTarget != null)
            {
                GameObject obj = GameManager.Instance.pool.Get(1); // 번개 이펙트 풀 인덱스
                LightningEffect effect = obj.GetComponent<LightningEffect>();

                effect.Init(
                    currentTarget.Transform.position,
                    nextTarget.Transform.position
                );
            }
            currentTarget = nextTarget;
        }
    }

    private IDamageable FindNextTarget(IDamageable current, HashSet<IDamageable> hitTargets)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            current.Transform.position,
            chainRange,
            LayerMask.GetMask("Enemy")
        );

        IDamageable nextTarget = null;
        float closestDist = float.MaxValue;

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out IDamageable damageable))
                continue;

            if (damageable.IsDead)
                continue;

            if (hitTargets.Contains(damageable))
                continue;

            float dist = Vector2.Distance(
                current.Transform.position,
                hit.transform.position
            );

            if (dist < closestDist)
            {
                closestDist = dist;
                nextTarget = damageable;
            }
        }

        return nextTarget;
    }

    private void CreateLightning(Vector2 start, Vector2 end)
    {
        GameObject obj = GameManager.Instance.pool.Get(1);
        LightningEffect effect = obj.GetComponent<LightningEffect>();
        effect.Init(start, end);
    }
}

public class AreaDOTAttackLogic : IAttackLogic
{
    public void Attack(Transform origin, IDamageable target)
    {
        Debug.Log("몰가w");

        target.TakeDamage(Player.Instance.Stats.Attack);
    }
}
