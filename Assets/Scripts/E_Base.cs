using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Base : MonoBehaviour
{
    public float speed = 3f;
    public float rotationSpeed = 1f;
    private Transform target;
    public int _health = 100;
    [SerializeField]
    private Transform resetPoint;
    private CircleCollider2D childCol;
    private Rigidbody2D rb;

    private void Awake() {
        childCol = GetComponentInChildren<CircleCollider2D>();
        target = resetPoint;
    }

    private void Update() {
        Die();
        Movement();
        RotateTowardsTarget();
    }

    private void Movement () {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, step);
    }

    private void RotateTowardsTarget()
    {
        var offset = 90f;
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       
        Quaternion rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == 6){
            target = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == 6){
            target = resetPoint;
            Debug.Log(target.name);
        }
    }

    private void Die () {
        if(_health <= 0){
            Destroy(gameObject);
        }        
    }
}