using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] GameObject _scoreText;
    
    private void Update() 
    {
        _scoreText.SetActive(!GameManager.IsGameOvered);
    }
}
