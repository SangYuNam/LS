using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [SerializeField] GameObject[] Objects;

    List<GameObject>[] Pools;

    private void Awake()
    {
        Pools = new List<GameObject>[Objects.Length];

        for (int i = 0; i < Pools.Length; i++)
        {
            Pools[i] = new List<GameObject>();
        }
    }

    public GameObject GetObject(int Index)
    {
        GameObject Select = null;

        foreach(GameObject item in Pools[Index])
        {
            if(!item.activeSelf)
            {
                Select = item;
                Select.SetActive(true);
                break;
            }
        }

        if(!Select)
        {
            Select = Instantiate(Objects[Index], transform);
            Pools[Index].Add(Select);
        }

        return Select;
    }
}
