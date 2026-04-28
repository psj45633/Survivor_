using UnityEngine;

public class WeaponSelectButton : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;

    public void Click()
    {
        PlayerDataManager.Instance.SelectWeapon(weaponData);
    }
}
