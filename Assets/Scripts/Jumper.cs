using UnityEngine;

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

    private GameObject _arrow;
    private Rigidbody2D _rb;
    #endregion

    #region Properties

    /// <summary>
    /// Whether in jump
    /// </summary>
    public bool IsJumping { get; private set; }

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        _arrow = GameObject.Find("arrow");
        _radius = _arrow.transform.position.y - transform.position.y;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Arrow hiding
        if (!IsJumping)
        {
            _arrow.GetComponent<SpriteRenderer>().enabled = true;
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
        _arrow.transform.position = new Vector2(x, y);

        // Changing rotation
        _arrow.transform.rotation = Quaternion.Euler(0, 0, 180 + angle);
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
        _rb.AddForce(new Vector2(x, y) * forceImpulseMultiplier, ForceMode2D.Impulse);
        _arrow.GetComponent<SpriteRenderer>().enabled = false;

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Circle"))
        {
            col.gameObject.GetComponent<Circle>().Reached();
            _rb.gravityScale = 0;
            _rb.velocity = new Vector2(0f, 0f);
            gameObject.transform.position = col.gameObject.transform.position;
            IsJumping = false;
            _arrow.SetActive(true);
        }
        else
        {
            _rb.gravityScale = 1;
        }
    }
}