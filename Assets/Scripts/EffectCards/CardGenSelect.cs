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

    public float[] rarityTable = { 0f, .65f, .85f, .95f };
    public Color[] colorTable = {new Color(1, 1, 1),
                                new Color(77f/255, 232f/255, 77f/255, 1),
                                new Color(194f/255, 77f/255, 1, 1),
                                new Color(1, 216f/255, 77f/255, 1)};

    public Card[] cardTable;
    public EffectData[] effectDataTable;

    private void Awake()
    {
        cardTable = Resources.LoadAll<Card>("ScriptableObjs/Cards");
        effectDataTable = Resources.LoadAll<EffectData>("ScriptableObjs/EffectDatas");
        Debug.Log(cardTable.Length);
        
        foreach (EffectData effectData in effectDataTable)
        {
            effectData.parseData();
        }
    }

    void CreateRandomCard() {
        int rarity = -1;
        float val = UnityEngine.Random.value;

        for (int i = rarityTable.Length - 1; i >= 0; i--)
        {
            if (val >= rarityTable[i])
            {
                rarity = i;
            }
        }

        int selection = UnityEngine.Random.Range(0, cardTable.Length - 1);
        Card card = cardTable[selection].Clone() as Card;
        EffectData ed = new EffectData();
        foreach (EffectData effectData in effectDataTable)
        {
            if (card.name == effectData.name)
            {
                ed = effectData; 
            }
        }
        float[] vals = new float[ed.valuesPerRarity[0].Length];
    }

}
