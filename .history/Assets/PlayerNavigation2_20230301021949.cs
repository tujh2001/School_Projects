using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerNavigation2 : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    private NavMeshAgent _agent;
    private static readonly int Forward = Animator.StringToHash("Forward");
    private static readonly int Turn = Animator.StringToHash("Turn");
 
    [SerializeField] private float _forwardDumpTime = 0.3f;
    [SerializeField] private float _turnDumpTime = 0.1f;
    [SerializeField] private float _movingTurnSpeed = 360f;
    [SerializeField] private float _stationaryTurnSpeed = 180f;
 
    private bool _isRotating;
    private List<Vector3> _path;
 
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                                 RigidbodyConstraints.FreezeRotationZ;
    }
 
    private void Update()
    {
        if (!_isRotating)
        {
            UpdateAnimator(_agent.remainingDistance > _agent.stoppingDistance ? _agent.desiredVelocity : Vector3.zero);
        }
    }
 
    public void MoveTo(Vector3 target)
    {
        _agent.SetDestination(target);
        StartCoroutine(RotateToNextPosition());
    }
 
    private IEnumerator RotateToNextPosition()
    {
        _agent.isStopped = true;
        _isRotating = true;
        while(rotateAngle > 0.1f)
        {
            _animator.SetFloat(Turn, rotateAngle);
            yield return null;
        }
        _isRotating = false;
        _agent.isStopped = false;
    }
 
    private float rotateAngle => Vector3.Angle(transform.forward, _agent.steeringTarget) / 180f;
 
    private void UpdateAnimator(Vector3 velocity)
    {
        float turnAmount = 0f;
        float forwardAmount = 0f;
 
        if (velocity != Vector3.zero)
        {
            if (velocity.magnitude > 1f)
            {
                velocity.Normalize();
            }
 
            velocity = transform.InverseTransformDirection(velocity);
 
            turnAmount = Mathf.Atan2(velocity.x, velocity.z);
            forwardAmount = velocity.z;
            float turnSpeed = Mathf.Lerp(_stationaryTurnSpeed, _movingTurnSpeed, forwardAmount);
 
            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0f);
        }
 
        _animator.SetFloat(Forward, forwardAmount, _forwardDumpTime, Time.deltaTime);
        _animator.SetFloat(Turn, turnAmount, _turnDumpTime, Time.deltaTime);
    }
 
    private void OnDrawGizmos()
        {
            if (_path != null && _navMeshAgent)
            {
                Gizmos.color = Color.blue;
                for (var i = 0; i < _path.Count - 1; i++)
                {
                    Gizmos.DrawLine(_path[i], _path[i + 1]);
                }
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(_navMeshAgent.steeringTarget, 0.2f);
            }
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        }
}