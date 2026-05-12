using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ObjectPool pool;

    public static Action<int, int> OnKillEnemy;
    public int killCount;
    public int gold;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        
    }

    private void Init()
    {
        killCount = 0;
    }


    private void OnEnable()
    {
        OnKillEnemy += HandleKillEnemy;
    }

    private void OnDisable()
    {
        OnKillEnemy -= HandleKillEnemy;
    }

    private void HandleKillEnemy(int exp, int gold)
    {
        killCount++;
        Player.Instance.exp.AddExp(exp);
        this.gold += gold;
    }





}
