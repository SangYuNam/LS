using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DodgePlayer : RepProperty
{
    public float mySpeed;
    private void Start()
    {
        mySpeed = DodgeSlime.Inst.PlayerSpeed;
    }
    void Update()
    {
        if (DodgeSlime.Inst.PlayerSpeed != mySpeed) mySpeed = DodgeSlime.Inst.PlayerSpeed;
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(transform.right * x * mySpeed * Time.deltaTime, Space.World);
        if (x != 0f) myAnim.SetBool("isMoving", true);
        else if (x == 0f) myAnim.SetBool("isMoving", false);
    }
}
