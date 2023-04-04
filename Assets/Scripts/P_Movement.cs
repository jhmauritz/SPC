using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Movement : MonoBehaviour
{

    //Movement
    public float _speed;
    private Vector2 _smoothMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Rigidbody2D rb;
    private Vector2 _movementInput;
    private InputActionReference pointerPosition;
    private Vector2 pointerInput;


    public float orbitSpeed = 5f; // The speed of the weapon orbit

    private Vector2 mousePosition; // The position of the mouse in screen coordinates
    private Vector2 weaponOffset; // The offset of the weapon from the player
    private Vector2 orbitDirection; // The direction of the orbit

    //Health
    public int _health = 100;
    private P_Weapon weap;
    [SerializeField] private Transform weapPos;
    private bool _attackInput;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        weap = GetComponentInChildren<P_Weapon>();

        weaponOffset = weapPos.position - transform.position;

    }

    private void FixedUpdate() {
        SetPlayerVelocity();
        WeaponOrbitAroundPlayer();
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
    
    private void WeaponOrbitAroundPlayer () {
        mousePosition = Mouse.current.position.ReadValue();
        // Calculate the direction of the orbit
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        orbitDirection = mousePosition - playerScreenPosition;

        // Calculate the new position of the weapon
        Vector2 newWeaponPosition = (Vector2)weapPos.position + orbitDirection.normalized * weaponOffset.magnitude;

        // Move the weapon towards the new position
        Vector2 weaponMovement = newWeaponPosition - (Vector2)weapPos.position;
        weapPos.position += (Vector3)weaponMovement * orbitSpeed * Time.fixedDeltaTime;

        // Rotate the weapon towards the orbit direction
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        weapPos.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
