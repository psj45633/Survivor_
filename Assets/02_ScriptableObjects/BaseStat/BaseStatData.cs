using UnityEngine;

[CreateAssetMenu(fileName = "BaseStatData", menuName = "Data/BaseStatData")]
public class BaseStatData : ScriptableObject
{
    public float maxHp;
    public float atk;
    public float atkSpeed;
    public float range;
    public float def;
    public float moveSpeed;

}
