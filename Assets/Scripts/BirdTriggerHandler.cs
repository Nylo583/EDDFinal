using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BirdTriggerHandler : MonoBehaviour
{
    BirdFSM bfsm;

    [SerializeField] float radius;

    private void Start() {
        bfsm = transform.parent.GetComponent<BirdFSM>();
    }

    /* use for circle collider in inspector
    private void OnTriggerStay2D(Collider2D collision) {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Totem") && !bfsm.isDashing) {
            Debug.Log("Hit " + collision.gameObject.tag);
            bfsm.SetTargetAndDash(collision.gameObject);            
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Totem")) {
            Debug.Log("Left " + collision.gameObject.tag);
        }
    }
    */

    private void Update() {
        Collider2D[] cols = Physics2D.OverlapCircleAll(this.transform.position, radius);
        foreach (Collider2D collision in cols) {
            if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Totem") && !bfsm.isDashing) {
                Debug.Log("Hit " + collision.gameObject.tag);
                bfsm.SetTargetAndDash(collision.gameObject);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
