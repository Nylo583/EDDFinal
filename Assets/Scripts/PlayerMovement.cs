using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    //states
    public bool isMoveable, isDamageable, isInAir, 
        canInteractWithTotem, isTotemHeld,
        canShoot;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isMoveable = true;
        isDamageable = true;
        isInAir = false;
        currentJumps = 0;
        canInteractWithTotem = false;
        canShoot = true;
        totem = GameObject.Find("Totem");
        isTotemHeld = false;
        //lineRenderer = GetComponent<LineRenderer>();
        platformMover = GameObject.Find("MovingWrapper").GetComponent<PlatformMover>();
    }

    void Update() {
        Jump();
        ModulateGravity();
        InteractWithTotem();
    }

    void FixedUpdate()
    {
        Walk();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals("Platform")) {
            isInAir = false;
            currentJumps = maxJumps;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag.Equals("Platform")) {
            isInAir = true;
        }
    }

    [SerializeField]
    float walkSpeed;

    [SerializeField]
    [Range(0f, 1f)]
    float deadzone;

    private PlatformMover platformMover;

    private void Walk() {
        float hInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(hInput) > deadzone && isMoveable) {
            rb.velocity = new Vector2(hInput * walkSpeed - platformMover.speed, rb.velocity.y);
        } else if (Mathf.Abs(hInput) <= deadzone) {
            rb.velocity = new Vector2(-platformMover.speed, rb.velocity.y);
        }
    }

    [SerializeField]
    float jumpImpulse;

    [SerializeField]
    [Range(1, 5)]
    int maxJumps;

    private int currentJumps;

    private void Jump() {

        if (Input.GetKeyDown(KeyCode.Space) && (!isInAir || currentJumps > 0)) {
            currentJumps--;

            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    private void ModulateGravity() {
        if (isInAir) { 
            if (!isInAir || (Input.GetKey(KeyCode.Space) && rb.velocity.y >= 0.2f)) {
                rb.gravityScale = 1;
            } else {
                rb.gravityScale = 2.25F;
            }
        }
    }

    GameObject totem;

    [SerializeField]
    float totemInteractRadius;

    [SerializeField]
    GameObject totemSitPoint;

    private void InteractWithTotem() {
        float dist = Mathf.Sqrt(Mathf.Pow(this.transform.position.y - totem.transform.position.y, 2f) +
            Mathf.Pow(this.transform.position.x - totem.transform.position.x, 2f));
        //Debug.Log(dist);
        canInteractWithTotem = dist <= totemInteractRadius;

        if (Input.GetKeyDown(KeyCode.Mouse1) && isTotemHeld) {
            isTotemHeld = false;
            canShoot = true;
            totem.transform.SetParent(null, true);
            ThrowTotem();
            lineRenderer.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Q) && isTotemHeld) {
            isTotemHeld = false;
            canShoot = true;
            totem.transform.SetParent(null, true);
            lineRenderer.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && canInteractWithTotem && !isTotemHeld) {
            totem.transform.SetParent(this.transform);
            totem.GetComponent<Rigidbody2D>().rotation = 0f;
            totem.GetComponent<Rigidbody2D>().velocity = new Vector2();
            totem.transform.localPosition = totemSitPoint.transform.localPosition;
            isTotemHeld = true;
            canShoot = false;
            lineRenderer.enabled = true;
        }

        if (isTotemHeld) {
            totem.GetComponent<Rigidbody2D>().rotation = 0f;
            totem.GetComponent<Rigidbody2D>().velocity = new Vector2();
            totem.transform.localPosition = totemSitPoint.transform.localPosition;

            StartCoroutine(RenderTrajectory());
        } else {
            if (Mathf.Abs(totem.GetComponent<Rigidbody2D>().velocity.magnitude) < platformMover.speed) {
                totem.GetComponent<Rigidbody2D>().velocity = new Vector2(- platformMover.speed,
                    totem.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }

    [SerializeField]
    float maxForceRadius;

    [SerializeField]
    float maxForce;

    [SerializeField]
    float interval;

    [SerializeField]
    int segments;

    [SerializeField]
    private LineRenderer lineRenderer;

    private void ThrowTotem() {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        float angle = Mathf.Atan2(mousePos.y - totemSitPoint.transform.position.y,
                                    mousePos.x - totemSitPoint.transform.position.x);

        float force = (Mathf.Min(Vector2.Distance(totemSitPoint.transform.position, mousePos), maxForceRadius) / maxForceRadius) * maxForce;

        Vector2 forceVector = new Vector2(force * Mathf.Cos(angle), force * Mathf.Sin(angle));

        totem.GetComponent<Rigidbody2D>().velocity = forceVector;
        totem.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-350, 350);
    }

    private IEnumerator RenderTrajectory() {
        List<Vector2> positions = new List<Vector2>();
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y);


        float angle = Mathf.Atan2(mousePos.y - totemSitPoint.transform.position.y, 
                                    mousePos.x - totemSitPoint.transform.position.x);

        float force = (Mathf.Min(Vector2.Distance(totemSitPoint.transform.position, mousePos), maxForceRadius) / maxForceRadius) * maxForce;

        Vector2 forceVector = new Vector2(force * Mathf.Cos(angle), force * Mathf.Sin(angle));

        Vector2 initialPos = totemSitPoint.transform.position;
        Vector2 currentStepPos = new Vector2();
        lineRenderer.positionCount = segments;

        for (float counter = 0; counter < segments*interval-.01f; counter += interval) {
            currentStepPos.x = forceVector.x * counter + initialPos.x;
            currentStepPos.y = (0.5f * Physics2D.gravity.y * counter * counter)
                                + forceVector.y * counter
                                + initialPos.y;

            //Debug.Log(currentStepPos.x + "  " + currentStepPos.y);
            positions.Add(currentStepPos);
        }

        Vector3[] linePositions = positions.Select(v2 => new Vector3(v2.x, v2.y, 0)).ToArray();
        lineRenderer.SetPositions(linePositions);

        yield break;
    }
}
