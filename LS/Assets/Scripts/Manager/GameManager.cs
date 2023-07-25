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

    [Header("참조 오브젝트")]
    public GameObject Player = null;
    public GameObject Monster = null;
    public GameObject CreatePos = null;

    public UnityEvent MonsterChangeState = null;

    public int Stage = 1;

    [Header("몬스터 스텟(임시)")]
    public float MonsterATK = 20.0f;
    public float MonsterMaxHP = 100f;
    public float curMonsterHP = 100f;


    private void Update()
    {
        if(!Monster.activeSelf)
        {
            Player.GetComponent<Player>().Gold += Stage * 10.0f;
            resetObject();
        }
    }

    void resetObject()
    {
        Monster.transform.position = CreatePos.transform.position;
        Monster.SetActive(true);
        MonsterChangeState?.Invoke();
        StageControl();
        curMonsterHP = MonsterMaxHP;
        Player.GetComponent<Player>().resetHP();
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
            MonsterATK += MonsterATK * 0.1f;
            MonsterMaxHP += MonsterMaxHP * 0.1f;
        }
        else
        {
            isStageLose = false;
        }
    }
}
