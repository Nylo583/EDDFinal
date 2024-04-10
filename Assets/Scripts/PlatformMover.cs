using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField]
    public float speed;

    private void Update() {
        foreach (Transform child in this.transform) {
            child.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, child.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
