using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private PlayerStats stats;
    public PlayerStats Stats => stats;

    private void Awake()
    {
        Instance = this;

        stats.Init();
    }
    void Start()
    {
        //Stats.Recalculate();
        Init();
    }


    private void Init()
    {
        SetBaseData();
        SetWeapon();
        SetEquipment();
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

    }
}
