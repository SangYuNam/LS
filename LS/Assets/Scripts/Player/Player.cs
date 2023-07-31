using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : RepProperty, GameManager.IBattle
{
    [Header("UI")]
    public TextMeshProUGUI _TitleHP;
    public TextMeshProUGUI _TitleGold;

    [Header("플레이어 스텟")]
    public float PlayerATK = 20.0f;
    public float PlayerDEF = 0f;
    public float PlayerATKSpeed = 1.0f;
    public float PlayerMaxHP = 100f;
    public float curPlayerHP = 100f;
    public float BeforeGold = 100f;
    public float Gold = 100f;
    public float BeforeHP = 100f;

    [Header("스텟 레벨")]
    public int HPLV = 1;
    public int ATKLV = 1;
    public int DEFLV = 1;
    public int ATKSPEEDLV = 1;

    [Header("강화 비용")]
    public float HPCOST = 10.0f;
    public float ATKCOST = 10.0f;
    public float DEFCOST = 10.0f;
    public float ATKSPEEDCOST = 10.0f;

    Coroutine AttackCo = null;

    public UnityEvent DeadAraml = null;
    public GameObject LoseText = null;
    public GameObject DamageTextobj = null;
    public Canvas dmgCanvas = null;

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
                Debug.Log("입력받은 파라매터 값이 선언하지 않은 상태입니다");
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
        if (BeforeGold != Gold)
        {
            BeforeGold = Gold;
            _TitleGold.text = $"Gold : {Gold}G";
            UIManger.Instance.rtGold = Gold;
            Debug.Log("골드 업데이트 PLAYER");
        }
        if (BeforeHP != curPlayerHP)
        {
            BeforeHP = curPlayerHP;
            _TitleHP.text = $"Hp : {curPlayerHP}";
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
        GameObject DmgText = Instantiate(DamageTextobj, transform.position + Vector3.up, Quaternion.identity, dmgCanvas.transform);
        DmgText.GetComponent<DamageText>()._dmgtext.text = $"{(int)dmg - PlayerDEF}";

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
