using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseStatData", menuName = "Data/BaseStatData")]
public class BaseStatData : ScriptableObject
{
    public string id;

    public List<PlayerLevelData> levelStats;

    public PlayerLevelData GetLevelData(int level)
    {
        if (level <= 0) level = 1;

        if (level > levelStats.Count)
            return levelStats[levelStats.Count - 1];

        return levelStats[level - 1];
    }
}

[System.Serializable]
public class PlayerLevelData
{
    public float maxHp;
    public float atk;
    public float atkSpeed;
    public float def;
    public float moveSpeed;
    public float range;
}