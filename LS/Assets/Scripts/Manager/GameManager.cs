using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public bool isBattle = false;
    public bool isStageMove = false;

    public GameObject Player = null;
    public GameObject Monster = null;
    public GameObject CreatePos = null;

    public UnityEvent MonsterChangeState = null;

    public int Stage = 1;
    public int PlayerHP;
    public int curPlayerHP;
    public int MonsterHP;
    public int curMonsterHP;

    private void Update()
    {
        resetMonster();
    }

    void resetMonster()
    {
        if (!Monster.activeSelf)
        {
            Monster.transform.position = CreatePos.transform.position;
            Monster.SetActive(true);
            MonsterChangeState?.Invoke();
        }
    }
}
