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

        }
    }
}
