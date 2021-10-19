using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Behaviour Modifiers/Mover")]
public class CircleMover : MonoBehaviour
{
    #region Fields

    Vector2 startPos, endPos;
    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float timerDuration = 1f;

    bool movingForward = true;
    bool stopped = false;
    bool isAlreadyMoved = false;
    Timer timer;
    Circle circle;

    #endregion

    void Start()
    {
        BoundsPack bp = Bounds.GetBoundsPack();
        startPos = transform.position;
        timer = gameObject.AddComponent<Timer>();
        circle = GetComponent<Circle>();

        // Generating endpos
        endPos.y = Random.Range(bp.Lower, bp.Upper);
        endPos.x = Random.Range(bp.Left, bp.Right);
    }

    // Update is called once per frame
    void Update()
    {
        if (!circle.IsActive) Destroy(this);
        // Logic
        if ((Vector2)transform.position == endPos || ((Vector2)transform.position == startPos && isAlreadyMoved))
        {
            if (movingForward) movingForward = false;
            else movingForward = true;

            if (!timer.Running && !timer.Finished)
            {
                stopped = true;
                timer.Duration = timerDuration;
                timer.Run();
            }
            else if (timer.Finished)
            {
                stopped = false;
                timer.Reset();
            }
        }
        if (!stopped)
        {
            isAlreadyMoved = true;

            if (movingForward) transform.position = Vector2.MoveTowards(transform.position,
             endPos, Time.deltaTime * speed);
            else transform.position = Vector2.MoveTowards(transform.position,
             startPos, Time.deltaTime * speed);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(startPos, endPos);
    }
}
