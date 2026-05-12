using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    [Header("Exp")]
    [SerializeField] private int lv;
    [SerializeField] private int curExp;
    [SerializeField] private int[] maxExp;

    [Header("UI")]
    [SerializeField] private Image curExpImg;
    [SerializeField] private LevelUpUI levelUpUI;

    private void Start()
    {
        Init();
        curExpImg.fillAmount = 0;
    }

    private float ExpPercet()
    {
        return (float)curExp / maxExp[lv];
    }


    private void Init()
    {
        lv = 0;
        curExp = 0;
    }

    public void AddExp(int exp)
    {
        if (lv >= maxExp.Length) return;

        curExp += exp;
        if (lv < maxExp.Length && curExp >= maxExp[lv])
        {
            LevelUp();
        }

        UpdateExpUI();
    }

    private void UpdateExpUI()
    {
        curExpImg.fillAmount = ExpPercet();
    }

    private void LevelUp()
    {
        curExp -= maxExp[lv];
        ShowSelect();
        lv++;
    }

    private void ShowSelect()
    {
        Time.timeScale = 0;
        levelUpUI.Open();
    }


}
