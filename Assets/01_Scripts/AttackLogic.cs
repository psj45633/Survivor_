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
    private const int baseChainCount = 5;
    private const float baseChainRange = 4f;

    private const float explosionRange = 2f;
    private const float explosionDamageRate = 0.5f;

    public void Attack(Transform origin, IDamageable target)
    {
        Debug.Log("연쇄 공격");

        if (target == null || target.IsDead) return;

        int totalChainCount = baseChainCount + Player.Instance.Upgrade.bonusChainCount;
        float totalChainRange = baseChainRange + Player.Instance.Upgrade.bonusChainRange;

        HashSet<IDamageable> hitTargets = new();

        IDamageable currentTarget = target;

        CreateLightning(origin.position, currentTarget.Transform.position);

        for (int i = 0; i < totalChainCount; i++)
        {
            if (currentTarget == null || currentTarget.IsDead)
                break;

            currentTarget.TakeDamage(Player.Instance.Stats.Attack);

            if (Player.Instance.Upgrade.chainExplosion)
            {
                Explosion(currentTarget.Transform.position);
            }

            hitTargets.Add(currentTarget);

            IDamageable nextTarget = FindNextTarget(
                currentTarget,
                hitTargets,
                totalChainRange
            );

            if (nextTarget != null)
            {
                CreateLightning(
                    currentTarget.Transform.position,
                    nextTarget.Transform.position
                );
            }

            currentTarget = nextTarget;
        }
    }

    private IDamageable FindNextTarget(
        IDamageable current,
        HashSet<IDamageable> hitTargets,
        float chainRange
    )
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

            if (damageable == current)
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

    private void Explosion(Vector2 position)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            position,
            explosionRange,
            LayerMask.GetMask("Enemy")
        );

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out IDamageable damageable))
                continue;

            if (damageable.IsDead)
                continue;

            float damage = Player.Instance.Stats.Attack * explosionDamageRate;
            damageable.TakeDamage(damage);
        }

        // 폭발 이펙트 풀 있으면 여기서 생성
        GameObject obj = GameManager.Instance.pool.Get(3);
        ExplosionEffect effect = obj.GetComponent<ExplosionEffect>();
        effect.Init(position);
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
