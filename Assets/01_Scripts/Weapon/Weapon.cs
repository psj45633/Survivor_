using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData data;
    [SerializeField] private float attackSpeed;

    private float timer;
    private IAttackLogic attackLogic;

    public void Init(WeaponData data)
    {
        this.data = data;
        attackSpeed = Player.Instance.Stats.AttackSpeed;

        switch (data.type)
        {
            case WeaponType.AOE:
                attackLogic = new AoEAttackLogic();
                break;

            case WeaponType.Shotgun:
                attackLogic = new ProjectileAttackLogic();
                break;

            case WeaponType.ChainLightning:
                attackLogic = new ChainAttackLogic();
                break;

            case WeaponType.AreaDOT:
                attackLogic = new AreaDOTAttackLogic();
                break;
        }
    }

    public void Tick(IDamageable target)
    {
        if (target == null)
        {
            timer = 1f / attackSpeed;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= 1f / attackSpeed)
        {
            attackLogic?.Attack(transform, target);
            timer = 0f;
        }
    }
}
