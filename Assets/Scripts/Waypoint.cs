using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public float MoveSpeed => _moveSpeed;
}
