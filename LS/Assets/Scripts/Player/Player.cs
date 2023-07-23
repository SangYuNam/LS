using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : RepProperty, GameManager.IBattle
{
    public float PlayerATK = 20.0f;
    public float PlayerDEF = 0f;
    public float PlayerATKSpeed = 1.0f;
    public float PlayerMaxHP = 100f;
    public float curPlayerHP = 100f;

    float AttackCoolTime = 1.0f;

    Coroutine AttackCo = null;

    public UnityEvent DeadAraml = null;
    public GameObject LoseText = null;

    [SerializeField] enum State
    {
        Creat = 0, Normal, Battle, Dead
    }
    [SerializeField] State myState = State.Creat;
    void ChangeState(State s)
    {
        if(myState == s) return;
        myState = s;
        switch(myState)
        {
            case State.Normal:
                if (AttackCo != null) StopCoroutine(AttackCo);
                break;
            case State.Battle:
                AttackCo = StartCoroutine(Attacking());
                break;
            case State.Dead:
                StopAllCoroutines();
                StartCoroutine(Deading());
                break;
            default:
                Debug.Log("입력받은 파라매터 값이 선언하지 않은 상태입니다");
                break;
        }
    }
    void StateProcess()
    {
        switch(myState)
        {
            case State.Normal:
                if (GameManager.Instance.isBattle)
                    ChangeState(State.Battle);
                if (GameManager.Instance.isStageMove)
                    myAnim.SetBool("isMoving", true);
                else
                    myAnim.SetBool("isMoving", false);
                break;
            case State.Battle:
                if (!GameManager.Instance.isBattle)
                    ChangeState(State.Normal);
                break;
            case State.Dead:
                break;
            default:
                Debug.Log("입력받은 파라매터 값이 선언하지 않은 상태입니다");
                break;
        }
    }
    private void Awake()
    {
        GameManager.Instance.Player = this.gameObject;
    }
    void Start()
    {
        ChangeState(State.Normal);        
    }

    void Update()
    {
        StateProcess();
    }

    IEnumerator Attacking()
    {
        while(true)
        {
            yield return new WaitForSeconds(AttackCoolTime);

            myAnim.SetTrigger("isAttack");            
        }
    }
    public bool isLive
    {
        get => myState != State.Dead;
    }

    public void OnTakeDamage(float dmg)
    {
        curPlayerHP -= dmg;
        myAnim.SetTrigger("isDamage");

        if (curPlayerHP > 0.0f)
        {
            myAnim.SetTrigger("isDamage");
        }
        else
        {
            ChangeState(State.Dead);
        }
    }

    IEnumerator Deading()
    {
        myAnim.SetTrigger("isDead");
        GameManager.Instance.isStageLose = true;
        DeadAraml?.Invoke();
        yield return new WaitForSeconds(1);
        if (!LoseText.activeSelf) LoseText.SetActive(true);
    }

}
