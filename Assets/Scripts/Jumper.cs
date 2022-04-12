using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Jumper - character
/// </summary>
public class Jumper : MonoBehaviour
{
    #region Fields
    
    /// <summary>
    /// The distance between jumper and arrow
    /// </summary>
    private float _radius;

    [Header("Balance")]
    [SerializeField]
    private float forceImpulseMultiplier = 10f;

    [SerializeField] private float StrikeMaxAngleDif = 5f;

    GameObject arrow;
    Rigidbody2D rb;

    #endregion

    public event Action<int, bool> CircleReached = delegate { };

    #region Properties

    /// <summary>
    /// Whether in jump
    /// </summary>
    public bool IsJumping { get; private set; }

    private bool _isArrowHided = true;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        arrow = GameObject.Find("arrow");
        _radius = arrow.transform.position.y - transform.position.y;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Arrow hiding
        if (!IsJumping)
        {
            arrow.GetComponent<SpriteRenderer>().enabled = !_isArrowHided;
        }
    }

    /// <summary>
    /// Rotates an arrow 
    /// </summary>
    public void RotateArrow(float angle)
    {
        // Changing position
        var rad = angle * Mathf.Deg2Rad;
        var position = transform.position;
        var x = _radius * Mathf.Cos(rad) + position.x;
        var y = _radius * Mathf.Sin(rad) + position.y;
        arrow.transform.position = new Vector2(x, y);

        // Changing rotation
        arrow.transform.rotation = Quaternion.Euler(0, 0, 180 + angle);
    }

    /// <summary>
    /// Jumper doing jump!
    /// </summary>
    public void Jump(float angle)
    {
        if (IsJumping) return;
        IsJumping = true;
        var rad = angle * Mathf.Deg2Rad;
        var x = _radius * Mathf.Cos(rad);
        var y = _radius * Mathf.Sin(rad);
        rb.AddForce(new Vector2(x, y) * forceImpulseMultiplier, ForceMode2D.Impulse);
        arrow.GetComponent<SpriteRenderer>().enabled = false;

    }


    private bool isStrike(Circle circle)
    {
        Vector2 jumperDirection = rb.velocity;
        Vector2 centerToCenterVector = ((Vector2)circle.transform.position - (Vector2)this.transform.position);

        // angle means how accurate shot was. closer to 90 - more accurate
        var angle = Vector2.Angle(jumperDirection, centerToCenterVector);

        return (angle < 90 + StrikeMaxAngleDif);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Circle"))
        {
            var circle = col.gameObject.GetComponent<Circle>(); 
            circle.Reached();

            CircleReached(circle.difficulty, isStrike(circle));
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0f, 0f);
            gameObject.transform.position = col.gameObject.transform.position;
            IsJumping = false;
            arrow.SetActive(true);
            
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    public void ResetStatus() 
    {
        IsJumping = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }

    public void HideArrow() { _isArrowHided = true;}

    public void ShowArrow() { _isArrowHided = false;}
}