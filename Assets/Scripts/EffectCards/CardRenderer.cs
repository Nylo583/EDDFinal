using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CardRenderer : MonoBehaviour
{
    public Card card;
    public EffectData effectData;
    public SpriteRenderer imageRenderer;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public float[] args;
    public bool[] isPercent;

    public string desc;

    public void Render() {
        imageRenderer.sprite = card.image;
        nameText.text = card.nameText;
        desc = card.descriptionText;
        args = card.args;
        isPercent = card.isPercent;
        UpdateCard();
    }

    void UpdateCard() {
        bool edited = false;
        for (int j = 0; j < args.Length; j++) {
            int limit = desc.Length;
            edited = false;
            for (int i = 0; i < limit; i++) {
                //Debug.Log(j + " " + i + " " + desc.Length);
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

        //Debug.Log(desc);
        descriptionText.text = desc;
    }

    private void OnMouseDown() {
        GameObject effectContainer = GameObject.Find("EffectContainer");
        bool didConsolidate = false;
        if (effectContainer.GetComponents(Type.GetType(effectData.effect)).Count() > 0)
        {
            didConsolidate = (effectContainer.GetComponent(Type.GetType(effectData.effect)) as Effect).Consolidate(card.args);
        }

        if (!didConsolidate)
        {
            Effect effect = effectContainer.AddComponent(Type.GetType(effectData.effect)) as Effect;
            effect.Init(card.args);
        }
        
        this.transform.parent.parent.GetComponent<CardGenSelect>().DestroyOverlay();
    }
}
