using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    public GameObject InfoWindow = null;
    public GameObject StrengthWindow = null;
    public GameObject ShopWindow = null;
    public GameObject SettingWindow = null;

    public void ShowWindow(GameObject window)
    {
        if (!window.activeSelf) window.SetActive(true);
    }
    public void OffWindow(GameObject window)
    {
        if (window.activeSelf) window.SetActive(false);
    }
}
