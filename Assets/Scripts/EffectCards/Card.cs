using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName = "Card")]
public class Card : ScriptableObject, ICloneable
{
    public Sprite image;
    public string nameText;
    public string descriptionText;
    public float[] args;
    public bool[] isPercent;

    public Card(Sprite i, string name, string desc, float[] a, bool[] ip)
    {
        image = i;
        nameText = name;
        descriptionText = desc;
        args = a;
        isPercent = ip;
    }

    public object Clone()
    {
        return new Card(image, nameText, descriptionText, args, isPercent);
    }
}
