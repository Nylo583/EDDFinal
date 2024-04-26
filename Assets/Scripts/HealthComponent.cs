using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    public float maxHealth;

    public float health;

    #nullable enable
    [SerializeField]
    GameObject? healthBarAssociated;

    [SerializeField]
    SceneLoader? sceneLoader;
    #nullable disable

    public bool canDropFragment;

    GameObject pFragment;

    [SerializeField]
    float baseScoreValue = 0;
    WorldVariables wv;
    private void Start() {
        health = maxHealth;
        wv = GameObject.Find("WorldVariables").GetComponent<WorldVariables>();
        pFragment = Resources.Load("Prefabs/SoulFragment") as GameObject;
    }

    private void Update() {
        if (health <= 0) {
            wv.AddScore(baseScoreValue * wv.difficulty);
            if (canDropFragment) {
                UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
                float v = Random.value;
                float threshold = (1 / (30 - Mathf.Max(0, 20 - (wv.difficulty/5))));
                Debug.Log(v + "<" + threshold);
                if (v <  threshold) {
                    Instantiate(pFragment, this.transform.position, new Quaternion(), null);
                    Destroy(this.gameObject);
                }
            }
            if (sceneLoader != null) { sceneLoader.LoadScene("DeathMenu"); }
            else { Destroy(gameObject); }
        }

        health = Mathf.Min(health, maxHealth);

        if (healthBarAssociated != null) {
            healthBarAssociated.GetComponent<HealthBar>().ModulateFill(health/maxHealth);
        }
    }

    public void AddHealth(float add) {
        health += add;
    }

    public void RemoveHealth(float remove) {
        health -= remove;
    }

    public float GetHealth() {
        return health;
    }

    public void SetMaxHealth(float max)
    {
        maxHealth = max;
    }

    public void SetHealth(float val)
    {
        health = val;
    }
}
