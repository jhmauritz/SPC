using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Follow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    private void Update() {
        transform.position = new Vector3(player.position.x * offset.x, player.position.y * offset.y, player.position.z * offset.z);
    }
}
