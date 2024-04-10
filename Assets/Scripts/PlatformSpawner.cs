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
    GameObject pTopPlatformChunk;

    [SerializeField]
    GameObject pBtmPlatformChunk;

    private GameObject movingWrapper;
    private float halfUnitTimer;

    private void Start() {
        movingWrapper = GameObject.Find("MovingWrapper");
        halfUnitTimer = 0f;
        xcoord = 0;
        Random.InitState(System.DateTime.Today.Millisecond);
    }

    private void Update() {
        halfUnitTimer += movingWrapper.GetComponent<PlatformMover>().speed * Time.deltaTime;
        if (halfUnitTimer >= 4.95f) {
            SpawnNewPlatforms();
            halfUnitTimer = 0f;
            xcoord++;
        }
    }

    private void SpawnNewPlatforms() {
        bool topSpawn = Random.Range(0f, 1f) > topThreshold;
        bool botSpawn = Random.Range(0f, 1f) > bottomThreshold;
        //Debug.Log(xcoord + " " +topSpawn + " " + botSpawn + " " + Mathf.PerlinNoise(xcoord/100, 1/100) + " " + Mathf.PerlinNoise(xcoord / 100, -1/100));
        if (topSpawn) {
            Instantiate(pTopPlatformChunk, new Vector2(this.transform.position.x, yTop), 
                new Quaternion(), movingWrapper.transform);
        }

        if (botSpawn) {
            Instantiate(pBtmPlatformChunk, new Vector2(this.transform.position.x, yBot),
                new Quaternion(), movingWrapper.transform);
        }
    }
}
