using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHit : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        _animator.SetTrigger("hit");
    }
}
