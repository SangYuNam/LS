using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullFire : MonoBehaviour
{
    public float DropSpeed = 3.0f;
    public LayerMask crashMask;
    public LayerMask tileMask;
    void Update()
    {
        transform.Translate(Vector3.down * DropSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & crashMask) != 0)
        {
            gameObject.SetActive(false);
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                DodgeSlime.Inst.Life--;
            }
        }
        else if (((1 << collision.gameObject.layer) & tileMask) != 0)
            gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        transform.position = DodgeSlime.Inst.enermyGoblin.transform.position;
    }
}
