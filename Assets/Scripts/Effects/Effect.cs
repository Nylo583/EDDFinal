using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public abstract void Init(params object[] args);
    public virtual void OnBulletSpawn(GameObject target) { }
    public virtual void OnBulletTravel(GameObject target) { }
    public virtual void OnBulletHit(GameObject target) { }
    public virtual void OnBulletDestroy(GameObject target) { }
}
