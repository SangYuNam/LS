using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMucus : MonoBehaviour
{
    public void Createmucus()
    {
        ObjectPool.Instance.GetObject(0);
    }
}
