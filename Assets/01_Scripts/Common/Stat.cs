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

    public float maxHp;
    public float atk;
    public float def;

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
            def += equipment.def;
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

    public FinalStats(BaseStatData baseData, Equipment equipmentStats, WeaponData weaponData)
    {
        maxHp     = new Stat(baseData.maxHp     , equipmentStats.maxHp);
        atk       = new Stat(baseData.atk       , equipmentStats.atk + weaponData.atk);
        atkSpeed  = new Stat(baseData.atkSpeed  , 0f                                       , 1f + weaponData.atkSpeed/100);
        def       = new Stat(baseData.def       , equipmentStats.def);
        moveSpeed = new Stat(baseData.moveSpeed);
        range     = new Stat(baseData.range     , weaponData.range);
    }
}


