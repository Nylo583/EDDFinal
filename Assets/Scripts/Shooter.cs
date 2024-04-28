using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    public float attacksPerSecond;

    [SerializeField]
    public float damage;

    [SerializeField]
    float bulletSpeed;

    private float counter;
    private float timer;
    [SerializeField]
    private GameObject effectContainer;
    [SerializeField]
    public GameObject pBaseBullet;
    private GameObject bulletContainer;

    private PlayerMovement pm;

    private void Start() {
        counter = 0f;
        
        //  effectContainer = this.transform.GetChild(0).gameObject;
        //pBaseBullet = (GameObject)Resources.Load("/Assets/Prefabs/BaseBullet.prefab", typeof(GameObject));
        bulletContainer = GameObject.Find("BulletContainer");
        pm = this.transform.parent.gameObject.GetComponent<PlayerMovement>();
    }

    private void Update() {
        timer = 1 / attacksPerSecond;
        counter += Time.deltaTime;

        if (counter > timer && Input.GetKey(KeyCode.Mouse0) && pm.canShoot) {
            counter = 0f;

            Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

            float angle = Mathf.Atan2(mousePos.y - this.transform.position.y,
                                        mousePos.x - this.transform.position.x);

            GameObject bullet = Instantiate(pBaseBullet);
            bullet.transform.position = this.gameObject.transform.position;
            bullet.transform.parent = bulletContainer.transform;
            bullet.GetComponent<Bullet>().Init(angle, bulletSpeed, damage, effectContainer);
        }
    }
}
