using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private PlayerStats stats;
    [SerializeField] private Transform weaponHolder;

    public PlayerStats Stats => stats;

    private PlayerCombat combat;

    private void Awake()
    {
        Instance = this;
        combat = GetComponent<PlayerCombat>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SetBaseData();
        SetWeapon();
        SetEquipment();

        CreateSelectedWeapon();
    }

    private void SetBaseData()
    {
        stats.SetBaseData(DataManager.Instance.BaseStatData);
    }

    private void SetWeapon()
    {
        stats.SetWeapon(DataManager.Instance.SelectedWeaponData);
    }

    private void SetEquipment()
    {
        // 나중 구현
    }

    private void CreateSelectedWeapon()
    {
        WeaponData data = DataManager.Instance.SelectedWeaponData;

        if (data == null || data.weaponPrefab == null)
        {
            Debug.LogWarning("무기 데이터 또는 프리팹 없음");
            return;
        }

        Weapon weapon = Instantiate(data.weaponPrefab, weaponHolder);
        weapon.Init(data);

        combat.AddWeapon(weapon);
    }

    // 레벨업 등으로 무기 추가할 때
    public void AddWeapon(WeaponData data)
    {
        if (data == null || data.weaponPrefab == null) return;

        Weapon weapon = Instantiate(data.weaponPrefab, weaponHolder);
        weapon.Init(data);

        combat.AddWeapon(weapon);
    }
}