using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Waypoint[] _waypoints;

    private readonly int _moveSpeed = Animator.StringToHash(nameof(_moveSpeed));

    private float _speed = 1.72f;
    private int _currentWaypointIndex;
    private Vector3 _targetPosition;
    private Animator CriminalAnimator;

    private Waypoint NextWaypoint => _waypoints[Mathf.Min(_waypoints.Length - 1, _currentWaypointIndex + 1)];

    private void Start()
    {
        CriminalAnimator = GetComponent<Animator>();
        _targetPosition = NextWaypoint.transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

        if (transform.position == _targetPosition && _currentWaypointIndex < _waypoints.Length - 1)
            ChangeWaypoint();
    }

    private void ChangeWaypoint()
    {
        _speed = _waypoints[++_currentWaypointIndex].MoveSpeed;

        _targetPosition = NextWaypoint.transform.position;

        transform.LookAt(NextWaypoint.transform);

        CriminalAnimator.SetFloat(_moveSpeed, _speed);
    }
}
