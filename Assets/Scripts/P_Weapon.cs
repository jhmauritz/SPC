using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class P_Weapon : MonoBehaviour
{
        //Attack
    [SerializeField]
    private BoxCollider2D _weapCol;
    [SerializeField]
    private SpriteRenderer _weapSprite;
    public Vector2 PointerPosition {get; set;}

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
}
