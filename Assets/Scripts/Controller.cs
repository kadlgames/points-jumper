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
        if (Input.touchCount > 0 && !GameManager.IsGamePaused)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            if (!IfTouchAtPauseButton(touchPos))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        animator.SetBool("isControllerShowed", true);
                        touchPos.y -= radius * point.transform.lossyScale.y;
                        transform.position = touchPos;
                        break;
                    case TouchPhase.Ended:
                        animator.SetBool("isControllerShowed", false);
                        jumper.GetComponent<Jumper>().Jump(angle);
                        break;
                    case TouchPhase.Moved:
                        touchPos = Camera.main.ScreenToWorldPoint(touch.position);

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
                        break;
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
        Debug.Log("touchPos " + tp.x + " " + tp.y);
        Debug.Log("buttonPos " + pauseButton.transform.position.x + " " + pauseButton.transform.position.y);
        if (tp.x >= (0.5 * pauseButton.transform.position.x
            - pauseButton.transform.lossyScale.x * pauseButtonImage.rectTransform.rect.width) &&
            tp.x <= (0.5 * pauseButton.transform.position.x
            + pauseButton.transform.lossyScale.x * pauseButtonImage.rectTransform.rect.width) &&
            tp.y >= (0.5 * pauseButton.transform.position.y
            - pauseButton.transform.lossyScale.y * pauseButtonImage.rectTransform.rect.height) &&
            tp.y <= (0.5 * pauseButton.transform.position.y
            + pauseButton.transform.lossyScale.y * pauseButtonImage.rectTransform.rect.height))
        {
            return true;
        }

        return false;

    }
}
