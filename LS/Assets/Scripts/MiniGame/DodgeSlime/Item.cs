using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        None, Fire, Score, Buff, Debuff, Goblin
    }

    public LayerMask crashMask;
    public LayerMask tileMask;
    protected float DropSpeed = 3.0f;
    public ItemType myType = ItemType.None;
    public GameObject Goblinobj = null;

    public void SetType(ItemType type)
    {
        myType = type;
    }

    protected void Drop()
    {
        transform.Translate(Vector3.down * DropSpeed * Time.deltaTime);
    }
    protected void PosSetting(Transform enermy)
    {
        transform.position = enermy.transform.position;
    }
    protected void Takeability(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & crashMask) != 0)
        {
            gameObject.SetActive(false);
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                switch (myType)
                {
                    case ItemType.Fire:
                        DodgeSlime.Inst.Life--;
                        break;
                    case ItemType.Score:
                        DodgeSlime.Inst.Score += 100;
                        break;
                    case ItemType.Buff:
                        DodgeSlime.Inst.PlayerSpeed += 0.3f;
                        break;
                    case ItemType.Debuff:
                        DodgeSlime.Inst.PlayerSpeed -= 0.3f;
                        break;
                    case ItemType.Goblin:
                        if (Goblinobj) Instantiate(Goblinobj.gameObject, new Vector3(0, -31, 0), Quaternion.identity); 
                        break;
                }
            }
        }
        else if (((1 << collision.gameObject.layer) & tileMask) != 0)
            gameObject.SetActive(false);
    }
}
