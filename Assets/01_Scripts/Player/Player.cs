using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;


    [SerializeField] private BaseStatData baseStatData;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Equipment equipment; 
    public PlayerStats Stats {  get; private set; }

    private void Awake()
    {
        Instance = this;

        Stats = new PlayerStats(baseStatData, weaponData, equipment);
    }
    void Start()
    {
        //Stats.Recalculate();
        //Debug.Log("d");
        weaponData = PlayerDataManager.Instance.SelectedWeaponData;
        //Debug.Log("dd");
    }

}
