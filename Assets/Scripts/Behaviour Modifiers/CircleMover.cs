using UnityEngine;

namespace Behaviour_Modifiers
{
    [AddComponentMenu("Behaviour Modifiers/Mover")]
    public class CircleMover : MonoBehaviour
    {
        #region Fields

        private Vector2 _startPos, _endPos;
        [SerializeField] private float speed = 1f;
        [SerializeField] private float timerDuration = 1f;

        private bool _movingForward = true;
        private bool _stopped;
        private bool _isAlreadyMoved;
        private Timer _timer;
        private Circle _circle;

        #endregion

        private void Start()
        {
            var bp = Bounds.GetBoundsPack();
            _startPos = transform.position;
            _timer = gameObject.AddComponent<Timer>();
            _circle = GetComponent<Circle>();

            // Generating end pos
            _endPos.y = Random.Range(bp.Lower, bp.Upper);
            _endPos.x = Random.Range(bp.Left, bp.Right);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!_circle.IsActive) Destroy(this);
            // Logic
            if ((Vector2)transform.position == _endPos || ((Vector2)transform.position == _startPos && _isAlreadyMoved))
            {
                _movingForward = !_movingForward;

                if (!_timer.Running && !_timer.Finished)
                {
                    _stopped = true;
                    _timer.Duration = timerDuration;
                    _timer.Run();
                }
                else if (_timer.Finished)
                {
                    _stopped = false;
                    _timer.Reset();
                }
            }

            if (_stopped) return;
            _isAlreadyMoved = true;

            transform.position = Vector2.MoveTowards(transform.position, _movingForward ? 
                _endPos : _startPos, Time.deltaTime * speed);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(_startPos, _endPos);
        }
    }
}
