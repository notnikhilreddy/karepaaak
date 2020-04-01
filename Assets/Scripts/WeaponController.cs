using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public enum WeaponTypes {
        Gun,
        Melee,
        Mouth,
        Plasma
    }

    public WeaponTypes weaponType;
    public float bulletSpeed, fireDelay, bulletsPerFire;
    public bool simultaneously;
    public float bulletSpreadingAngle, timeBetweenBullets, damagePerFire;
    public float visualRange, visualAngle;
    public GameObject bulletObject;
    public LineRenderer aimingLaser;
    
    private GameObject parentObject;
    private string weaponOwner;
    private Vector2 pointDirection, firePoint;
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
        else if(weapon.Equals("Plasma"))
            coroutine = Plasma();

        StartCoroutine(coroutine);
    }

    private IEnumerator Gun() {
        if(weaponOwner.Equals("Player")) {
            float lastFireTime = 0f;
            while(true) {
                if(Input.mousePresent) { //CHANGE LATER
                    pointDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObject.transform.position;
                    pointDirection = pointDirection.normalized;

                    RaycastHit2D hitInfo = Physics2D.Raycast(firePoint, pointDirection, visualRange);
                    // aimingLaser.SetPosition(0, firePoint);
                    // aimingLaser.SetPosition(1, firePoint + pointDirection * visualRange);

                    float angle = Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg;

                    if(pointDirection.x >= 0f) {
                        transform.rotation = Quaternion.Euler(0f, 0f, angle);
                        parentTransform.localScale = new Vector2(Mathf.Abs(parentTransform.localScale.x), parentTransform.localScale.y);
                    } else {
                        transform.rotation = Quaternion.Euler(0f, 0f, angle + 180);
                        parentTransform.localScale = new Vector2(-Mathf.Abs(parentTransform.localScale.x), parentTransform.localScale.y);
                    }

                    if(Input.GetMouseButtonDown(0)) {
                        firePoint = transform.GetChild(0).position;

                        pointDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - parentObject.transform.position;
                        pointDirection = pointDirection.normalized;
                        
                        if(Time.time - lastFireTime >= fireDelay) {
                            
                            GameObject newBullet = Instantiate(bulletObject, firePoint, transform.rotation);
                            newBullet.GetComponent<BulletController>().shotBy = parentObject.tag;
                            newBullet.GetComponent<BulletController>().damage = damagePerFire / bulletsPerFire;
                            newBullet.GetComponent<Rigidbody2D>().velocity = pointDirection * bulletSpeed;

                            float lastBulletTime = Time.time;
                            for(int i = 0; i < bulletsPerFire-1; i++) {
                                if(!simultaneously) {
                                    while(Time.time - lastBulletTime < timeBetweenBullets) yield return null;
                                }
                                float shootAngle = Random.Range(bulletSpreadingAngle/2f, -bulletSpreadingAngle/2f);
                                pointDirection = Quaternion.Euler(0, 0, shootAngle) * pointDirection;

                                newBullet = Instantiate(bulletObject, firePoint, transform.rotation);
                                newBullet.GetComponent<BulletController>().shotBy = parentObject.tag;
                                newBullet.GetComponent<BulletController>().damage = damagePerFire / bulletsPerFire;
                                newBullet.GetComponent<Rigidbody2D>().velocity = pointDirection * bulletSpeed;

                                lastBulletTime = Time.time;
                            }

                            lastFireTime = Time.time;
                        }
                    }
                }

                yield return null;
            }
        } else if(weaponOwner.Equals("Enemy")) {
            Vector2 raycastDirection;
            RaycastHit2D hitInfo;
            float lastFireTime = Time.time;
            
            while(true) {
                bool playerSpotted = false;
                for(float angle = visualAngle/2f; angle >= -visualAngle/2f; angle -= visualAngle/(visualRange*2)) {
                    raycastDirection = Quaternion.Euler(0f, 0f, angle) * parentTransform.right * parentTransform.localScale.x;
                    raycastDirection = raycastDirection.normalized;
                    
                    hitInfo = Physics2D.Raycast(transform.position, raycastDirection, visualRange, ~1 << LayerMask.NameToLayer("Enemies"));
                    
                    if(hitInfo && hitInfo.collider.tag.Equals("Player")) {
                        playerSpotted = true;
                        parentObject.GetComponent<EnemyController>().isAttacking = true;

                        Vector3 hitPoint = hitInfo.collider.transform.position;

                        pointDirection = hitPoint - parentObject.transform.position;
                        pointDirection = pointDirection.normalized;
                        
                        float gunAngle = Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg;
                        if(pointDirection.x >= 0f) {
                            transform.rotation = Quaternion.Euler(0f, 0f, gunAngle);
                        } else {
                            transform.rotation = Quaternion.Euler(0f, 0f, gunAngle + 180);
                        }

                        if(Time.time - lastFireTime >= fireDelay) {
                            
                            firePoint = transform.GetChild(0).position;
                            GameObject newBullet = Instantiate(bulletObject, firePoint, transform.rotation);
                            newBullet.GetComponent<BulletController>().shotBy = parentObject.tag;
                            newBullet.GetComponent<BulletController>().damage = damagePerFire / bulletsPerFire;
                            newBullet.GetComponent<Rigidbody2D>().velocity = pointDirection * bulletSpeed;

                            float lastBulletTime = Time.time;
                            for(int i = 0; i < bulletsPerFire-1; i++) {
                                if(!simultaneously) {
                                    while(Time.time - lastBulletTime < timeBetweenBullets) yield return null;
                                }
                                float shootAngle = Random.Range(bulletSpreadingAngle/2f, -bulletSpreadingAngle/2f);
                                pointDirection = Quaternion.Euler(0, 0, shootAngle) * pointDirection;

                                newBullet = Instantiate(bulletObject, firePoint, transform.rotation);
                                newBullet.GetComponent<BulletController>().shotBy = parentObject.tag;
                                newBullet.GetComponent<BulletController>().damage = damagePerFire / bulletsPerFire;
                                newBullet.GetComponent<Rigidbody2D>().velocity = pointDirection * bulletSpeed;

                                lastBulletTime = Time.time;
                            }

                            lastFireTime = Time.time;
                        }
                    }
                }
                if(!playerSpotted) {
                    parentObject.GetComponent<EnemyController>().isAttacking = false;
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

                yield return null;
            }
        }
    }

    private IEnumerator Melee() {
        if(weaponOwner.Equals("Player")) {
            float lastFireTime = 0f;
            while(true) {
                if(Input.GetMouseButtonDown(0)) {
                    
                    if(Time.time - lastFireTime >= fireDelay) {

                        firePoint = transform.GetChild(0).position;
                        GameObject newBullet = Instantiate(bulletObject, firePoint, transform.rotation, transform);
                        newBullet.GetComponent<BulletController>().shotBy = parentObject.tag;
                        newBullet.GetComponent<BulletController>().damage = damagePerFire;

                        lastFireTime = Time.time;

                        float time = Time.time;
                        while(Time.time - time <= 0.5f) {
                            yield return null;
                        }

                        Destroy(newBullet);
                    }
                }

                yield return null;
            }
        } else if(weaponOwner.Equals("Enemy")) {
            Vector2 raycastDirection;
            // RaycastHit2D hitInfo, hit;
            float lastFireTime = Time.time;
            
            while(true) {
                bool playerSpotted = false;

                RaycastHit2D hitInfo, hit;

                raycastDirection = parentTransform.right * parentTransform.localScale.x;
                raycastDirection = raycastDirection.normalized;
                
                hitInfo = Physics2D.Raycast(transform.position, raycastDirection, 0, ~1 << LayerMask.NameToLayer("Enemies"));
                for(float angle = visualAngle/2f; angle >= -visualAngle/2f; angle -= visualAngle/(visualRange*2)) {
                    hit = Physics2D.Raycast(transform.position, Quaternion.Euler(0f, 0f, angle) * raycastDirection, visualRange, ~1 << LayerMask.NameToLayer("Enemies"));
                    
                    if(hit && hit.collider.tag.Equals("Player")) {
                        hitInfo = hit;
                    }
                }
                
                if(hitInfo && hitInfo.collider.tag.Equals("Player")) {
                    playerSpotted = true;

                    Vector3 hitPoint = hitInfo.collider.transform.position;

                    pointDirection = hitPoint - parentObject.transform.position;
                    pointDirection = pointDirection.normalized;

                    Vector2 runVelocity;

                    parentObject.GetComponent<EnemyController>().isAttacking = true;

                    if(pointDirection.x >= 0) {
                        runVelocity = new Vector2(parentObject.GetComponent<EnemyController>().speed, parentObject.GetComponent<Rigidbody2D>().velocity.y);
                    } else {
                        runVelocity = new Vector2(-parentObject.GetComponent<EnemyController>().speed, parentObject.GetComponent<Rigidbody2D>().velocity.y);
                    }
                    
                    parentObject.GetComponent<Rigidbody2D>().velocity = runVelocity;
                    
                    if(hitInfo.distance <= 1.5f) {
                        parentObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        if(Time.time - lastFireTime >= fireDelay) {
                            
                            firePoint = transform.GetChild(0).position;
                            GameObject newBullet = Instantiate(bulletObject, firePoint, transform.rotation, transform);
                            newBullet.GetComponent<BulletController>().shotBy = parentObject.tag;
                            newBullet.GetComponent<BulletController>().damage = damagePerFire;

                            lastFireTime = Time.time;

                            float time = Time.time;
                            while(Time.time - time <= 0.5f) {
                                yield return null;
                            }

                            Destroy(newBullet);
                        }
                    }
                }

                if(!playerSpotted) {
                    parentObject.GetComponent<EnemyController>().isAttacking = false;
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

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

    private IEnumerator Plasma() {
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
