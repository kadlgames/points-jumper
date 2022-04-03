using UnityEngine;
using System;

public class GOTrigger : MonoBehaviour
{
    public Action GameOver = delegate {};

    void OnTriggerEnter2D()
    {
        GameOver();
    }
}