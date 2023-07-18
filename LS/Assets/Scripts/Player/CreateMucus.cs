using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMucus : MonoBehaviour
{
    [SerializeField] GameObject MucusObj;
    public void Createmucus()
    {
        GameObject mucus = Instantiate(MucusObj, transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity); ;
    }
}
