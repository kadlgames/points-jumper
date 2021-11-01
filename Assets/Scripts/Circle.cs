using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    #region Fields

    bool isActive = true;
    bool isAlreadyReached = false;
    Animator animator;
    [SerializeField]
    GameObject particleSystemGO = null;

    public int difficulty = 0;

    #endregion

    #region Properties

    /// <summary>
    /// Is this circle active
    /// </summary>
    /// <value></value>
    public bool IsActive
    {
        get { return isActive; }
    }

    #endregion

    Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive && !isAlreadyReached)
        {
            animator.SetTrigger("reached");
            isAlreadyReached = true;
            Instantiate<GameObject>(particleSystemGO, transform.position, Quaternion.identity);
        }  
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
        isActive = false;
        col.enabled = false;
    }
}

