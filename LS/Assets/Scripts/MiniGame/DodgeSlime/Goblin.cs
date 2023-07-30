using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goblin : RepProperty
{
    public Vector2 MoveArea;
    float myDir = 0.0f;
    public float moveSpeed = 2.0f;
    public UnityEvent CreateFire = null;

    void Start()
    {
        myAnim.SetBool("isMoving", true);
        switch (Random.Range(0, 2))
        {
            case 0:
                myDir = -1.0f;
                break;
            case 1:
                myDir = 1.0f;
                break;
            default:
                break;
        }
    }

    public void StartDrop()
    {
        StartCoroutine(Dropping(2.0f));
    }

    public void StopDrop()
    {
        StopAllCoroutines();
    }

    void Update()
    {
        transform.Translate(Vector2.right * myDir * moveSpeed * Time.deltaTime);
        if(myDir > 0.0f)
        {
            myRenderer.flipX = false;
        }
        else
        {
            myRenderer.flipX = true;
        }
        if (transform.position.x <= MoveArea.x)
        {            
            myDir *= -1.0f;
            transform.position = new Vector3(MoveArea.x, transform.position.y, transform.position.z);
        }
        if (transform.position.x >= MoveArea.y)
        {            
            myDir *= -1.0f;
            transform.position = new Vector3(MoveArea.y, transform.position.y, transform.position.z);
        }
    }

    IEnumerator Dropping(float delay)
    {
        while (true)
        {
            ObjectPool.Instance.GetObject(Random.Range(2,7));
            yield return new WaitForSeconds(delay);
        }
    }
}
