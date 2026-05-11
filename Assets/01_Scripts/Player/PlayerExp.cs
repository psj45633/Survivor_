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

    private void Start()
    {
        Init();
    }

    private float expPercet()
    {
        return curExp / maxExp[lv];
    }


    private void Init()
    {
        lv = 0;
        curExp = 0;
    }

    public void AddExp(int exp)
    {
        if (lv > maxExp.Length) return;

        curExp += exp;
        if (curExp >= maxExp[lv])
        {
            curExp -= maxExp[lv];
            LevelUp();
        }

        UpdateExpUI();
    }

    private void UpdateExpUI()
    {

    }

    private void LevelUp()
    {
        lv++;

        //게임 멈추고 선택지 띄우기
    }


}
