using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour
{
    public GameObject MonsterPlace;
    public GameObject Canvas;
    Animator myAnim;
    void Start()
    {
       myAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!Canvas.activeSelf)
            {
                Canvas.SetActive(true);
            }
            StartCoroutine(OrcMoving());
        }
    }

    IEnumerator OrcMoving()
    {
        while(true)
        {
            if (MonsterPlace.transform.position.x - gameObject.transform.position.x < 0)
            {
                myAnim.SetBool("isMoving", true);
                transform.Translate(Vector2.left * Time.deltaTime * 2.0f);
            }
            else
            {
                myAnim.SetBool("isMoving", false);
            }
            yield return null;
        }
    }
}
