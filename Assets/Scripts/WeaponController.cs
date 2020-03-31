using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public string weaponType;
    public float bulletSpeed, damage;
    public GameObject bulletObject;
   
    private GameObject parentObject;
    private string weaponOwner;
    private Vector2 pointDirection;
    private Vector2 parentInitScale;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start() {
        parentObject = transform.parent.gameObject;
        parentInitScale = parentObject.transform.localScale;

        weaponOwner = parentObject.tag;

        if(weaponType.Equals("Pistol"))
            coroutine = Pistol();
        else if(weaponType.Equals("Automatic"))
            coroutine = Automatic();
        else if(weaponType.Equals("Sniper"))
            coroutine = Sniper();
        else if(weaponType.Equals("MissileLauncher"))
            coroutine = MissileLauncher();
        else if(weaponType.Equals("Melee"))
            coroutine = Melee();
        else if(weaponType.Equals("Mouth"))
            coroutine = Mouth();

        StartCoroutine(coroutine);
    }

    private IEnumerator Pistol() {
        if(weaponOwner.Equals("Player")) {
            while(true) {
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

                    if(Input.GetMouseButtonDown(0)) {
                        Vector3 firePoint = transform.GetChild(0).position;
                        pointDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObject.transform.position;
                        pointDirection = pointDirection.normalized;
                        
                        GameObject newBullet = Instantiate(bulletObject, firePoint, transform.rotation);
                        newBullet.GetComponent<BulletController>().shotBy = parentObject.tag;
                        newBullet.GetComponent<BulletController>().damage = damage;
                        newBullet.GetComponent<Rigidbody2D>().velocity = pointDirection * bulletSpeed;
                    }
                }

                yield return null;
            }
        } else if(weaponOwner.Equals("Enemy")) {
            while(true) {

                yield return null;
            }
        }
    }

    private IEnumerator Automatic() {
        if(weaponOwner.Equals("Player")) {
            while(true) {

                yield return null;
            }
        } else if(weaponOwner.Equals("Enemy")) {
            while(true) {

                yield return null;
            }
        }
    }

    private IEnumerator Sniper() {
        if(weaponOwner.Equals("Player")) {
            while(true) {

                yield return null;
            }
        } else if(weaponOwner.Equals("Enemy")) {
            while(true) {

                yield return null;
            }
        }
    }

    private IEnumerator MissileLauncher() {
        if(weaponOwner.Equals("Player")) {
            while(true) {

                yield return null;
            }
        } else if(weaponOwner.Equals("Enemy")) {
            while(true) {

                yield return null;
            }
        }
    }

    private IEnumerator Melee() {
        if(weaponOwner.Equals("Player")) {
            while(true) {

                yield return null;
            }
        } else if(weaponOwner.Equals("Enemy")) {
            while(true) {

                yield return null;
            }
        }
    }

    private IEnumerator Mouth() {
        if(weaponOwner.Equals("Player")) {
            while(true) {

                yield return null;
            }
        } else if(weaponOwner.Equals("Enemy")) {
            while(true) {

                yield return null;
            }
        }
    }
}
