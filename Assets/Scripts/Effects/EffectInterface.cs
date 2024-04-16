using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface EffectInterface
{
    public void Init(params object[] args);
    public void OnBulletSpawn(GameObject target);
    public void OnBulletTravel(GameObject target);
    public void OnBulletHit(GameObject target);
    public void OnBulletDestroy(GameObject target);
}
