using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class CardGenSelect : MonoBehaviour
{

    public GameObject pCard;
    public GameObject cardContainer;

    public Card[] cardTable;
    public EffectData[] effectDataTable;
    public float[] rarityTable;
    public Color[] colorTable;

    public Transform leftTransform;
    public Transform middleTransform;
    public Transform rightTransform;
    
    private void Awake()
    {
        rarityTable = new float[4] { 0f, .45f, .75f, .85f };
        colorTable = new Color[4] {new Color(1, 1, 1),
                                new Color(77f/255, 232f/255, 77f/255, 1),
                                new Color(194f/255, 77f/255, 1, 1),
                                new Color(1, 216f/255, 77f/255, 1)};

        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        cardTable = Resources.LoadAll<Card>("ScriptableObjs/Cards");
        effectDataTable = Resources.LoadAll<EffectData>("ScriptableObjs/EffectDatas");
        //Debug.Log(cardTable.Length);
        
        foreach (EffectData effectData in effectDataTable)
        {
            effectData.parseData();
        }
    }

    private void OnEnable() {
        
    }

    public void CreateOverlay()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        Time.timeScale = 0f;
        GameObject.Find("Canvas").GetComponent<CanvasGroup>().alpha = 0f;
        CreateRandomCard(leftTransform);
        CreateRandomCard(middleTransform);
        CreateRandomCard(rightTransform);
    }

    void CreateRandomCard(Transform pos) {
        int rarity = -1;
        float val = UnityEngine.Random.value;
        //Debug.Log(val);
        for (int i = rarityTable.Length - 1; i >= 0; i--)
        {
            if (val >= rarityTable[i] && rarity == -1)
            {
                rarity = i;
            }
        }

        int selection = UnityEngine.Random.Range(0, cardTable.Length);
        Card card = cardTable[selection].Clone() as Card;
        EffectData ed = null;
        foreach (EffectData effectData in effectDataTable)
        {
            if (cardTable[selection].name == effectData.name)
            {
                ed = effectData.Clone() as EffectData;
                ed.valuesPerRarity = ed.parseData();
            }
        }
        //Debug.Log(ed.valuesPerRarity);
        float[] vals = new float[ed.valuesPerRarity.Length];
        for (int i = 0; i < vals.Length; i++) {
            vals[i] = ed.valuesPerRarity[i][rarity];
        }

        card.args = vals;

        GameObject obj = Instantiate(pCard, Vector3.zero, new Quaternion(), GameObject.Find("CardContainer").transform);

        CardRenderer cr = obj.GetComponent<CardRenderer>();

        cr.card = card;
        foreach (var v in card.args) {
            Debug.Log(v);
        }
        cr.effectData = ed;
        cr.Render();
        obj.transform.position = pos.position;
        obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colorTable[rarity];
    }

    public void DestroyOverlay() {
        foreach (Transform child in GameObject.Find("CardContainer").transform) {
            Destroy(child.gameObject);
        }
        Time.timeScale = 1.0f;
        GameObject.Find("Canvas").GetComponent<CanvasGroup>().alpha = 1f;
    }
}
