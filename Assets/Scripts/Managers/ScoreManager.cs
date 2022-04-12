using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int _nowScore = 0;

    private Animator _scoreAnimator;

    private TMP_Text _scoreText;
    private int _strikeCount = 0;
    private float _curLayerW = 0f;

    [SerializeField] private Jumper jumper;
    
    public int Score => _nowScore;

    // Start is called before the first frame update
    void Awake()
    {
        jumper.CircleReached += OnCircleReached;
    }

    void Update()
    {
        if (GameManager.IsGameOvered) { _nowScore = 0;}
    }

    void OnGameOver()
    {
        _nowScore = 0;
    }

    void Start()
    {
        _scoreText = GetComponent<TMP_Text>();
        _scoreText.color = Color.blue;
        _scoreAnimator = GetComponent<Animator>();
        _scoreAnimator.SetLayerWeight(1, 0);
    }

    private void OnCircleReached(int dif, bool isStrike)
    {
        int _incr = 1;
        _incr += dif;
        
        if(isStrike)
        {
            _scoreText.color = Color.yellow;
            _scoreAnimator.SetBool("Tremble", true);
            _curLayerW += 0.1f;
            _scoreAnimator.SetLayerWeight(1, _curLayerW);
            _strikeCount++;
            _incr *= _strikeCount;
        }
        else
        {
            _scoreText.color = Color.blue;
            _scoreAnimator.SetBool("Tremble", false);
            _scoreAnimator.SetLayerWeight(1, 0f);
            _strikeCount = 0;
            _curLayerW = 0f;
        } 
            
        _nowScore += _incr;

        _scoreText.text = _nowScore.ToString();
        _scoreAnimator.SetTrigger("Bounce");

    }

    public void ResetScore() 
    {
        _nowScore = 0;
        _scoreText.text = _nowScore.ToString();
        _scoreText.color = Color.blue;
        _scoreAnimator.SetBool("Tremble", false);
        _scoreAnimator.SetLayerWeight(1, 0f);
        _strikeCount = 0;
        _curLayerW = 0f;
        _scoreAnimator.SetTrigger("Bounce");
    }
}
