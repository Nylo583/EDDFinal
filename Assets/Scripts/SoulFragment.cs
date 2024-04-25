using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFragment : MonoBehaviour
{
    bool didCreate;
    private void Start()
    {
        didCreate = false;
    }
    // Update is called once per frame
    void Update()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(this.transform.position, 5f);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.tag == "Player" && !didCreate)
            {
                SignalCardOverlay();
            }
        }

        void SignalCardOverlay()
        {
            didCreate = true;
            GameObject cardOverlay = GameObject.Find("CardSelectOverlay");
            cardOverlay.GetComponent<CardGenSelect>().CreateOverlay();
            Destroy(this.gameObject);
        }
    }
}
