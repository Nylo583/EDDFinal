using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        score += Time.deltaTime * (difficulty / 40);
        difficulty += Time.deltaTime / 3;
        UpdateScoreboard();
    }

    public void AddScore(float val) { score += val; }
    public void RemoveScore(float val) { score -= val; }
    void UpdateScoreboard() {
        scoreBoard.GetComponent<TMP_Text>().text = Mathf.RoundToInt(score).ToString();
    }
}
