using JetBrains.Annotations;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public WeaponType type;

    private IAttackLogic attackLogic;

    public void Awake()
    {
        switch (type)
        {
            case WeaponType.Field:
                attackLogic = new AoEAttackLogic();

                break;
            
            case WeaponType.Shotgun:
                attackLogic = new ProjectileAttackLogic();

                break;
            
            case WeaponType.Staff_Chain:
                attackLogic = new ChainAttackLogic();

                break;

            case WeaponType.Staff_Hitscan:
                attackLogic = new HitscanAttackLogic();

                break;
        }
    }

    public void Attack(IDamageable target)
    {
        attackLogic?.Attack(transform, target);
    }

}
