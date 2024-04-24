using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardRenderer : MonoBehaviour
{
    public Card card;
    public SpriteRenderer imageRenderer;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public float[] args;
    public bool[] isPercent;

    public string desc;

    private void OnEnable() {
        imageRenderer.sprite = card.image;
        nameText.text = card.nameText;
        desc = card.descriptionText;
        args = card.args;
        isPercent = card.isPercent;
        bool edited = false;
        for (int j = 0; j < args.Length; j++) {
            int limit = desc.Length;
            edited = false;
            for (int i = 0; i < limit; i++) {
                Debug.Log(j + " " + i + " " + desc.Length);
                if (!edited) {
                    if (desc[i] == '#') {

                        desc = desc.Remove(i, 1);
                        float val = args[j];
                        string add = "";
                        if (isPercent[j]) {
                            val *= 100;
                            add = "%";
                        }
                        int v = ((int)val);

                        string build = "<b>" + v + add + "</b>";
                        desc = desc.Substring(0, i) + build + desc.Substring(i);
                        edited = true;
                    }
                } 
            }
        }

        Debug.Log(desc);
        descriptionText.text = desc;
    }
}
