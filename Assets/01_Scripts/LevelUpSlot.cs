using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelUpSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descText;
    [SerializeField] private Button button;

    private UpgradeData data;
    private LevelUpUI levelUpUI;

    public void Set(UpgradeData data, LevelUpUI levelUpUI)
    {
        this.data = data;
        this.levelUpUI = levelUpUI;

        icon.sprite = data.icon;
        titleText.text = data.upgradeName;
        descText.text = string.Format(data.description, data.value);

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Select);
    }

    private void Select()
    {
        levelUpUI.ApplyUpgrade(data);
    }
}
