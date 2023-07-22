using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        Application.targetFrameRate = 30;
    }

    public bool isStageLose = false;
    public bool isBattle = false;
    public bool isStageMove = false;

    public GameObject Player = null;
    public GameObject Monster = null;
    public GameObject CreatePos = null;

    public UnityEvent MonsterChangeState = null;

    public int Stage = 1;
    public int Gold = 0;

    public float PlayerATK = 20.0f;
    public float PlayerDEF = 0f;
    public float PlayerATKSpeed = 1f;
    public float PlayerMaxHP = 100f;
    public float curPlayerHP = 100f;

    public float MonsterATK = 20.0f;
    public float MonsterMaxHP = 100f;
    public float curMonsterHP = 100f;


    private void Update()
    {
        if(!Monster.activeSelf)
        {
            resetMonster();
        }
    }

    void resetMonster()
    {
        Monster.transform.position = CreatePos.transform.position;
        Monster.SetActive(true);
        MonsterChangeState?.Invoke();
        StageControl();
        curMonsterHP = MonsterMaxHP;
    }

    public interface IBattle
    {
        void OnTakeDamage(float dmg);
        bool isLive { get; }
    }

    public void StageControl()
    {
        if(!isStageLose)
        {
            Stage += 1;
            MonsterATK += MonsterATK * 0.5f;
            MonsterMaxHP += MonsterMaxHP * 0.25f;
        }
        else
        {
            isStageLose = false;
        }
    }
}
