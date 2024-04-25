using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAttackBonus : Effect
{
    public override void Init(float[] args)
    {
        GameObject.Find("Gun").GetComponent<Shooter>().damage *= (1 + args[0]);
    }
}
