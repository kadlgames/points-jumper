using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Behaviour Modifiers/Shield")]
public class Shield : MonoBehaviour
{

    #region Fields

    [SerializeField]
    GameObject shieldGO = null;
    Renderer shieldRend;

    [Header("Shield data (Legacy Information)")]
    [SerializeField]
    float shieldLength;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // Check if this circleGO doesn't have a CircleMover component
        if (GetComponent<CircleMover>() == null) 
        {
            float radius = Mathf.Abs(shieldGO.transform.localPosition.y);
            GameObject target = GameObject.Find("Jumper");
            float distance = Vector2.Distance(transform.position, target.transform.position);
            float cos = -(transform.position.x - target.transform.position.x) / distance;
            float sin = -Mathf.Sqrt(1 - Mathf.Pow(cos, 2f));
            Vector2 newPos = new Vector2();
            newPos.x = radius * cos; newPos.y = radius * sin;
            shieldGO.transform.localPosition = newPos;
            shieldGO.transform.rotation = Quaternion.AngleAxis(90f - Mathf.Acos(cos)*Mathf.Rad2Deg, Vector3.forward);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Context fucnction to set up data (Legacy function)
    /// </summary>
    [ContextMenu("Fetch shield length")]
    void FetchShieldLength()
    {
        BoxCollider2D shieldCol = GetComponentInChildren<BoxCollider2D>();
        shieldLength = shieldCol.bounds.size.x;  
    }
}
