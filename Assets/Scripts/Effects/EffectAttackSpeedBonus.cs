using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAttackSpeedBonus : Effect
{
    public override void Init(float[] args) {
        GameObject.Find("Gun").GetComponent<Shooter>().attacksPerSecond *= (1 + args[0]);
    }
}
