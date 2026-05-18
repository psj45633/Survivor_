using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private UpgradeData[] allUpgrades;
    [SerializeField] private LevelUpSlot[] slots;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        Debug.Log("1");
        gameObject.SetActive(true);
        Debug.Log("2");
        Time.timeScale = 0f;

        List<UpgradeData> pool = new();

        foreach (var data in allUpgrades)
        {
            if (IsAlreadyOwned(data))
                continue;

            pool.Add(data);
        }
        Debug.Log($"pool count : {pool.Count}, slot count : {slots.Length}");
        int count = Mathf.Min(slots.Length, pool.Count);

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < count; i++)
        {
            UpgradeData selected = GetRandomUpgrade(pool);

            pool.Remove(selected);

            slots[i].gameObject.SetActive(true);
            slots[i].Set(selected, this);
        }
    }

    private UpgradeData GetRandomUpgrade(List<UpgradeData> pool)
    {
        if (pool == null || pool.Count == 0)
            return null;

        int totalWeight = 0;

        foreach (var data in pool)
        {
            totalWeight += data.weight;
        }

        int randomValue = Random.Range(0, totalWeight);

        foreach (var data in pool)
        {
            randomValue -= data.weight;

            if (randomValue <= 0)
                return data;
        }

        return pool[0];
    }

    public void ApplyUpgrade(UpgradeData data)
    {
        switch (data.type)
        {
            case UpgradeType.Attack:
                Player.Instance.Stats.AddAttack(data.value);
                break;

            case UpgradeType.AttackSpeed:
                Player.Instance.Stats.AddAttackSpeed(data.value);
                break;

            case UpgradeType.ChainExplosion:
                Player.Instance.Upgrade.chainExplosion = true;
                break;
        }

        Close();
    }

    private bool IsAlreadyOwned(UpgradeData data)
    {
        switch (data.type)
        {
            case UpgradeType.ChainExplosion:
                return Player.Instance.Upgrade.chainExplosion;
            //SO bool값 추가할때마다 여기에 추가
        }

        return false;
    }

    private void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
