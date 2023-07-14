using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Orc : RepProperty
{
    float AttackCoolTime = 2.0f;

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
                StartCoroutine(TextShowing("��ũ�� �����ߴ�!"));
                coAttacking = StartCoroutine(OrcAttacking());
                break;
            case State.Dead:
                StopAllCoroutines();
                GameManager.Instance.isBattle = false;
                StartCoroutine(Deading());
                break;
            default:
                Debug.Log("�Է¹��� �Ķ���� ���� �������� ���� �����Դϴ�");
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
                    Debug.Log("��ũ ����!");
                }
                    break;
            case State.Battle:
                break;
            case State.Dead:
                break;
            default:
                Debug.Log("�Է¹��� �Ķ���� ���� �������� ���� �����Դϴ�");
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
        StartCoroutine(TextShowing("��ũ�� ��ġ����!"));
        yield return new WaitForSeconds(2.0f);
        DeathAlarm?.Invoke();
        this.gameObject.SetActive(false);
    }
}
