using UnityEngine;

public class WeaponSelectButton : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;

    public void Click()
    {
        DataManager.Instance.SelectWeapon(weaponData);
    }
}
