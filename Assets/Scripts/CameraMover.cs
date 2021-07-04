using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    #region Fields

    [SerializeField]
    float cameraSpeed = 1f;

    [SerializeField]
    GameObject player = null;

    Vector3 target = new Vector3(0f, 0f, -10f);
    Jumper playerBehavior;
    float defaultDistanceToPlayer;
    bool isOnTargetPosition = true;

    #endregion

    #region Properties

    /// <summary>
    /// Is camera located on target position
    /// </summary>
    /// <value></value>
    public bool IsOnTargetPosition
    {
        get { return isOnTargetPosition; }
    }

    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        playerBehavior = player.GetComponent<Jumper>();
        defaultDistanceToPlayer = transform.position.y - player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Lerping
        target.y = player.transform.position.y;
        target.y += defaultDistanceToPlayer;
        if (!playerBehavior.IsJumping) LerpToPlayer(target);

        // Checking if camera located on target position
        if (Mathf.Abs(target.y - transform.position.y) <= 0.1f) isOnTargetPosition = true;
        else isOnTargetPosition = false;
        //Debug.Log("isOnTargetPosition: " + isOnTargetPosition);
    }

    /// <summary>
    /// Lerps to player with some distance by Y
    /// </summary>
    /// <param name="target">Target (player)</param>
    void LerpToPlayer(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * cameraSpeed);
    } 

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(target, 0.1f);
    }
    #endregion


}