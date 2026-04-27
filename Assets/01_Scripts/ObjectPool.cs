using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public PrefabsAndSize[] prefabsAndSize;

    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabsAndSize.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();

            GameObject prefab = prefabsAndSize[i].prefab;
            int size = prefabsAndSize[i].poolSize;
            GameObject parent = prefabsAndSize[i].parent;

            // 미리 생성
            for (int j = 0; j < size; j++)
            {
                GameObject obj = Instantiate(prefab, parent.transform);
                obj.SetActive(false);
                pools[i].Add(obj);
            }
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                return select;
            }
        }

        // 부족하면 추가 생성
        GameObject prefab = prefabsAndSize[index].prefab;

        select = Instantiate(prefab, transform);
        pools[index].Add(select);

        return select;
    }
}

[System.Serializable]
public class PrefabsAndSize
{
    public GameObject prefab;
    public int poolSize;
    public GameObject parent;
}