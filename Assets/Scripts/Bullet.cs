using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float directionRads;
    public float speed;
    public float damage;
    public bool canSpawnMoreBullets;

    public abstract void Init(float dirRads, float spd, float dmg, GameObject effects);
}
