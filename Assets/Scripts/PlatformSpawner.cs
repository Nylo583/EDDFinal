using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private int xcoord;

    [SerializeField]
    float yBot;

    [SerializeField]
    float yTop;

    [SerializeField]
    float bottomThreshold;

    [SerializeField]
    float topThreshold;

    [SerializeField]
    float botStreakAdd;

    [SerializeField]
    float topStreakAdd;

    [SerializeField]
    GameObject pTopPlatformChunk;

    [SerializeField]
    GameObject pBtmPlatformChunk;

    private GameObject movingWrapper;
    private float halfUnitTimer;
    private bool lastSpawnedBottom = false;
    private bool lastSpawnedTop = false;
    private void Start() {
        movingWrapper = GameObject.Find("MovingWrapper");
        halfUnitTimer = 0f;
        xcoord = 0;
        Random.InitState(System.DateTime.Today.Millisecond);
    }

    private void Update() {
        halfUnitTimer += movingWrapper.GetComponent<PlatformMover>().speed * Time.deltaTime;
        if (halfUnitTimer >= 9.99f) {
            SpawnNewPlatforms();
            halfUnitTimer = 0f;
            xcoord++;
        }
    }

    private void SpawnNewPlatforms() {
        float topSpawn = Random.Range(0f, 1f);
        float botSpawn = Random.Range(0f, 1f);
        if (lastSpawnedBottom) { botSpawn += botStreakAdd; }
        //Debug.Log(xcoord + " " +topSpawn + " " + botSpawn + " " + Mathf.PerlinNoise(xcoord/100, 1/100) + " " + Mathf.PerlinNoise(xcoord / 100, -1/100));
        

        if (botSpawn > bottomThreshold) {
            Instantiate(pBtmPlatformChunk, new Vector2(this.transform.position.x, this.transform.position.y + yBot),
                new Quaternion(), movingWrapper.transform);
            lastSpawnedBottom = true;

        } else {
            lastSpawnedBottom = false;
        }

        if (lastSpawnedTop) { topSpawn += topStreakAdd; }
        if (topSpawn > topThreshold) {
            Instantiate(pTopPlatformChunk, new Vector2(this.transform.position.x, this.transform.position.y + yTop),
                new Quaternion(), movingWrapper.transform);
            lastSpawnedTop = true;
        } else {
            lastSpawnedTop = false;
        }
    }
}
