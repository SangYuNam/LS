using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManger : MonoBehaviour
{
    public GameObject InfoWindow = null;
    public GameObject StrengthWindow = null;
    public GameObject ShopWindow = null;
    public GameObject SettingWindow = null;
    public TextMeshProUGUI _Stage;

    public Image FadeImg; // 화면전환 이미지
    float curTime = 0f;
    float FadeTime = 1f;

    public void ShowWindow(GameObject window)
    {
        if (!window.activeSelf) window.SetActive(true);
    }
    public void OffWindow(GameObject window)
    {
        if (window.activeSelf) window.SetActive(false);
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
        GameManager.Instance.Stage += 1; // 스테이지 증가
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
}
