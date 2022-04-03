using UnityEngine;

public class CameraMover : MonoBehaviour
{
    #region Fields

    [SerializeField] private float cameraSpeed = 1f;

    [SerializeField] private GameObject player;

    private Vector3 _target = new Vector3(0f, 0f, -10f);
    private Jumper _playerBehavior;
    private float _defaultDistanceToPlayer;

    #endregion

    #region Properties

    /// <summary>
    /// Is camera located on target position
    /// </summary>
    /// <value></value>
    public bool IsOnTargetPosition { get; private set; } = true;

    #endregion

    #region Methods
    // Start is called before the first frame update
    private void Start()
    {
        _playerBehavior = player.GetComponent<Jumper>();
        _defaultDistanceToPlayer = transform.position.y - player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Lerping
        _target.y = player.transform.position.y;
        _target.y += _defaultDistanceToPlayer;
        if (!_playerBehavior.IsJumping) LerpToPlayer(_target);

        // Checking if camera located on target position
        IsOnTargetPosition = Mathf.Abs(_target.y - transform.position.y) <= 0.1f;
        //Debug.Log("isOnTargetPosition: " + isOnTargetPosition);
    }

    /// <summary>
    /// Lerps to player with some distance by Y
    /// </summary>
    /// <param name="target">Target (player)</param>
    private void LerpToPlayer(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * cameraSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_target, 0.1f);
    }
    #endregion


}