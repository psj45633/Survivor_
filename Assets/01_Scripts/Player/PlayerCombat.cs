using UnityEngine;

public class PlayerCombat : MonoBehaviour, IAttacker, IDamageable
{
    public float attackCooltime;
    public float detectRange;
    public float damage;
    public IDamageable target;
    public LayerMask targetMask;

    float timer;
    WeaponHandler weapon;

    private void Awake()
    {
        weapon = GetComponentInChildren<WeaponHandler>();
    }

    void Update()
    {
        target = SelectTarget();

        if (target == null)
        {
            timer = attackCooltime;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= attackCooltime)
        {
            Attack(target);
            timer = 0f;
        }
    }

    public void Attack(IDamageable target)
    {
        weapon.Attack(target);
    }

    public void TakeDamage(float damage)
    {

    }

    public IDamageable SelectTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRange, targetMask);

        IDamageable closestTarget = null;
        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();

            if (damageable == null) continue;
            if (damageable == (IDamageable)this) continue;

            float dist = Vector2.Distance(transform.position, hit.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                closestTarget = damageable;
            }
        }

        return closestTarget;
    }
}
