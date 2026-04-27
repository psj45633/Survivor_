using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData",menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    public WeaponType type;

    public float atk;
    public float atkSpeed;
    public float range;

    public int count;
    public float healthSteal;


    [Header("Field")]
    public float slow;

    [Header("Shotgun")]
    public float angle;

    [Header("Staff")]
    public float chainRange;



}
