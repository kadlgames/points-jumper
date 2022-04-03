using UnityEngine;

[System.Serializable]
public class CirclePackage
{
    public string name;
    public GameObject circlePrefab;
    [Range(0, 10)]
    public int difficulty; 
}
