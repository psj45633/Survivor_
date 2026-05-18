using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    [Header("Source Data")]
    [SerializeField] private BaseStatData baseData;
    [SerializeField] private WeaponData[] weaponDatas;
    [SerializeField] private Equipment equipment;

    [Header("Upgrade Bonus")]
    [SerializeField] private float upgradeAtk;
    [SerializeField] private float upgradeAtkSpeed;
    [SerializeField] private float upgradeMoveSpeed;
    [SerializeField] private float upgradeRange;

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
        if (newWeapon == null) return;

        if (weaponDatas == null || weaponDatas.Length == 0)
        {
            weaponDatas = new WeaponData[1];
        }

        weaponDatas[0] = newWeapon;
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
        if (baseData == null)
        {
            Debug.LogWarning("baseData가 설정되지 않음");
            return;
        }

        if (weaponDatas == null || weaponDatas.Length == 0 || weaponDatas[0] == null)
        {
            Debug.LogWarning("무기 데이터가 설정되지 않음");
            return;
        }

        equipment ??= new Equipment();
        equipment.Recalculate();

        PlayerLevelData playerLvData = baseData.GetLevelData(playerLevel);

        // 여기서 직접 가져오기
        int weaponLevel = DataManager.Instance.GetWeaponLevel(weaponDatas[0]);
        WeaponLevelData weaponLvData = weaponDatas[0].GetLevelData(weaponLevel);

        finalStats = new FinalStats(playerLvData, weaponLvData, equipment, baseData);

        maxHp = finalStats.maxHp.Value;
        atk = finalStats.atk.Value + upgradeAtk;
        atkSpeed = finalStats.atkSpeed.Value * (1f + upgradeAtkSpeed);
        def = finalStats.def.Value;
        moveSpeed = finalStats.moveSpeed.Value * (1f + upgradeMoveSpeed);
        range = finalStats.range.Value + upgradeRange;
    }

    public void AddAttack(float value)
    {
        upgradeAtk += value;
        Recalculate();
    }

    public void AddAttackSpeed(float value)
    {
        upgradeAtkSpeed += value;
        Recalculate();
    }

    public void AddMoveSpeed(float value)
    {
        upgradeMoveSpeed += value;
        Recalculate();
    }

    public void AddRange(float value)
    {
        upgradeRange += value;
        Recalculate();
    }
}