using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : RepProperty
{
    [SerializeField] GameObject Player = null;

    void Update()
    {
        if (Mathf.Abs(transform.position.x - Player.transform.position.x) > 1f)
        {
            transform.Translate(Vector2.left * Time.deltaTime * 2.0f);
        }
        else
        {
            gameObject.SetActive(false);
            HitDamage();
        }

        if (!GameManager.Instance.isStageMove)
            gameObject.SetActive(false);
    }

    public void HitDamage()
    {
        GameManager.Instance.Player.GetComponent<GameManager.IBattle>().OnTakeDamage(20.0f);
    }

    private void OnEnable()
    {
        Player = GameManager.Instance.Player;
        transform.position = GameManager.Instance.Monster.transform.position + new Vector3(-0.5f, 0.5f, 0f);
    }
}
