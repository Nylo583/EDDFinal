using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    WorldVariables wv;
    bool canSpawnWave;

    [SerializeField]
    float waveTimer;

    [SerializeField]
    GameObject pBird;

    [SerializeField]
    GameObject pCannon;

    void Start()
    {
        wv = GameObject.Find("WorldVariables").GetComponent<WorldVariables>();
        canSpawnWave = true;
    }

    private void Update()
    {
        if (canSpawnWave)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        canSpawnWave = false;
        float pctCannon = .5f / (1 + Mathf.Exp(-5f * (wv.difficulty - 15)));
        int numEnemies = Mathf.Max(1, Mathf.RoundToInt(wv.difficulty * .25f));
        int numCannons = Mathf.RoundToInt(numEnemies * pctCannon);
        int numBirds = numEnemies - numCannons;
        //Debug.Log(pctCannon + " " +  numEnemies);
        float x = UnityEngine.Random.Range(50f, 65f);
        float y = UnityEngine.Random.Range(50f, 65f);
        float modX = UnityEngine.Random.value < .5 ? -1 : 1;
        float modY = UnityEngine.Random.value < .5 ? -1 : 1;

        for (int i = 0; i < numCannons; i++)
        {
            GameObject enemy = Instantiate(pCannon, this.transform);
            enemy.transform.localPosition = new Vector2(x * modX, y * modY);
            yield return new WaitForSeconds(.5f);
        }

        x = UnityEngine.Random.Range(50f, 65f);
        y = UnityEngine.Random.Range(50f, 65f);
        modX = UnityEngine.Random.value < .5 ? -1 : 1;
        modY = UnityEngine.Random.value < .5 ? -1 : 1;
        for (int i = 0; i < numBirds; i++)
        {
            GameObject enemy = Instantiate(pBird, this.transform);
            enemy.transform.localPosition = new Vector2(x*modX, y*modY);
            yield return new WaitForSeconds(.5f);
        }
        yield return new WaitForSeconds(Mathf.Max(waveTimer - (.5f * numEnemies), 0));
        canSpawnWave = true;
        yield break;
    }
}
