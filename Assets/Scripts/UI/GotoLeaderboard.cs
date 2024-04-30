using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GotoLeaderboard : MonoBehaviour
{
    public void Goto() {
        PersistingData.Instance.NAME = GameObject.Find("Text0").gameObject.GetComponent<TMP_Text>().text +
            GameObject.Find("Text1").gameObject.GetComponent<TMP_Text>().text +
            GameObject.Find("Text2").gameObject.GetComponent<TMP_Text>().text;

        //load leaderboard scene
        //this.gameObject.GetComponent<SceneLoader>().LoadScene("LeaderboardMenu");
    }
}
