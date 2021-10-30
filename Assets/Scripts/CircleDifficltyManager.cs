using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDifficltyManager : MonoBehaviour
{
    [SerializeField]
    CirclePackage[] circles = null;

    [Header("Balance")]
    [SerializeField]
    float jumpNumberMultiplier = 0.1f;

    int maxDif = 0;

    List<int> suitableCirclesId = new List<int>();
    
    //Circles id with difficlty less or equal than maxNextDif
    List<int> nextDifCirclesId = new List<int>();


    public GameObject getNextCicrcle(int jumpNumber)
    {
        suitableCirclesId.Clear();
        nextDifCirclesId.Clear();
        
        int maxNextDif = (int)(jumpNumber * jumpNumberMultiplier);
        for(int i = 0; i < circles.Length; i++)
        {
            if (circles[i].difficulty <= maxNextDif)
                nextDifCirclesId.Add(i);
        }

        // Get next difficlty circle id
        int nextCircleId = Random.Range(0, nextDifCirclesId.Count);
        // Circle difficulty select
        int n = circles[nextCircleId].difficulty;
        if (n > maxDif) n = maxDif;

        //Creating array of suitable circles
        for (int i = 0; i < circles.Length; i++)
        {
            if (circles[i].difficulty == n) suitableCirclesId.Add(i);
        }

        if (suitableCirclesId.Count == 0)
        {
            Debug.LogError("in function CircleDifficltyManager::getNextCicrcle. Get empty suitableCirclesId list");
            
            int minDifCircleId = 0;
            for (int i = 0; i < circles.Length; i++)
            {
                int minDif = int.MaxValue;
                if (circles[i].difficulty < minDif) 
                {
                    minDif = circles[i].difficulty;
                    minDifCircleId = i;
                }
            }

            suitableCirclesId.Add(minDifCircleId);
        }

        // Select one of them
        n = Random.Range(0, (int)suitableCirclesId.Count);
        //Get real circle id
        int circleId = suitableCirclesId[n]; 

        return circles[circleId].circlePrefab;
    }


    public void getMaxDifficulty()
    {
        foreach (CirclePackage circlePack in circles)
        {
            if (circlePack.difficulty > maxDif) maxDif = circlePack.difficulty;
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
