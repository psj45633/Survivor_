public class PlayerStats
{
    public BaseStatData baseData;
    public WeaponData weaponData;
    public Equipment equipment;

    public FinalStats finalStats;

    public float MaxHp => finalStats.maxHp.Value;
    public float Attack => finalStats.atk.Value;
    public float AttackSpeed => finalStats.atkSpeed.Value;
    public float Defense => finalStats.def.Value;
    public float MoveSpeed => finalStats.moveSpeed.Value;
    public float Range => finalStats.range.Value;

    public PlayerStats(BaseStatData baseData, WeaponData weaponData, Equipment equipment)
    {
        this.baseData = baseData;
        this.weaponData = weaponData;
        this.equipment = equipment;
        Recalculate();
    }

    public void EquipEquipment(EquipmentData newEquipmentData)
    {
        switch (newEquipmentData.type)
        {
            case EquipmentType.Head:
                equipment.head = newEquipmentData;
                break;
            case EquipmentType.Top:
                equipment.top = newEquipmentData;
                break;
            case EquipmentType.Bottom:
                equipment.bottom = newEquipmentData;
                break;
            case EquipmentType.Glove:
                equipment.glove = newEquipmentData;
                break;
            case EquipmentType.Shoe:
                equipment.shoe = newEquipmentData;
                break;
        }

        Recalculate();
    }

    public void EquipWeapon(WeaponData newWeaponData)
    {
        weaponData = newWeaponData;
        Recalculate();
    }

    public void Recalculate()
    {
        equipment.Recalculate();
        finalStats = new FinalStats(baseData, equipment, weaponData);
    }
}