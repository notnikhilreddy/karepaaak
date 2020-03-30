using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject bulletObject;
    public float bulletSpeed;
    private GameObject parentObject;
    Vector3 pointDirection;
    Vector2 parentInitScale;
    // Start is called before the first frame update
    void Start()
    {
        parentObject = transform.parent.gameObject;
        parentInitScale = parentObject.transform.localScale;
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 firePoint = transform.GetChild(0).position;
            pointDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObject.transform.position;
            pointDirection.Normalize();
             
            GameObject newBullet = Instantiate(bulletObject, firePoint, transform.rotation);
            newBullet.GetComponent<BulletController>().shotBy = parentObject.tag;
            newBullet.GetComponent<Rigidbody2D>().velocity = pointDirection * bulletSpeed;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.mousePresent) { //CHANGE LATER
            pointDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObject.transform.position;
            pointDirection.Normalize();
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
