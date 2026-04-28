using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData",menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    public WeaponType type;

    public string id;
    public string weaponName;

    public List<WeaponLevelData> levelStats;

    public WeaponLevelData GetLevelData(int level)
    {
        if (level <= 0) level = 1;

        if (level > levelStats.Count)
            return levelStats[levelStats.Count - 1];

        return levelStats[level - 1];
    }
}

[System.Serializable]
public class WeaponLevelData
{
    public float atk;
    public float range;
    public float atkSpeed;
}