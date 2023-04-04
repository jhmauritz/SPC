using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class P_Weapon : MonoBehaviour
{
        //Attack
    [SerializeField]
    private CircleCollider2D _weapCol;
    [SerializeField]
    private SpriteRenderer _weapSprite;
    public Vector2 PointerPosition {get; set;}



    private void Awake() {
    }

    private void Update() {
        //NewMousePos();
    }

    private void RotateWeapon(){
        transform.right = (PointerPosition-(Vector2)transform.position).normalized;
    }

    public IEnumerator Attack(){
        if(_weapCol.enabled == false){
            _weapCol.enabled = true;
            Debug.Log("Col On");
            yield return new WaitForSeconds(0.01f);
            _weapCol.enabled = false;
            Debug.Log("Col Off");
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject != null){
            if(other.gameObject.layer == 7){
                other.gameObject.GetComponent<E_Base>()._health = 0;
                Debug.Log(other.gameObject.GetComponent<E_Base>()._health);
            }
        }
    }

    private void NewMousePos () {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float anfleDeg = (180/Mathf.PI) * angleRad;
        this.transform.rotation = Quaternion.Euler(0,0, anfleDeg);

    }
}
