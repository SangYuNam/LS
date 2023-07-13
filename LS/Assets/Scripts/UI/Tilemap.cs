using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap : RepProperty
{
    void Update()
    {
        if(GameManager.Instance.isStageMove)
            myAnim.SetBool("MovingMap", true);
        else
            myAnim.SetBool("MovingMap", false);
    }
}
