using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public string weaponType;
    public GameObject bulletObject;
    public float bulletSpeed, damage;
    private GameObject parentObject;
    private string weaponOwner;
    Vector2 pointDirection;
    Vector2 parentInitScale;
    // Start is called before the first frame update
    void Start()
    {
        parentObject = transform.parent.gameObject;
        parentInitScale = parentObject.transform.localScale;

        weaponOwner = parentObject.tag;

        if(weaponOwner.Equals("Player"))
    }

    void Update() {
        if(weaponOwner.Equals("Player")) {
            if(Input.GetMouseButtonDown(0)) {
                if(weaponType.Equals("Gun")) {
                    Vector3 firePoint = transform.GetChild(0).position;
                    pointDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObject.transform.position;
                    pointDirection = pointDirection.normalized;
                    
                    GameObject newBullet = Instantiate(bulletObject, firePoint, transform.rotation);
                    newBullet.GetComponent<BulletController>().shotBy = parentObject.tag;
                    newBullet.GetComponent<Rigidbody2D>().velocity = pointDirection * bulletSpeed;
                } else {
                    // MELEE WEAPON ATTACK CODE HERE
                }
            }
        } else {
            // ENEMY WEAPON CODE HERE
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(weaponOwner.Equals("Player")) {
            if(Input.mousePresent) { //CHANGE LATER
                pointDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObject.transform.position;
                pointDirection = pointDirection.normalized;

                float angle = Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg;

                if(pointDirection.x >= 0f) {
                    transform.rotation = Quaternion.Euler(0f, 0f, angle);
                    parentObject.transform.localScale = parentInitScale;
                } else {
                    transform.rotation = Quaternion.Euler(0f, 0f, angle + 180);
                    parentObject.transform.localScale = new Vector2(-parentInitScale.x, parentInitScale.y);
                }
            }
        }
    }
}
