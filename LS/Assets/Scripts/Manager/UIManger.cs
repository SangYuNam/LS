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


    public GameObject MinigameCamera = null;
    public GameObject MinigameCanvas = null;
    public GameObject DodgeManager = null;
    public GameObject DodgePlayer = null;
    public GameObject DodgeGoblin = null;

    [Header("�÷��̾� ��������Ʈ �Լ�")]
    public UnityEvent PlHpUp = null;
    public UnityEvent PlAtkUp = null;
    public UnityEvent PlDefUp = null;
    public UnityEvent PlAtkSpeedUp = null;

    [Header("���� ��������")]
    public TextMeshProUGUI _Stage;

    [Header("���� ����")]
    public TextMeshProUGUI _StatHP;
    public TextMeshProUGUI _StatATK;
    public TextMeshProUGUI _StatDEF;
    public TextMeshProUGUI _StatATKSPEED;
    public TextMeshProUGUI _StatGold;

    [Header("��ȭ ����")]
    public TextMeshProUGUI _StrengthHP;
    public TextMeshProUGUI _StrengthATK;
    public TextMeshProUGUI _StrengthDEF;
    public TextMeshProUGUI _StrengthATKSPEED;

    [Header("���� ����")]
    public TextMeshProUGUI _HPLV;
    public TextMeshProUGUI _ATKLV;
    public TextMeshProUGUI _DEFLV;
    public TextMeshProUGUI _ATKSPEEDLV;

    [Header("��ȭ ���")]
    public TextMeshProUGUI _HPCOST;
    public TextMeshProUGUI _ATKCOST;
    public TextMeshProUGUI _DEFCOST;
    public TextMeshProUGUI _ATKSPEEDCOST;

    GameObject _Player = null;

    [Header("ȭ����ȯ �̹���")]
    public Image FadeImg; // ȭ����ȯ �̹���
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
            Debug.Log("��� ������Ʈ UIMANAGER");
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

        GameManager.Instance.isStageMove = false; // ȭ���� ��ο� ������ ����� ó�� �ڸ��� ���ư��Բ�
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
            _StatHP.text = $"ü�� : {_Player.GetComponent<Player>().PlayerMaxHP}";
            _HPLV.text = $"LV : {_Player.GetComponent<Player>().HPLV}";
            _StrengthHP.text = $"ü�� : {_Player.GetComponent<Player>().PlayerMaxHP}";
            _HPCOST.text = $"��ȭ \n{_Player.GetComponent<Player>().HPCOST}G";
        }        
    }

    public void AtkUp()
    {
        if(rtGold >= _Player.GetComponent<Player>().ATKCOST)
        {
            PlAtkUp?.Invoke();
            _StatATK.text = $"���ݷ� : {_Player.GetComponent<Player>().PlayerATK}";
            _ATKLV.text = $"LV : {_Player.GetComponent<Player>().ATKLV}";
            _StrengthATK.text = $"���ݷ� : {_Player.GetComponent<Player>().PlayerATK}";
            _ATKCOST.text = $"��ȭ \n{_Player.GetComponent<Player>().ATKCOST}G";
        }
    }
    
    public void DefUp()
    {
        if(rtGold >= _Player.GetComponent<Player>().DEFCOST)
        {
            PlDefUp?.Invoke();
            _StatDEF.text = $"���� : {_Player.GetComponent<Player>().PlayerDEF}";
            _DEFLV.text = $"LV : {_Player.GetComponent<Player>().DEFLV}";
            _StrengthDEF.text = $"���� : {_Player.GetComponent<Player>().PlayerDEF}";
            _DEFCOST.text = $"��ȭ \n{_Player.GetComponent<Player>().DEFCOST}G";
        }
    }

    public void AtkSpeedUp()
    {
        if (rtGold >= _Player.GetComponent<Player>().ATKSPEEDCOST)
        {
            PlAtkSpeedUp?.Invoke();
            _StatATKSPEED.text = $"���ݼӵ� : {_Player.GetComponent<Player>().PlayerATKSpeed}s";
            _ATKSPEEDLV.text = $"LV : {_Player.GetComponent<Player>().ATKSPEEDLV}";
            _StrengthATKSPEED.text = $"���ݼӵ� : {_Player.GetComponent<Player>().PlayerATKSpeed}s";
            _ATKSPEEDCOST.text = $"��ȭ \n{_Player.GetComponent<Player>().ATKSPEEDCOST}G";
        }
    }

    public void MinigameStart()
    {
        StartCoroutine(MinigameUIShowing());
    }
    public void MinigameEnd()
    {
        StartCoroutine(MinigameOff());
    }

    IEnumerator MinigameUIShowing()
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

        MinigameCamera.SetActive(true);
        MinigameCanvas.SetActive(true);
        DodgeManager.SetActive(true);
        DodgePlayer.SetActive(true);
        DodgeGoblin.SetActive(true);

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

    IEnumerator MinigameOff()
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

        MinigameCamera.SetActive(false);
        MinigameCanvas.SetActive(false);
        DodgeManager.SetActive(false);
        DodgePlayer.SetActive(false);
        DodgeGoblin.SetActive(false);

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
}
