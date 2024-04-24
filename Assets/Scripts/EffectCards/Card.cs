using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public Sprite image;
    public string nameText;
    public string descriptionText;
    public float[] args;
    public bool[] isPercent;
}
