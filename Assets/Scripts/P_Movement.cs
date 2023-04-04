using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Movement : MonoBehaviour
{

    //Movement
    public float _speed;
    [SerializeField]
    private float _rotSpeed;
    private Vector2 _smoothMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Rigidbody2D rb;
    private Vector2 _movementInput;

    //Health
    public int _health = 100;
    private P_Weapon weap;
    private bool _attackInput;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        weap = GetComponentInChildren<P_Weapon>();
    }

    private void FixedUpdate() {
        SetPlayerVelocity();
    }

    #region MOVEMENT
    private void SetPlayerVelocity(){
        _smoothMovementInput = Vector2.SmoothDamp(
            _smoothMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
            0.1f);
        rb.velocity = _smoothMovementInput * _speed;
    }

    private void OnMove(InputValue ip){
        _movementInput = ip.Get<Vector2>();
    }
    #endregion

    private void OnAttack (InputValue ip) {
        _attackInput = ip.isPressed;
        if(ip.isPressed){
            StartCoroutine(weap.Attack());
        }
    }
}
