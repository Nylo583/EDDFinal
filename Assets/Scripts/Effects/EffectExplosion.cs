using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExplosion : Effect
{
    GameObject pExplosion;
    public float radius;
    public float damage;

    public override void Init(float[] args)
    {
        pExplosion = Resources.Load("Prefabs/Explosion") as GameObject; 
        radius = args[0];
        damage = args[1];
    }

    public override void OnBulletDestroy(GameObject target)
    {
        GameObject o = Instantiate(pExplosion, target.transform.position, new Quaternion(), GameObject.Find("BulletContainer").transform);
        o.GetComponent<Explosion>().Init(damage, radius);
    }

    public override bool Consolidate(float[] args)
    {
        radius += args[0];
        damage += args[1];
        return true;
    }
}
