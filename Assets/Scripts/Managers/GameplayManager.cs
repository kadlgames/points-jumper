using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class creates default gameplay env.
/// Using when inst scene for first time and 
/// then for reset after GO
/// </summary>
public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject _jumper;
    [SerializeField] private ScoreManager _scoreManager;
    private Jumper _jpScript;
    private SpriteRenderer _jpSRenderer;

    private Vector2 _initialJumperPos;

    private void Awake() 
    {
        GameObject.Find("GOTrigger").GetComponent<GOTrigger>().GameOver += HideEnvironment;
        _jpScript = _jumper.GetComponent<Jumper>();
        _jpSRenderer = _jumper.GetComponentInChildren<SpriteRenderer>();
        _jpSRenderer.enabled = false;
        _jpScript.HideArrow();
    }

    public void SpawnEnvironment()
    {
        if (GameManager.IsGameOvered)
        {
            _scoreManager.ResetScore();
            
            RecalculateInitJumperPos();
            _jumper.transform.position = _initialJumperPos;
            _jpScript.ResetStatus();
            _jpSRenderer.enabled = true;
            _jpScript.ShowArrow();
            _jpScript.RotateArrow(90f);

            GameManager.ResetGame();
        }
    }

    public void HideEnvironment()
    {
        DeleteAllCirclesFromScene();
        _jpScript.HideArrow();
    }

    private void DeleteAllCirclesFromScene()
    {
        foreach(GameObject circle in GameObject.FindGameObjectsWithTag("Circle"))
        {
            Destroy(circle);
        }
    }

    private void RecalculateInitJumperPos()
    {
        ScreenUtils.Initialize();
        _initialJumperPos = new Vector2();
        _initialJumperPos.x = (ScreenUtils.ScreenRight + ScreenUtils.ScreenLeft) / 2;
        _initialJumperPos.y = ScreenUtils.ScreenBottom + (ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom) * 0.1f;
    }
}
