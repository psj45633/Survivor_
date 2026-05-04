using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamageable
{
    [Header("Targeting")]
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask targetMask;

    private readonly List<Weapon> weapons = new();
    private IDamageable target;

    private void Start()
    {
        detectRange = Player.Instance.Stats.Range;
    }

    private void Update()
    {
        target = SelectTarget();

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].Tick(target);
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        if (weapon == null) return;
        if (weapons.Contains(weapon)) return;

        weapons.Add(weapon);
    }

    public void TakeDamage(float damage)
    {
        // 나중에 체력 처리
    }

    private IDamageable SelectTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRange, targetMask);

        List<IDamageable> targets = new();
        List<float> weights = new();

        float totalWeight = 0f;

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out IDamageable damageable)) continue;
            if (damageable == (IDamageable)this) continue;

            float distance = Vector2.Distance(transform.position, hit.transform.position);

            float weight = 1f / Mathf.Max(distance, 0.1f);

            targets.Add(damageable);
            weights.Add(weight);
            totalWeight += weight;
        }

        if (targets.Count == 0) return null;

        float randomValue = Random.Range(0f, totalWeight);

        for (int i = 0; i < targets.Count; i++)
        {
            randomValue -= weights[i];

            if (randomValue <= 0f)
            {
                return targets[i];
            }
        }

        return targets[targets.Count - 1];
    }
}