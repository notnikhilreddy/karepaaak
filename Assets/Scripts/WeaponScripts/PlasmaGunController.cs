using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGunController : MonoBehaviour
{
    private GameObject parentObject;
    private string weaponOwner;
    private Vector2 pointDirection, firePoint;
    private Transform parentTransform;

    // Start is called before the first frame update
    void Start()
    {
        parentObject = transform.parent.gameObject;
        parentTransform = parentObject.transform;

        weaponOwner = parentObject.tag;

        IEnumerator coroutine = Plasma();

        StartCoroutine(coroutine);
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
