using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float moveSpeed;
    public TextMeshProUGUI _dmgtext = null;

    private void Awake()
    {
        moveSpeed = 2.0f;
        _dmgtext = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartCoroutine(Movingtext());
        StartCoroutine(Destroying());
    }    

    IEnumerator Movingtext()
    {
        while(true)
        {
            transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
            yield return null;
        }
    }

    IEnumerator Destroying()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
