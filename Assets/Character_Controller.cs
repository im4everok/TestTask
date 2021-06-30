using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public CharacterController playerCtrl;
    public float speed = 6f;
    public float smoothTurnTime = 0.1f;
    [SerializeField] private Transform _camera;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;
    private float _turnSmoothVelocity;
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerCtrl.Move(moveDir.normalized * speed * Time.deltaTime);
            
        }
        if(_rb.velocity.x > 0 || _rb.velocity.z > 0)
        {
            _animator.SetTrigger("StartRunning");
        }
    }
}
