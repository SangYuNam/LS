using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string Ingame = "PlayScene";
    public string Settingmenu;

    public void ClickStart()
    {
        SceneManager.LoadScene(Ingame);
    }

    public void ClickSetting()
    {
        Debug.Log("·Îµå");
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}