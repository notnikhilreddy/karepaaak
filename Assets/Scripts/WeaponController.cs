using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public enum WeaponTypes {
        Gun,
        Melee,
        Mouth
    }

    public WeaponTypes weaponType;
    public float bulletSpeed, fireRate, bulletsPerFire, damagePerFire;
    public GameObject bulletObject;
   
    private GameObject parentObject;
    private string weaponOwner;
    private Vector2 pointDirection;
    private Transform parentTransform;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    
    void Start() {
        parentObject = transform.parent.gameObject;
        parentTransform = parentObject.transform;

        weaponOwner = parentObject.tag;

        string weapon = weaponType.ToString();

        if(weapon.Equals("Gun"))
            coroutine = Gun();
        else if(weapon.Equals("Melee"))
            coroutine = Melee();
        else if(weapon.Equals("Mouth"))
            coroutine = Mouth();

        StartCoroutine(coroutine);
    }

    private IEnumerator Gun() {
        if(weaponOwner.Equals("Player")) {
            while(true) {
                if(Input.mousePresent) { //CHANGE LATER
                    pointDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObject.transform.position;
                    pointDirection = pointDirection.normalized;

                    float angle = Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg;

                    if(pointDirection.x >= 0f) {
                        transform.rotation = Quaternion.Euler(0f, 0f, angle);
                        parentTransform.localScale = new Vector2(Mathf.Abs(parentTransform.localScale.x), parentTransform.localScale.y);
                    } else {
                        transform.rotation = Quaternion.Euler(0f, 0f, angle + 180);
                        parentTransform.localScale = new Vector2(-Mathf.Abs(parentTransform.localScale.x), parentTransform.localScale.y);
                    }

                    if(Input.GetMouseButtonDown(0)) {
                        Vector3 firePoint = transform.GetChild(0).position;
                        pointDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObject.transform.position;
                        pointDirection = pointDirection.normalized;
                        
                        GameObject newBullet = Instantiate(bulletObject, firePoint, transform.rotation);
                        newBullet.GetComponent<BulletController>().shotBy = parentObject.tag;
                        newBullet.GetComponent<BulletController>().damage = damagePerFire / bulletsPerFire;
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
