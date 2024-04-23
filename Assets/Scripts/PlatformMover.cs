using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField]
    public float baseSpeed;

    [SerializeField]
    public float speed;

    private void Update() {
        speed = baseSpeed * GameObject.Find("WorldVariables").GetComponent<WorldVariables>().difficulty / 4;
        speed = Mathf.Clamp(speed, 0, 5);
        foreach (Transform child in this.transform) {
            child.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, child.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
