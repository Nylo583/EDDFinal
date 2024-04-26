using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage;
    public float radius;
    public bool animatedDone;

    public void Init(float d, float r)
    {
        damage = d;
        radius = r;
        this.transform.localScale = new Vector3(1, 1, 1) * (radius / 5);
        StartCoroutine(AnimateAndDestroy());
    }

    IEnumerator AnimateAndDestroy()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("Explode");
        while (!animatedDone)
        {
            yield return null;
        }

        Collider2D[] cols = Physics2D.OverlapCircleAll(this.transform.position, radius);
        foreach (Collider2D col in cols)
        {
            if (col.tag == "Enemy")
            {
                col.GetComponent<HealthComponent>().RemoveHealth(damage*GameObject.Find("Gun").GetComponent<Shooter>().damage);
            }
        }
        Destroy(this.gameObject);
    }


}
