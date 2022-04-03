using UnityEngine;

public class Controller : MonoBehaviour
{

    #region Fields

    [SerializeField] private GameObject point;

    /// <summary>
    /// The distance between controller object and a point
    /// </summary>
    private float _radius;

    /// <summary>
    /// The controlling angle
    /// </summary>
    private float _angle = 90;

    /// <summary>
    /// Reference to a player's jumper
    /// </summary>
    [SerializeField] private GameObject jumper;

    private Animator _animator;
    private static readonly int IsControllerShowed = Animator.StringToHash("isControllerShowed");

    #endregion
    // Start is called before the first frame update

    private void Start()
    {
        _radius = point.transform.localPosition.y;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GetTouchPosition(out var touchPos, out var touch)) return;
        if (IfTouchAtUI()) return;
        if (touch.phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            _animator.SetBool(IsControllerShowed, true);
            touchPos.y -= _radius * point.transform.lossyScale.y;
            transform.position = touchPos;
        }
        else if (touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
        {
            _animator.SetBool(IsControllerShowed, false);
            jumper.GetComponent<Jumper>().Jump(_angle);
        }
        else
        {
            // touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touchPos.y > transform.position.y) touchPos.y = transform.position.y;

            // Move the point and change the angle

            var position = transform.position;
            var distance = Vector2.Distance(position, touchPos);
            var cosX = (position.y - touchPos.y) / distance;
            var sinX = (position.x - touchPos.x) / distance;

            _angle = Mathf.Acos(cosX) * Mathf.Rad2Deg;
            if (sinX > 0) _angle *= -1;
            _angle += 90;

            jumper.GetComponent<Jumper>().RotateArrow(_angle);
            var newPointPos = new Vector2(_radius * sinX, _radius * cosX);

            RotatePointAndArrow(newPointPos);
        }
    }

    /// <summary>
    /// Rotates a point and changing the angle
    /// </summary>
    private void RotatePointAndArrow(Vector2 newPointPos)
    {
        point.transform.localPosition = newPointPos;
    }

    private static bool IfTouchAtUI()
    {
        return Utils.Utils.IsPointerOverUi();
    }

    private static bool GetTouchPosition(out Vector2 outVector, out Touch outTouch)
    {
        if (!GameManager.IsGamePaused)
        {
            if (Input.touchCount > 0)
            {
                outTouch = Input.GetTouch(0);
                if (Camera.main is { }) outVector = Camera.main.ScreenToWorldPoint(outTouch.position);
                else outVector = Vector2.zero;
                return true;
            }
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            {
                outTouch = new Touch {phase = TouchPhase.Canceled};
                if (Camera.main is { }) outVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                else outVector = Vector2.zero;
                return true;
            }
        }
        outVector = Vector2.zero;
        outTouch = new Touch {phase = TouchPhase.Canceled};
        return false;
    }
}
