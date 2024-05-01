using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Leaderboard : MonoBehaviour
{
    [Serializable]
    public class ScorePackage {
        public string Name;
        public int Score;

        public ScorePackage(string n, int s)
        {
            Name = n;
            Score = s;
        }
    }

    public List<ScorePackage> leaderboard;

    private void Awake()
    {
        //Debug.Log(Application.persistentDataPath);
        //leaderboard = JsonUtility.FromJson<List<ScorePackage>>(File.ReadAllText(Application.persistentDataPath + "/Leaderboard.json"));
        string path = Application.persistentDataPath + "/Leaderboard.json";
        leaderboard = new List<ScorePackage>();

        if (!File.Exists(path)) {
            FileStream stream = File.Create(path);
            stream.Close();

            File.WriteAllText(path, JsonConvert.SerializeObject(leaderboard));
        } else {
            leaderboard = JsonConvert.DeserializeObject<List<ScorePackage>>(File.ReadAllText(path));
        }
        
        Debug.Log(leaderboard + " " + leaderboard.Count);
        List<ScorePackage> removes = new List<ScorePackage>();
        if (!(leaderboard == null || leaderboard.Count == 0)) {
            foreach (ScorePackage p in leaderboard) {
                if (p.Name == null && p.Score == 0) {
                    removes.Add(p);
                }
            }

            foreach (ScorePackage p in removes) {
                leaderboard.Remove(p);
            }
        }
        

        if (leaderboard.Count == 0)
        {
            Debug.Log("reset");
            leaderboard = new List<ScorePackage>();
        }
        ScorePackage runScorePackage = new ScorePackage(PersistingData.Instance.NAME, PersistingData.Instance.SCORE);
        leaderboard.Add(runScorePackage);

        Debug.Log(JsonConvert.SerializeObject(leaderboard));
        leaderboard.Sort((p1, p2) => p2.Score - p1.Score);

        File.WriteAllText(path, JsonConvert.SerializeObject(leaderboard));
        
        TMP_Text text110 = GameObject.Find("Scores110").GetComponent<TMP_Text>();
        TMP_Text text1120 = GameObject.Find("Scores1120").GetComponent<TMP_Text>();
        WriteBoard(text110, 0, runScorePackage);
        WriteBoard(text1120, 10, runScorePackage, true);
    }

    void WriteBoard(TMP_Text t, int start, ScorePackage p, bool end = false)
    {
        int idx = -1;
        for (int i = 0; i < leaderboard.Count; i++)
        {
            ScorePackage package = leaderboard[i];
            if (p.Name == package.Name && p.Score == package.Score)
            {
                idx = i; 
                break;
            }
        }
        string build = "";
        for (int i = start; i < Mathf.Min(10, leaderboard.Count-start); i++)
        {
            ScorePackage package = leaderboard[i];
            if (idx == i)
            {
                build += String.Format("<color=#58e88d>{0}. {1}    {2}</color>\n", (i + 1).ToString("D2"), package.Name, package.Score.ToString("D6"));
            } else
            {
                build += String.Format("{0}. {1}    {2}\n", (i + 1).ToString("D2"), package.Name, package.Score.ToString("D6"));
            }
 
        }

        if (leaderboard.Count < start + 10)
        {
            for (int j = Mathf.Max(start, leaderboard.Count); j < 10 + start; j++)
            {
                build += String.Format("{0}. XXX    000000\n", (j + 1).ToString("D2"));
            }
        }
       

        if (end && idx > 20)
        {
            build.Remove(build.Length-21);
            build += String.Format("{0}. {1}    {2}\n", (idx + 1).ToString("D2"), p.Name, p.Score.ToString("D6"));
        }

        t.text = build;
    }
}
