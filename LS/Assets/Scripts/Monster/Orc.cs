using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : RepProperty
{
    float AttackCoolTime = 2.0f;

    public GameObject MonsterPlace;
    public GameObject Canvas;

    Coroutine coMoving = null;
    public Coroutine coAttacking = null;
    [SerializeField] enum State
    {
        Creat = 0, Normal, Battle, Dead
    }

    [SerializeField] State myState = State.Creat;
    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                coMoving = StartCoroutine(OrcMoving());
                break;
            case State.Battle:
                StopCoroutine(coMoving);
                GameManager.Instance.isBattle = true;
                StartCoroutine(UIShow());
                coAttacking = StartCoroutine(OrcAttacking());
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
        switch (myState)
        {
            case State.Normal:
                if (MonsterPlace.transform.position.x - gameObject.transform.position.x >= 0)
                {
                    ChangeState(State.Battle);
                    Debug.Log("오크 도착!");
                }
                    break;
            case State.Battle:
                break;
            case State.Dead:
                break;
            default:
                Debug.Log("입력받은 파라매터 값이 선언하지 않은 상태입니다");
                break;
        }
    }
    private void Start()
    {
        GameManager.Instance.Monster = this.gameObject;
        ChangeState(State.Normal);
    }
    void Update()
    {
        StateProcess();
    }

    IEnumerator OrcMoving()
    {
        while(true)
        {
            if (MonsterPlace.transform.position.x - gameObject.transform.position.x < 0)
            {
                GameManager.Instance.isStageMove = true;
                myAnim.SetBool("isMoving", true);
                transform.Translate(Vector2.left * Time.deltaTime * 2.0f);
            }
            else
            {
                GameManager.Instance.isStageMove = false;
                myAnim.SetBool("isMoving", false);
            }
            yield return null;
        }
    }
    IEnumerator OrcAttacking()
    {
        while(true)
        {
            yield return new WaitForSeconds(AttackCoolTime);
            myAnim.SetTrigger("isAttack");
        }
    }
    IEnumerator UIShow()
    {
        if(!Canvas.activeSelf) Canvas.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        if (Canvas.activeSelf) Canvas.SetActive(false);
    }
}
