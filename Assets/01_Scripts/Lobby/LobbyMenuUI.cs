using UnityEngine;

public class LobbyMenuUI : MonoBehaviour
{
    [Header("Armoury Objects")]
    [SerializeField] private GameObject[] menuObjs;

    private void Start()
    {
        HideAllArmoury();
        ShowUI(2);
    }

    public void ShowUI(int index)
    {
        if (index < 0 || index >= menuObjs.Length)
        {
            Debug.LogWarning("잘못된 Armoury index: " + index);
            return;
        }

        HideAllArmoury();

        menuObjs[index].SetActive(true);
    }

    private void HideAllArmoury()
    {
        for (int i = 0; i < menuObjs.Length; i++)
        {
            menuObjs[i].SetActive(false);
        }
    }
}