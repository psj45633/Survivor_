using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    [Header("Source Data")]
    [SerializeField] private BaseStatData baseData;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Equipment equipment;

    [Header("Level")]
    [SerializeField] private int playerLevel = 1;
    [SerializeField] private int weaponLevel = 1;

    [Header("Final Values")]
    [SerializeField] private float maxHp;
    [SerializeField] private float atk;
    [SerializeField] private float atkSpeed;
    [SerializeField] private float def;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float range;

    public FinalStats finalStats;

    public float MaxHp => maxHp;
    public float Attack => atk;
    public float AttackSpeed => atkSpeed;
    public float Defense => def;
    public float MoveSpeed => moveSpeed;
    public float Range => range;

    public void Init()
    {
        Recalculate();
    }

    public void SetBaseData(BaseStatData baseData)
    {
        this.baseData = baseData;
    }

    public void SetWeapon(WeaponData newWeapon)
    {
        weaponData = newWeapon;
        weaponLevel = DataManager.Instance.GetWeaponLevel(newWeapon);
        Recalculate();
    }

    public EquipmentData SetEquipment(EquipmentData newEquipment)
    {
        if (newEquipment == null) return null;

        EquipmentData previous = null;

        switch (newEquipment.type)
        {
            case EquipmentType.Head:
                previous = equipment.head;
                equipment.head = newEquipment;
                break;

            case EquipmentType.Top:
                previous = equipment.top;
                equipment.top = newEquipment;
                break;

            case EquipmentType.Bottom:
                previous = equipment.bottom;
                equipment.bottom = newEquipment;
                break;

            case EquipmentType.Glove:
                previous = equipment.glove;
                equipment.glove = newEquipment;
                break;

            case EquipmentType.Shoe:
                previous = equipment.shoe;
                equipment.shoe = newEquipment;
                break;
        }

        Recalculate();
        return previous;
    }





    public void LevelUp()
    {
        playerLevel++;
        Recalculate();
    }


    public void Recalculate()
    {
        if (baseData == null || weaponData == null)
        {
            Debug.LogWarning("데이터가 설정되지 않음");
            return;
        }

        equipment ??= new Equipment();
        equipment.Recalculate();

        PlayerLevelData playerLvData = baseData.GetLevelData(playerLevel);

        // 여기서 직접 가져오기
        int weaponLevel = DataManager.Instance.GetWeaponLevel(weaponData);
        WeaponLevelData weaponLvData = weaponData.GetLevelData(weaponLevel);

        finalStats = new FinalStats(playerLvData, weaponLvData, equipment, baseData);

        maxHp = finalStats.maxHp.Value;
        atk = finalStats.atk.Value;
        atkSpeed = finalStats.atkSpeed.Value;
        def = finalStats.def.Value;
        moveSpeed = finalStats.moveSpeed.Value;
        range = finalStats.range.Value;
    }
}