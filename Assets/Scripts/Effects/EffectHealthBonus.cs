using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHealthBonus : Effect
{
    float pctBonus;

    public override void Init(float[] args)
    {
        pctBonus = args[0];
        HealthComponent playerHealth = GameObject.Find("Player").GetComponent<HealthComponent>();
        HealthComponent totemHealth = GameObject.Find("Totem").GetComponent<HealthComponent>();

        float playerPct = playerHealth.health / playerHealth.maxHealth;
        playerHealth.SetMaxHealth(pctBonus*playerHealth.maxHealth);
        playerHealth.SetHealth(playerPct*playerHealth.maxHealth);

        float totemPct = totemHealth.health / totemHealth.maxHealth;
        totemHealth.SetMaxHealth(pctBonus * totemHealth.maxHealth);
        totemHealth.SetHealth(totemPct*totemHealth.maxHealth);
    }


}
