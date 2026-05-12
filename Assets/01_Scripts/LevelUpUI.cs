using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private GameObject levelUpUI;
    [SerializeField] private GameObject[] choices;



    public void Open()
    {
        levelUpUI.SetActive(true);
    }
}
