using UnityEngine;

public class Stat
{
    public float baseValue;
    public float bonusValue;

    public float modifier;

    public float Value => (baseValue + bonusValue) * modifier;

    public Stat(float baseValue, float bonusValue = 0, float modifier = 1f)
    {
        this.baseValue = baseValue;
        this.bonusValue = bonusValue;
        this.modifier = modifier;
    }
}

[System.Serializable]
public class Equipment
{
    public EquipmentData head;
    public EquipmentData top;
    public EquipmentData bottom;
    public EquipmentData glove;
    public EquipmentData shoe;

    private float maxHp;
    private float atk;
    private float atkSpeed;
    private float def;
    private float moveSpeed;

    public float MaxHp => maxHp;
    public float Atk => atk;
    public float AtkSpeed => atkSpeed;
    public float Def => def;
    public float MoveSpeed => moveSpeed;


    public void Recalculate()
    {
        maxHp = 0f;
        atk = 0f;
        def = 0f;

        EquipmentData[] equipments = { head, top, bottom, glove, shoe };

        foreach (var equipment in equipments)
        {
            if (equipment == null) continue;

            maxHp += equipment.maxHp;
            atk += equipment.atk;
            atkSpeed += equipment.atkSpeed;
            def += equipment.def;
            moveSpeed += equipment.moveSpeed;
        }
    }
}

public class FinalStats
{
    public Stat maxHp;
    public Stat atk;
    public Stat atkSpeed;
    public Stat def;
    public Stat moveSpeed;
    public Stat range;

    public FinalStats(PlayerLevelData playerLvData, WeaponLevelData weaponLvData, Equipment equipmentStats, BaseStatData baseData)
    {
        maxHp     = new Stat(playerLvData.maxHp,     equipmentStats.MaxHp);
        atk       = new Stat(playerLvData.atk,       equipmentStats.Atk + weaponLvData.atk);
        atkSpeed  = new Stat(playerLvData.atkSpeed,  0f,                                     (1f + weaponLvData.atkSpeed + equipmentStats.AtkSpeed));
        def       = new Stat(playerLvData.def,       equipmentStats.Def);
        moveSpeed = new Stat(playerLvData.moveSpeed);
        range     = new Stat(playerLvData.range,     weaponLvData.range);
         
    }
}


