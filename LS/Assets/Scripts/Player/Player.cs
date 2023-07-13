using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RepProperty
{
    float AttackCoolTime = 2.0f;

    Coroutine AttackCo = null;
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
    void Start()
    {
        ChangeState(State.Normal);
        GameManager.Instance.Player = this.gameObject;
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
}
