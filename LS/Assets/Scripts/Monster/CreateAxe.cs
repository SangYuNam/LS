using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAxe : MonoBehaviour
{
    public void Createaxe()
    {
        ObjectPool.Instance.GetObject(1);
    }
}
