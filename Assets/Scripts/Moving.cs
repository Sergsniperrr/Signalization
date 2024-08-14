using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Moving : MonoBehaviour
{
    [SerializeField] private Waypoint[] _waypoints;

    private float _speed = 1.72f;
    private int _currentWaypointIndex;
    private Animator CriminalAnimator;

    private Waypoint CurrentWaypoint => _waypoints[_currentWaypointIndex];

    private void Start()
    {
        CriminalAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, ReceiveNextWaypoint().transform.position, _speed * Time.deltaTime);

        if (transform.position == ReceiveNextWaypoint().transform.position && _currentWaypointIndex < _waypoints.Length - 1)
            ChangeWaypoint();
    }

    private Waypoint ReceiveNextWaypoint()
    {
        Waypoint nextWaypoint;

        if (_currentWaypointIndex < _waypoints.Length - 1)
            nextWaypoint = _waypoints[_currentWaypointIndex + 1];
        else
            nextWaypoint = CurrentWaypoint;

        return nextWaypoint;
    }

    private void ChangeWaypoint()
    {
        _currentWaypointIndex ++;

        transform.LookAt(ReceiveNextWaypoint().transform);

        _speed = CurrentWaypoint.MoveSpeed;

        CriminalAnimator.SetFloat("_speed", _speed);
    }
}
