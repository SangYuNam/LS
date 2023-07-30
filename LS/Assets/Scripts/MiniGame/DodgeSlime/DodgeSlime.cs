using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeSlime : MonoBehaviour
{
    public static DodgeSlime Inst = null;
    public enum State
    {
        Create, Title, Play, GameOver
    }
    public State myState = State.Create;
    int myLife = 3;
    public int Life
    {
        get => myLife;
        set
        {
            myLife = Mathf.Clamp(value, 0, 5);
            myLifeUI.SetLife(myLife);
            if (myLife == 0)
            {
                ChangeState(State.GameOver);
            }
        }
    }
    int myScore = 0;

    public int Score
    {
        get => myScore;
        set
        {
            myScore = value;
            myScoreUI.text = myScore.ToString();
        }
    }

    public float PlayerSpeed = 2.0f;
    public DodgePlayer myPlayer;
    public LifeUI myLifeUI;
    public Goblin enermyGoblin;
    public GameObject myTitleUI;
    public GameObject myGameOverUI;
    public TMPro.TMP_Text myScoreUI;
    public TMPro.TMP_Text LastScore;

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Title:
                myGameOverUI.SetActive(false);
                myTitleUI.SetActive(true);
                myPlayer.gameObject.SetActive(false);
                enermyGoblin.StopDrop();
                break;
            case State.Play:
                myGameOverUI.SetActive(false);
                myTitleUI.SetActive(false);
                myPlayer.gameObject.SetActive(true);
                enermyGoblin.StartDrop();
                myLifeUI.SetLife(myLife);
                break;
            case State.GameOver:
                myGameOverUI.SetActive(true);
                enermyGoblin.StopDrop();
                myPlayer.gameObject.SetActive(false);
                LastScore.text = $"최종 점수 : {Score}";
                break;
            default:
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Title:
                if (Input.anyKey)
                {
                    ChangeState(State.Play);
                }
                break;
            case State.Play:
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        Inst = this;
    }

    void Start()
    {
        ChangeState(State.Title);
    }

    void Update()
    {
        StateProcess();
    }

    public void OnRetry()
    {
        Score = 0;
        Life = 3;
        ChangeState(State.Play);
    }
}
