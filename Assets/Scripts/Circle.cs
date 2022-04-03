using UnityEngine;

public class Circle : MonoBehaviour
{
    #region Fields

    private bool _isAlreadyReached;
    private Animator _animator;
    [SerializeField] private GameObject particleSystemGO;

    public int difficulty = 0;

    #endregion

    /// <summary>
    /// Is this circle active
    /// </summary>
    /// <value></value>
    public bool IsActive { get; private set; } = true;


    private Collider2D _col;

    private static readonly int Reached1 = Animator.StringToHash("reached");

    // Start is called before the first frame update
    private void Start()
    {
        _col = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsActive || _isAlreadyReached) return;
        _animator.SetTrigger(Reached1);
        _isAlreadyReached = true;
        Instantiate(particleSystemGO, transform.position, Quaternion.identity);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Reach the circle
    /// </summary>
    public void Reached()
    {
        IsActive = false;
        _col.enabled = false;
    }
}

