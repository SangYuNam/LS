using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManger : Singleton<UIManger>
{
    public float bfGold = 100f;
    public float rtGold = 100f;

    public GameObject InfoWindow = null;
    public GameObject StrengthWindow = null;
    public GameObject DengeonWindow = null;
    public GameObject SettingWindow = null;

    [Header("플레이어 딜리게이트 함수")]
    public UnityEvent PlHpUp = null;
    public UnityEvent PlAtkUp = null;
    public UnityEvent PlDefUp = null;
    public UnityEvent PlAtkSpeedUp = null;

    [Header("현재 스테이지")]
    public TextMeshProUGUI _Stage;

    [Header("정보 스탯")]
    public TextMeshProUGUI _StatHP;
    public TextMeshProUGUI _StatATK;
    public TextMeshProUGUI _StatDEF;
    public TextMeshProUGUI _StatATKSPEED;
    public TextMeshProUGUI _StatGold;

    [Header("강화 스탯")]
    public TextMeshProUGUI _StrengthHP;
    public TextMeshProUGUI _StrengthATK;
    public TextMeshProUGUI _StrengthDEF;
    public TextMeshProUGUI _StrengthATKSPEED;

    [Header("스탯 레벨")]
    public TextMeshProUGUI _HPLV;
    public TextMeshProUGUI _ATKLV;
    public TextMeshProUGUI _DEFLV;
    public TextMeshProUGUI _ATKSPEEDLV;

    [Header("강화 비용")]
    public TextMeshProUGUI _HPCOST;
    public TextMeshProUGUI _ATKCOST;
    public TextMeshProUGUI _DEFCOST;
    public TextMeshProUGUI _ATKSPEEDCOST;

    GameObject _Player = null;

    [Header("화면전환 이미지")]
    public Image FadeImg; // 화면전환 이미지
    float curTime = 0f;
    float FadeTime = 1f;

    private void Awake()
    {
        _Player = GameManager.Instance.Player;
    }

    private void Update()
    {
        if(bfGold != rtGold)
        {
            bfGold = rtGold;
            _StatGold.text = $"GOLD : {rtGold}G";
            Debug.Log("골드 업데이트 UIMANAGER");
        }
    }

    public void WindowControl(GameObject window)
    {
        if(InfoWindow.activeSelf)
        {
            InfoWindow.SetActive(false);
        }
        else if(StrengthWindow.activeSelf)
        {
            StrengthWindow.SetActive(false);
        }
        else if(DengeonWindow.activeSelf)
        {
            DengeonWindow.SetActive(false);
        }
        window.SetActive(true);
    }


    public void Fade()
    {
        StartCoroutine(FadeEffect());
    }

    IEnumerator FadeEffect()
    {
        FadeImg.gameObject.SetActive(true);
        curTime = 0f;
        Color alpha = FadeImg.color;
        while (alpha.a < 1f)
        {
            curTime += Time.deltaTime / FadeTime;
            alpha.a = Mathf.Lerp(0, 1, curTime);
            FadeImg.color = alpha;
            yield return null;
        }

        curTime = 0f;

        GameManager.Instance.isStageMove = false; // 화면이 어두워 졌을때 배경이 처음 자리로 돌아가게끔
        _Stage.text = $"STAGE : {GameManager.Instance.Stage}";

        yield return new WaitForSeconds(1f);

        while (alpha.a > 0f)
        {
            curTime += Time.deltaTime / FadeTime;
            alpha.a = Mathf.Lerp(1, 0, curTime);
            FadeImg.color = alpha;
            yield return null;
        }
        yield return null;
    }

    public void HpUp()
    {
        if(rtGold >= _Player.GetComponent<Player>().HPCOST)
        {
            PlHpUp?.Invoke();
            _StatHP.text = $"체력 : {_Player.GetComponent<Player>().PlayerMaxHP}";
            _HPLV.text = $"LV : {_Player.GetComponent<Player>().HPLV}";
            _StrengthHP.text = $"체력 : {_Player.GetComponent<Player>().PlayerMaxHP}";
            _HPCOST.text = $"강화 \n{_Player.GetComponent<Player>().HPCOST}G";
        }        
    }

    public void AtkUp()
    {
        if(rtGold >= _Player.GetComponent<Player>().ATKCOST)
        {
            PlAtkUp?.Invoke();
            _StatATK.text = $"공격력 : {_Player.GetComponent<Player>().PlayerATK}";
            _ATKLV.text = $"LV : {_Player.GetComponent<Player>().ATKLV}";
            _StrengthATK.text = $"공격력 : {_Player.GetComponent<Player>().PlayerATK}";
            _ATKCOST.text = $"강화 \n{_Player.GetComponent<Player>().ATKCOST}G";
        }
    }
    
    public void DefUp()
    {
        if(rtGold >= _Player.GetComponent<Player>().DEFCOST)
        {
            PlDefUp?.Invoke();
            _StatDEF.text = $"방어력 : {_Player.GetComponent<Player>().PlayerDEF}";
            _DEFLV.text = $"LV : {_Player.GetComponent<Player>().DEFLV}";
            _StrengthDEF.text = $"방어력 : {_Player.GetComponent<Player>().PlayerDEF}";
            _DEFCOST.text = $"강화 \n{_Player.GetComponent<Player>().DEFCOST}G";
        }
    }

    public void AtkSpeedUp()
    {
        if (rtGold >= _Player.GetComponent<Player>().ATKSPEEDCOST)
        {
            PlAtkSpeedUp?.Invoke();
            _StatATKSPEED.text = $"공격속도 : {_Player.GetComponent<Player>().PlayerATKSpeed}s";
            _ATKSPEEDLV.text = $"LV : {_Player.GetComponent<Player>().ATKSPEEDLV}";
            _StrengthATKSPEED.text = $"공격속도 : {_Player.GetComponent<Player>().PlayerATKSpeed}s";
            _ATKSPEEDCOST.text = $"강화 \n{_Player.GetComponent<Player>().ATKSPEEDCOST}G";
        }
    }
}
