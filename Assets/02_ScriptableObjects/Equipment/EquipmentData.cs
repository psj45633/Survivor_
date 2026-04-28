using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentData", menuName = "Data/EquipmentData")]
public class EquipmentData : ScriptableObject
{
    public EquipmentType type;

    public float maxHp;
    public float atk;
    public float atkSpeed;
    public float def;
    public float moveSpeed;
}
