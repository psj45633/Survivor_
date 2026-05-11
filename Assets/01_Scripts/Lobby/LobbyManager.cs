using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
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
