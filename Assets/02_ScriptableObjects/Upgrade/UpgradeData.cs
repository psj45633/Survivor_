using UnityEngine;

[CreateAssetMenu(fileName ="UpgradeData", menuName ="Upgrade/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

    public UpgradeType type;
    public float value;

    [Range(0, 100)]
    public int weight = 10;
}
