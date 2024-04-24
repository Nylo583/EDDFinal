using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New EffectData", menuName = "EffectData")]
public class EffectData : ScriptableObject, ICloneable
{
    public string effect;
    public string data;
    public float[][] valuesPerRarity;

    public float[][] parseData()
    {
        int rows = data.Split('/').Count();
        //Debug.Log(rows);
        float[][] vals = new float[rows][];

        for (int i = 0; i < vals.Length; i++)
        {
            vals[i] = Array.ConvertAll(data.Split("/")[i].Split(","), float.Parse);
        }
        return vals;
    }


    public EffectData(string e, string d, float[][] vpr) {
        effect = e;
        data = d;
        valuesPerRarity = vpr;
        parseData();
    }

    public object Clone() {
        return new EffectData(effect, data, valuesPerRarity);
    }
}
