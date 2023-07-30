using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : Item
{
    private void Start()
    {
        SetType(ItemType.Buff);
    }
    void Update()
    {
        Drop();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Takeability(collision);
    }
    private void OnEnable()
    {
        PosSetting(DodgeSlime.Inst.enermyGoblin.transform);
    }
}
