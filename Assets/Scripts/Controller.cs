using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    #region Fields

    [SerializeField]
    GameObject point = null;

    /// <summary>
    /// The distance between controller object and a point
    /// </summary>
    float radius;

    /// <summary>
    /// The controlling angle
    /// </summary>
    float angle = 90;

    /// <summary>
    /// Reference to a player's jumper
    /// </summary>
    [SerializeField]
    GameObject jumper = null;

    /// <summary>
    /// Странная часть кода. Ссылка на кнопку паузы
    /// </summary>
    [SerializeField]
    GameObject pauseButton = null;
    Image pauseButtonImage = null;

    Animator animator;

    #endregion
    // Start is called before the first frame update
    
    void Start()
    {
        radius = point.transform.localPosition.y;
        animator = GetComponent<Animator>();
        pauseButton = GameObject.Find("PauseButton");
        pauseButtonImage = pauseButton.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetTouchPosition(out var touchPos, out Touch touch))
        {
            if (!IfTouchAtPauseButton(touchPos))
            {
                if (touch.phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
                {
                    animator.SetBool("isControllerShowed", true);
                    touchPos.y -= radius * point.transform.lossyScale.y;
                    transform.position = touchPos;
                }
                else if (touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
                {
                    animator.SetBool("isControllerShowed", false);
                    jumper.GetComponent<Jumper>().Jump(angle);
                }
                else
                {
                    // touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                    if (touchPos.y > transform.position.y) touchPos.y = transform.position.y;

                    // Move the point and change the angle

                    float distance = Vector2.Distance(transform.position, touchPos);
                    float cosx = (transform.position.y - touchPos.y) / distance;
                    float sinx = (transform.position.x - touchPos.x) / distance;

                    angle = Mathf.Acos(cosx) * Mathf.Rad2Deg;
                    if (sinx > 0) angle *= -1;
                    angle += 90;

                    jumper.GetComponent<Jumper>().RotateArrow(angle);
                    Vector2 newPointPos = new Vector2(radius * sinx, radius * cosx);

                    RotatePointAndArrow(newPointPos);
                }
            }
        }
    }

    /// <summary>
    /// Rotates a point and changing the angle
    /// </summary>
    void RotatePointAndArrow(Vector2 newPointPos)
    {
        point.transform.localPosition = newPointPos;
    }

    bool IfTouchAtPauseButton(Vector2 tp)
    {
        return false;
    }

    private bool GetTouchPosition(out Vector2 outVector, out Touch outTouch)
    {
        if (!GameManager.IsGamePaused)
        {
            if (Input.touchCount > 0)
            {
                outTouch = Input.GetTouch(0);
                outVector = Camera.main.ScreenToWorldPoint(outTouch.position);
                return true;
            }
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            {
                outTouch = new Touch {phase = TouchPhase.Canceled};
                outVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                return true;
            }
        }
        outVector = Vector2.zero;
        outTouch = new Touch {phase = TouchPhase.Canceled};
        return false;
    }
}
