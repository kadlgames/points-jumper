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

    [SerializeField] private Jumper jumper;
    
    // Start is called before the first frame update
    void Awake()
    {
        jumper.CircleReached += OnCircleReached;
    }

    void Start()
    {
        _scoreText = GetComponent<TMP_Text>();
        _scoreAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCircleReached(int dif, bool isStrike)
    {
        int _incr = 1;
        _incr += dif;
        
        if(isStrike)
        {
            _strikeCount++;
            _incr *= _strikeCount;
        }
        else
        {
            _strikeCount = 0;
        } 
            
        _nowScore += _incr;

        _scoreText.text = _nowScore.ToString();
        _scoreAnimator.SetTrigger("Bounce");

    }
}
