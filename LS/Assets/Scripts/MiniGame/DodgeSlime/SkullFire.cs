using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullFire : Item
{
    private void Start()
    {
        SetType(ItemType.Fire);
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
