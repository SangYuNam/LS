using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlayer : RepProperty
{
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        myAnim.SetFloat("Dir", x);
        transform.Translate(transform.right * x * 2.0f * Time.deltaTime, Space.World);
    }
}
