using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : RepProperty, GameManager.IBattle
{
    [Header("�÷��̾� ����")]
    public float PlayerATK = 20.0f;
    public float PlayerDEF = 0f;
    public float PlayerATKSpeed = 1.0f;
    public float PlayerMaxHP = 100f;
    public float curPlayerHP = 100f;
    public float BeforGold = 100f;
    public float Gold = 100f;

    [Header("���� ����")]
    public int HPLV = 1;
    public int ATKLV = 1;
    public int DEFLV = 1;
    public int ATKSPEEDLV = 1;

    [Header("��ȭ ���")]
    public float HPCOST = 10.0f;
    public float ATKCOST = 10.0f;
    public float DEFCOST = 10.0f;
    public float ATKSPEEDCOST = 10.0f;

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
                if (AttackCo != null)
                {
                    StopCoroutine(AttackCo);
                    curPlayerHP = PlayerMaxHP;
                }
                break;
            case State.Battle:
                AttackCo = StartCoroutine(Attacking());
                break;
            case State.Dead:
                StopAllCoroutines();
                StartCoroutine(Deading());
                break;
            default:
                Debug.Log("�Է¹��� �Ķ���� ���� �������� ���� �����Դϴ�");
                break;
        }
    }
    void StateProcess()
    {
        switch(myState)
        {
            case State.Normal:
                if (GameManager.Instance.isStageMove)
                    myAnim.SetBool("isMoving", true);
                if (GameManager.Instance.isBattle)
                    ChangeState(State.Battle);
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
                Debug.Log("�Է¹��� �Ķ���� ���� �������� ���� �����Դϴ�");
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
        if (BeforGold != Gold)
        {
            BeforGold = Gold;
            UIManger.Instance.rtGold = Gold;
            Debug.Log("��� ������Ʈ PLAYER");
        }
    }

    IEnumerator Attacking()
    {
        while(true)
        {
            yield return new WaitForSeconds(PlayerATKSpeed);

            myAnim.SetTrigger("isAttack");            
        }
    }
    public bool isLive
    {
        get => myState != State.Dead;
    }

    public void OnTakeDamage(float dmg)
    {
        curPlayerHP -= dmg - PlayerDEF;
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

    public void PlayerHPUP()
    {
        Gold -= HPCOST;
        PlayerMaxHP += PlayerMaxHP / 10;
        if (PlayerMaxHP - curPlayerHP >= PlayerMaxHP / 10)
            curPlayerHP += PlayerMaxHP / 10;
        else if (PlayerMaxHP - curPlayerHP < PlayerMaxHP / 10 && PlayerMaxHP - curPlayerHP > 0)
            curPlayerHP += PlayerMaxHP - curPlayerHP;
        ++HPLV;
        HPCOST += 1;
    }
    public void PlayerATKUP()
    {
        Gold -= ATKCOST;
        PlayerATK += PlayerATK / 5;
        ++ATKLV;
        ATKCOST += 1;
    }
    public void PlayerDEFUP()
    {
        Gold -= DEFCOST;
        PlayerDEF += 1.0f;
        ++DEFLV;
        DEFCOST += 1;
    }
    public void PlayerATKSPEEDUP()
    {
        Gold -= ATKSPEEDCOST;
        PlayerATKSpeed = PlayerATKSpeed * 0.9f;
        ++ATKSPEEDLV;
        ATKSPEEDCOST += 1;
    }

    public void resetHP()
    {
        curPlayerHP = PlayerMaxHP;
    }

}
