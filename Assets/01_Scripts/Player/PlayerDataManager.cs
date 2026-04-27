using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    [SerializeField] private WeaponData selectedWeaponData;

    public WeaponData SelectedWeaponData => selectedWeaponData;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SelectWeapon(WeaponData weaponData)
    {
        selectedWeaponData = weaponData;
    }


}
