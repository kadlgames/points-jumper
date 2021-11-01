using System.Collections.Generic;
using UnityEngine;

public class CircleDifficultyManager : MonoBehaviour
{
    [SerializeField] private CirclePackage[] circles;
    
    [Header("Balance")]
    [SerializeField] private float jumpNumberMultiplier = 0.1f;

    private int _maxDif;

    private readonly List<int> _suitableCirclesId = new List<int>();
    
    //Circles id with difficulty less or equal than maxNextDif
    private readonly List<int> _nextDifCirclesId = new List<int>();
    
    public GameObject GetNextCircle(int jumpNumber)
    {
        _suitableCirclesId.Clear();
        _nextDifCirclesId.Clear();
        
        var maxNextDif = (int)(jumpNumber * jumpNumberMultiplier);
        
        for(var i = 0; i < circles.Length; i++)
        {
            if (circles[i].difficulty <= maxNextDif)
            {
                _nextDifCirclesId.Add(i);

            }
        }
        
        // Get next difficulty circle id
        var nextCircleId = _nextDifCirclesId[Random.Range(0, _nextDifCirclesId.Count)];
        // Circle difficulty select
        var n = circles[nextCircleId].difficulty;
        if (n > _maxDif) n = _maxDif;

        //Creating array of suitable circles
        for (var i = 0; i < circles.Length; i++)
        {
            if (circles[i].difficulty == n) _suitableCirclesId.Add(i);
        }

        if (_suitableCirclesId.Count == 0)
        {
            Debug.LogError("in function CircleDifficultyManager::getNextCircle. Get empty suitableCirclesId list");
            
            var minDifCircleId = 0;
            var minDif = int.MaxValue;
            for (var i = 0; i < circles.Length; i++)
            {
                if (circles[i].difficulty >= minDif) continue;
                minDif = circles[i].difficulty;
                minDifCircleId = i;
            }

            _suitableCirclesId.Add(minDifCircleId);
        }

        // Select one of them
        var circleId = _suitableCirclesId[Random.Range(0, _suitableCirclesId.Count)];

        return circles[circleId].circlePrefab;
    }


    public void GetMaxDifficulty()
    {
        foreach (var circlePack in circles)
        {
            if (circlePack.difficulty > _maxDif) _maxDif = circlePack.difficulty;
        }
        
    }
}
