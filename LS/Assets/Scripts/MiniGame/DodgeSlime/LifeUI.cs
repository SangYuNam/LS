using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    public GameObject[] HeartList;

    public void SetLife(int n)
    {
        for (int i = 0; i < HeartList.Length; ++i)
        {
            if (i < n)
                HeartList[i].SetActive(true);
            else
                HeartList[i].SetActive(false);
        }
    }
}
