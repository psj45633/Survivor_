using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ObjectPool pool;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }









}
