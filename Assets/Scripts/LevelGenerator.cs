using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    #region Fields
    
    // [SerializeField]
    // CirclePackage[] circles = null;

    private CircleDifficultyManager _circDifManager;


    [Space(10)]
    [SerializeField]
    Jumper jumper = null;

    // [Header("Balance")]
    // [SerializeField]
    // float jumpNumberMultiplier = 0.1f;

    CameraMover cameraMover;
    bool isCircleSpawnedOnThisJump = false;
    int jumpNumber = 0;

    //Boundaries
    float upperBound = 0;
    float lowerBound = 0;
    float leftBound = 0;
    float rightBound = 0;

    public LevelGenerator(CircleDifficultyManager circDifManager)
    {
        this._circDifManager = circDifManager;
    }

    #endregion

    #region Properties


    #endregion

    #region Methods

    // Start is called before the first frame update
    void Start()
    {
        cameraMover = GetComponent<CameraMover>();
        _circDifManager = GetComponent<CircleDifficultyManager>();
        _circDifManager.GetMaxDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("isCircleSpawnedOnThisJump:" + isCircleSpawnedOnThisJump);
        // Debug.Log("IsJumping:" + jumper.IsJumping);
        // Debug.Log("IsCamera on target:" + cameraMover.IsOnTargetPosition);
        if (!isCircleSpawnedOnThisJump && !jumper.IsJumping && cameraMover.IsOnTargetPosition) SpawnCircle();
        else if (jumper.IsJumping) isCircleSpawnedOnThisJump = false;
    }

    /// <summary>
    /// Spawns a circle
    /// </summary>
    void SpawnCircle()
    {
        ScreenUtils.Initialize();

        // // Bounds calculated
        // upperBound = ScreenUtils.ScreenTop - (ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom) * 0.1f;
        // lowerBound = ScreenUtils.ScreenBottom + (ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom) * 0.45f;
        // leftBound = ScreenUtils.ScreenLeft + (ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft) * 0.15f;
        // rightBound = ScreenUtils.ScreenRight - (ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft) * 0.15f;

        // upperBound = Bounds.GetBound(Bound.Upper);
        // lowerBound = Bounds.GetBound(Bound.Lower);
        // leftBound = Bounds.GetBound(Bound.Left);
        // rightBound = Bounds.GetBound(Bound.Right);

        BoundsPack bp = Bounds.GetBoundsPack();
        upperBound = bp.Upper;
        lowerBound = bp.Lower;
        leftBound = bp.Left;
        rightBound = bp.Right;

        isCircleSpawnedOnThisJump = true;
        jumpNumber++;

        // SPAWNING CIRCLE
        //Debug.Log("Spawn!");

        
        GameObject circle = _circDifManager.GetNextCicrcle(jumpNumber);

        // Creating spawn coordinates
        float x = Random.Range(leftBound, rightBound);
        float y = Random.Range(lowerBound, upperBound);

        //Spawning a circle
        Instantiate<GameObject>(circle, new Vector3(x, y, 0), Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawLine(new Vector2(leftBound, upperBound), new Vector2(rightBound, upperBound));
        Gizmos.DrawLine(new Vector2(rightBound, upperBound), new Vector2(rightBound, lowerBound));
        Gizmos.DrawLine(new Vector2(rightBound, lowerBound), new Vector2(leftBound, lowerBound));
        Gizmos.DrawLine(new Vector2(leftBound, lowerBound), new Vector2(leftBound, upperBound));
    }

    #endregion

}