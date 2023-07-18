using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mucus : RepProperty
{
    [SerializeField] GameObject Monster = null;
    void Start()
    {
        
    }

    private void Awake()
    {
        Monster = GameObject.Find("Orc");
    }

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
            Destroy(gameObject);
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void HitDamage()
    {
        GameManager.Instance.Monster.GetComponent<GameManager.IBattle>().OnTakeDamage(20.0f);
    }
}
