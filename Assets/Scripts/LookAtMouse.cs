using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtMouse : MonoBehaviour
{
    private void Update() {
        Vector2 mouseCursorPos = Mouse.current.position.ReadValue();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mouseCursorPos);
        Vector2 dir = mousePos - (Vector2)transform.position;
        transform.up = dir.normalized;
    }
}
