using UnityEngine;
using System;

public class GOTrigger : MonoBehaviour
{
    public Action GameOver = delegate {};

    private bool _isTriggered = false;

    void OnTriggerEnter2D()
    {
        if (!_isTriggered) GameOver();
        _isTriggered = true;
    }

    public void ResetTrigger()
    {
        _isTriggered = false;
    }
}