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
    /// The angle arrow now rotated
    /// </summary>
    float angle;

    /// <summary>
    /// The distance between jumper and arrow
    /// </summary>
    float radius;

    /// <summary>
    /// Whether in jump
    /// </summary>
    bool isJumping = false;

    [Header("Balance")]
    [SerializeField]
    float ForceImpulseMultiplier = 10f;

    GameObject arrow;
    Rigidbody2D rb;
    #endregion

    public event Action<int> CircleReached = delegate { };

    #region Properties

    public bool IsJumping
    {
        get { return isJumping; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        arrow = GameObject.Find("arrow");
        radius = arrow.transform.position.y - transform.position.y;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Arrow hiding
        if (!isJumping)
        {
            arrow.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    /// <summary>
    /// Rotates an arrow 
    /// </summary>
    public void RotateArrow(float angle)
    {
        // Changing position
        float rad = angle * Mathf.Deg2Rad;
        float x = radius * Mathf.Cos(rad) + transform.position.x;
        float y = radius * Mathf.Sin(rad) + transform.position.y;
        arrow.transform.position = new Vector2(x, y);

        // Changing rotation
        arrow.transform.rotation = Quaternion.Euler(0, 0, 180 + angle);
    }

    /// <summary>
    /// Jumper doing jump!
    /// </summary>
    public void Jump(float angle)
    {
        if (!isJumping)
        {
            isJumping = true;
            float rad = angle * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(rad);
            float y = radius * Mathf.Sin(rad);
            rb.AddForce(new Vector2(x, y) * ForceImpulseMultiplier, ForceMode2D.Impulse);
            arrow.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Circle")
        {
            var circle = col.gameObject.GetComponent<Circle>(); 
            circle.Reached();
            CircleReached(circle.difficulty);
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0f, 0f);
            gameObject.transform.position = col.gameObject.transform.position;
            isJumping = false;
            arrow.SetActive(true);
            
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
}