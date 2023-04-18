using UnityEngine;
using UnityEngine.AI;
using System.Collections;
 
[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class AnimationController : MonoBehaviour
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
    _rigidbody.detectCollisions = false;
    yield return null;
    _animator.SetFloat(Turn, rotateAngle);
    while (Mathf.Abs(rotateAngle) > 0.1f)
    {
        yield return null;
    }
    _animator.SetFloat(Turn, 0f);
    _rigidbody.detectCollisions = true;
    _isRotating = false;
    _agent.isStopped = false;
}
 
private float rotateAngle
{
    get
    {
        Vector3 direction = _agent.steeringTarget - transform.position;
        return Vector3.SignedAngle(transform.forward, direction, transform.up) / 180f;
    }
}