using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldVariables : MonoBehaviour
{
    public float score;
    public float difficulty;
    GameObject scoreBoard;

    private void Start() {
        difficulty = 1;
        scoreBoard = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * (difficulty / 2);
        difficulty += Time.deltaTime / 30;
        UpdateScoreboard();
    }

    public void AddScore(float val) { score += val; }
    public void RemoveScore(float val) { score -= val; }
    void UpdateScoreboard() {
        scoreBoard.GetComponent<Text>().text = Mathf.RoundToInt(score).ToString();
    }
}
