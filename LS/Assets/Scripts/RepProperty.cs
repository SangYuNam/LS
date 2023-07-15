using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepProperty : MonoBehaviour
{
    private Animator _Anim = null;
    protected Animator myAnim
    {
        get
        {
            if (_Anim == null)
            {
                _Anim = GetComponent<Animator>();
                if (_Anim == null)
                {
                    _Anim = GetComponentInChildren<Animator>();
                }
            }
            return _Anim;
        }
    }

    private SpriteRenderer _Renderer = null;
    protected SpriteRenderer myRenderer
    {
        get
        {
            if(_Renderer == null)
            {
                _Renderer = GetComponent<SpriteRenderer>();
                if(_Renderer == null)
                {
                    _Renderer = GetComponentInChildren<SpriteRenderer>();
                }
            }
            return _Renderer;
        }
    }
}