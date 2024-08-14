using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Moving : MonoBehaviour
{
    [SerializeField] private Waypoint[] _waypoints;

    private float _speed = 1.72f;
    private int _currentWaypointIndex;

    private Animator CriminalAnimator => GetComponent<Animator>();
    private Waypoint CurrentWaypoint => _waypoints[_currentWaypointIndex];

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, NextWaypoint().transform.position, _speed * Time.deltaTime);

        if (transform.position == NextWaypoint().transform.position && _currentWaypointIndex < _waypoints.Length - 1)
            ChangeWaypoint();
    }

    private Waypoint NextWaypoint()
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

        transform.LookAt(NextWaypoint().transform);

        _speed = CurrentWaypoint.MoveSpeed;

        CriminalAnimator.SetFloat("_speed", _speed);
    }
}
