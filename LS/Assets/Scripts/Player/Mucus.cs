using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mucus : RepProperty
{
    [SerializeField] GameObject Monster = null;

    void Update()
    {
        if(Mathf.Abs(transform.position.x - Monster.transform.position.x) > 1f)
        {
            transform.Translate(Vector2.right * Time.deltaTime * 2.0f);
        }
        else
        {            
            myAnim.SetTrigger("isContact");
        }

        if (!GameManager.Instance.isStageMove)
            gameObject.SetActive(false);
    }

    public void Oncontact()
    {
        gameObject.SetActive(false);
    }

    public void HitDamage()
    {
        GameManager.Instance.Monster.GetComponent<GameManager.IBattle>().OnTakeDamage(GameManager.Instance.Player.GetComponent<Player>().PlayerATK);
    }

    private void OnEnable()
    {
        if(!Monster)
        {
            Monster = GameManager.Instance.Monster;
        }
        transform.position = GameManager.Instance.Player.transform.position + new Vector3(0.5f, 0.5f, 0f);
    }
}
