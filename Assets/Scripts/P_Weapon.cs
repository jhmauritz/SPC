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

    public IEnumerator Attack(){
        if(_weapCol.enabled == false){
            _weapCol.enabled = true;
            _weapSprite.enabled = true;
            Debug.Log("Col On");
            yield return new WaitForSeconds(0.1f);
            _weapCol.enabled = false;
            _weapSprite.enabled = false;
            Debug.Log("Col Off");
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 7){
            other.gameObject.GetComponent<E_Base>()._health = 0;
        }
    }
}
