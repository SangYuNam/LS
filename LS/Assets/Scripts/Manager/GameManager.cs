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
    public float PlayerHP = 100f;
    public float curPlayerHP = 100f;
    public float MonsterHP = 100f;
    public float curMonsterHP = 100f;

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
            curMonsterHP = MonsterHP;
        }
    }

    public interface IBattle
    {
        void OnTakeDamage(float dmg);
        bool isLive { get; }
    }
}
