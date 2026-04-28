using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OpenOption()
    {
        Debug.Log("옵션 열기");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
