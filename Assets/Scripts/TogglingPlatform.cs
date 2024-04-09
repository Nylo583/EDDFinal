using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglingPlatform : MonoBehaviour
{

    [SerializeField]
    Sprite missingSprite;

    [SerializeField]
    Sprite enabledSprite;

    [SerializeField]
    int toggleProbabilityMaximum;

    [SerializeField]
    Collider2D col;

    [SerializeField]
    float minToggleDelay;

    private SpriteRenderer sr;

    private bool alreadyQueuedForToggle;
    private bool isCurrentlyIntersected;
    private bool isEnabled;
    private bool canRoll;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        alreadyQueuedForToggle = false;
        isCurrentlyIntersected = false;
        isEnabled = true;
        Random.InitState(Time.frameCount);
        canRoll = true;
    }

    void Update()
    {
        if (canRoll) {
            StartCoroutine(RollForToggle());
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        isCurrentlyIntersected = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        isCurrentlyIntersected = false;
    }

    IEnumerator RollForToggle() {
        canRoll = false;
        Debug.Log("Waiting timer");
        yield return new WaitForSeconds(minToggleDelay);
        Debug.Log("Now rolling");

        while (true) {
            yield return new WaitForSeconds(Time.fixedDeltaTime);
            int val = Mathf.RoundToInt(Random.Range(0, toggleProbabilityMaximum));
            //Debug.Log(val);
            bool toggle = val == 13;

            if (toggle && !alreadyQueuedForToggle) {
                alreadyQueuedForToggle = true;
                StartCoroutine(WaitAndToggle());
                yield break;
            }
        }
        
    }

    IEnumerator WaitAndToggle() {

        while (isCurrentlyIntersected) {
            yield return null;
        }

        if (!isEnabled) {
            isEnabled = true;
            sr.sprite = enabledSprite;
            col.enabled = true;
            this.gameObject.tag = "Platform";
        } else {
            yield return new WaitForSeconds(.5f);
            isEnabled = false;
            sr.sprite = missingSprite; 
            col.enabled = false;
            this.gameObject.tag = "PlatformDisabled";
        }

        alreadyQueuedForToggle = false;
        canRoll = true;
    }
}
