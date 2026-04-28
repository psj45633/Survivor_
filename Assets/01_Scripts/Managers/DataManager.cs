using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;



    [SerializeField] private BaseStatData baseStatData;
    [SerializeField] private WeaponData selectedWeaponData;

    public BaseStatData BaseStatData => baseStatData;
    public WeaponData SelectedWeaponData => selectedWeaponData;

    [Header("Weapon Levels")]
    [SerializeField] private List<WeaponLevelSaveData> weaponLevels = new();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetBaseData(baseStatData);
    }

    // base스탯 세팅
    public void SetBaseData(BaseStatData baseStatData)
    {
        this.baseStatData = baseStatData;
    }

    // 무기 세팅
    public void SelectWeapon(WeaponData weaponData)
    {
        selectedWeaponData = weaponData;
        
    }

    // 무기 레벨 가져오기
    public int GetWeaponLevel(WeaponData weapon)
    {
        WeaponLevelSaveData data = GetOrCreateWeaponData(weapon.type);
        return data.level;
    }

    // 무기 레벨업
    public void LevelUpWeapon(WeaponData weapon)
    {
        WeaponLevelSaveData data = GetOrCreateWeaponData(weapon.type);
        data.level++;
    }

    // 내부 함수
    private WeaponLevelSaveData GetOrCreateWeaponData(WeaponType type)
    {
        foreach (var data in weaponLevels)
        {
            if (data.weaponType == type)
                return data;
        }

        var newData = new WeaponLevelSaveData
        {
            weaponType = type,
            level = 1
        };

        weaponLevels.Add(newData);
        return newData;
    }

}
