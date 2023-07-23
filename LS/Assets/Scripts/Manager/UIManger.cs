using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManger : MonoBehaviour
{
    public GameObject InfoWindow = null;
    public GameObject StrengthWindow = null;
    public GameObject DengeonWindow = null;
    public GameObject SettingWindow = null;

    public UnityEvent GMHpUp = null;
    public UnityEvent GMAtkUp = null;
    public UnityEvent GMDefUp = null;
    public UnityEvent GMAtkSpeedUp = null;

    [Header("���� ��������")]
    public TextMeshProUGUI _Stage;

    [Header("���� ����")]
    public TextMeshProUGUI _StatHP;
    public TextMeshProUGUI _StatATK;
    public TextMeshProUGUI _StatDEF;
    public TextMeshProUGUI _StatATKSPEED;

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
        GMHpUp?.Invoke();
        _StatHP.text = $"ü�� : {_Player.GetComponent<Player>().PlayerMaxHP}";
        _HPLV.text = $"LV : ";
        _StrengthHP.text = $"ü�� : {_Player.GetComponent<Player>().PlayerMaxHP}";
        _HPCOST.text = $"��ȭ G";
    }

    public void AtkUp()
    {
        GMAtkUp?.Invoke();
    }
    
    public void DefUp()
    {
        GMDefUp?.Invoke();
    }

    public void AtkSpeedUp()
    {
        GMAtkSpeedUp?.Invoke();
    }
}
