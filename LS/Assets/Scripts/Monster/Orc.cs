using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;

public class Orc : RepProperty, GameManager.IBattle
{
    float AttackCoolTime = 3.0f;

    public GameObject MonsterPlace;
    public GameObject ShowTextObj;
    public TextMeshProUGUI _ShowTxt;
    public UnityEvent DeathAlarm = null;

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
                StopAllCoroutines();
                coMoving = StartCoroutine(OrcMoving());
                break;
            case State.Battle:
                StopCoroutine(coMoving);
                GameManager.Instance.isBattle = true;
                StartCoroutine(TextShowing("오크가 등장했다!"));
                coAttacking = StartCoroutine(OrcAttacking());
                break;
            case State.Dead:
                StopAllCoroutines();
                GameManager.Instance.isBattle = false;
                StartCoroutine(Deading());
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

    public void StageMoveEndAlarm()
    {
        ChangeState(State.Normal);
    }

    private void Start()
    {
        GameManager.Instance.Monster = this.gameObject;
        ChangeState(State.Normal);
    }
    void Update()
    {
        StateProcess();
        if (Input.GetKeyDown(KeyCode.Space))
        {
                ChangeState(State.Dead);
        }
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
    IEnumerator TextShowing(string txt)
    {
        _ShowTxt.text = txt;
        if(!ShowTextObj.activeSelf) ShowTextObj.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        if (ShowTextObj.activeSelf) ShowTextObj.SetActive(false);
    }
    IEnumerator Deading()
    {
        myAnim.SetTrigger("isDead");
        StartCoroutine(TextShowing("오크를 해치웠다!"));
        yield return new WaitForSeconds(2.0f);
        DeathAlarm?.Invoke();
        this.gameObject.SetActive(false);
    }

    public bool isLive
    {
        get => myState != State.Dead;
    }

    public void OnTakeDamage(float dmg)
    {
        GameManager.Instance.curMonsterHP -= dmg;
        myAnim.SetTrigger("isDamage");       

        if(!Mathf.Approximately(GameManager.Instance.curMonsterHP, 0f))
        {
            myAnim.SetTrigger("isDamage");
        }
        else
        {
            ChangeState(State.Dead);
        }
    }


    /*public void OnTakeDamage(float dmg)
    {
        GameObject obj = Instantiate(Resources.Load("UI/DmgText"), TextArea) as GameObject;
        obj.GetComponent<DamageText>().ChangeTextColor(dmg);
        curHp -= dmg;
        if (dmg < 0)
        {
            myAnim.SetTrigger("OnHealColor");
            return;
        }
        myAnim.SetTrigger("OnDamageColor"); // 피격시 이미지의 색상을 바꿔주도록 Animator에서 설정

        if (!Mathf.Approximately(curHp, 0f))
        {
            if (!myAnim.GetBool("isAttacking"))
                myAnim.SetTrigger("OnDamage");  // 공격중엔 애니메이션 호출 안 함
        }
        else
        {
            Collider2D[] colList = transform.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D col in colList) col.enabled = false;
            StopAllCoroutines();
            StartCoroutine(Death());
        }
    }*/
}
