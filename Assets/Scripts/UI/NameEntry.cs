using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameEntry : MonoBehaviour
{
    int counter;
    GameObject[] texts;
    bool canEdit;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        texts = new GameObject[3] {
            GameObject.Find("Text0").gameObject,
            GameObject.Find("Text1").gameObject,
            GameObject.Find("Text2").gameObject
        };

        GameObject.Find("Score").GetComponent<TMP_Text>().text = PersistingData.Instance.SCORE + " souls";

        canEdit = false;
        StartCoroutine(Timeout());
    }

    // Update is called once per frame
    void Update()
    {
        if (canEdit) {
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                GameObject text = GameObject.Find("Text" + counter).gameObject;
                TMP_Text tmp = text.GetComponent<TMP_Text>();
                tmp.text = "-";
                if (counter > 0) { counter--; }
            } else if (Input.inputString.Length == 1 && Input.inputString != " ") {
                GameObject text = GameObject.Find("Text" + counter).gameObject;
                TMP_Text tmp = text.GetComponent<TMP_Text>();
                tmp.text = Input.inputString;
                if (counter < 2) { counter++; }
            }

            foreach (GameObject g in texts) {
                if (g.name != ("Text" + counter)) {
                    g.GetComponent<TMP_Text>().color = new Color(0.6745304f, 0.4493592f, 0.9622642f);
                } else {
                    g.GetComponent<TMP_Text>().color = new Color(0.9607843f, 0.4509804f, 0.8479824f);
                }
            }
        }
    }

    IEnumerator Timeout() {
        yield return new WaitForSeconds(1.5f);
        canEdit = true;
    }
}
