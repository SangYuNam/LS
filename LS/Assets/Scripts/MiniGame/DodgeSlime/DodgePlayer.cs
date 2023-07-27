using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgePlayer : RepProperty
{
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        transform.Translate(transform.right * x * 2.0f * Time.deltaTime, Space.World);
        if (x != 0f) myAnim.SetBool("isMoving", true);
        else if (x == 0f) myAnim.SetBool("isMoving", false);
    }
}
