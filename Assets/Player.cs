using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
  Vector2 movement = Vector2.zero;
  Vector3 rotation = Vector3.zero;

  private Camera _playerCamera = null;
  private Rigidbody _rigidbody = null;
  private PlayerInput _playerInput = null;

  private float _speed = 10f;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;

    _playerCamera = GetComponentInChildren<Camera>();
    _rigidbody = GetComponent<Rigidbody>();
    _playerInput = GetComponent<PlayerInput>();
  }

  void FixedUpdate()
  {
    Vector3 localMovement = new Vector3(this.movement.x, 0, this.movement.y)
      * _speed;
    Quaternion yaw = Quaternion.Euler(0, rotation.y, 0);
    Vector3 velocity = yaw * localMovement;

    velocity.y = _rigidbody.linearVelocity.y;
    _rigidbody.linearVelocity = velocity;
  }

  void Update()
  {
    _playerCamera.transform.localRotation = Quaternion.Euler(rotation);
  }

  public void OnMove(InputValue value)
  {
    Vector2 input = value.Get<Vector2>();
    this.movement = input;
  }

  public void OnLook(InputValue value)
  {
    Vector2 input = value.Get<Vector2>() * 0.1f;
    this.rotation += new Vector3(-input.y, input.x, 0);
  }

  public void OnTilt(InputValue value)
  {
    Double input = value.Get<Double>() * 0.1f;
  }
}
