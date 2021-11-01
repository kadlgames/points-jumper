using UnityEngine;

/// <summary>
/// A timer
/// </summary>
public class Timer : MonoBehaviour
{

    #region Fields

    // timer duration
    private float _totalSeconds;

    // timer execution
    private float _elapsedSeconds;

    // support for Finished property
    private bool _started;

    #endregion

    #region Properties

    /// <summary>
    /// Sets the duration of the timer
    /// The duration can only be set if the timer isn't currently running
    /// </summary>
    /// <value>duration</value>
    public float Duration
    {
        set
        {
            if (!Running)
            {
                _totalSeconds = value;
            }
        }
    }

    /// <summary>
    /// Gets whether or not the timer has finished running
    /// This property returns false if the timer has never been started
    /// </summary>
    /// <value>true if finished; otherwise, false.</value>
    public bool Finished => _started && !Running;

    /// <summary>
    /// Gets whether or not the timer is currently running
    /// </summary>
    /// <value>true if running; otherwise, false.</value>
    public bool Running { get; private set; }

    #endregion

    #region Methods

    // Update is called once per frame
    private void Update()
    {

        // update timer and check for finished
        if (!Running) return;
        _elapsedSeconds += Time.deltaTime;
        if (_elapsedSeconds >= _totalSeconds)
        {
            Running = false;
        }
    }

    /// <summary>
    /// Runs the timer
    /// Because a timer of 0 duration doesn't really make sense,
    /// the timer only runs if the total seconds is larger than 0
    /// This also makes sure the consumer of the class has actually 
    /// set the duration to something higher than 0
    /// </summary>
    public void Run()
    {

        // only run with valid duration
        if (!(_totalSeconds > 0)) return;
        _started = true;
        Running = true;
        _elapsedSeconds = 0;
    }

    // reset all fields to default values
    public void Reset()
    {
        _totalSeconds = 0;

        _elapsedSeconds = 0;
        Running = false;

        _started = false;
    }

    #endregion
}
