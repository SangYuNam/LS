using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isBattle = false;
    public bool isStageMove = false;

    public GameObject Player = null;
    public GameObject Monster = null;

    public int Stage;
    public int PlayerHP;
    public int curPlayerHP;
    public int MonsterHP;
    public int curMonsterHP;
}
