using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _Instance;

    public static T Instance
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = (T)FindObjectOfType(typeof(T));
                if(_Instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    _Instance = obj.GetComponent<T>();
                }
            }
            return _Instance;
        }
    }
    private void Awake()
    {
        if(transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
