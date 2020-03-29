using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject parentObject;
    Vector2 pointDirection;
    Vector2 parentInitScale;
    // Start is called before the first frame update
    void Start()
    {
        parentObject = transform.parent.gameObject;
        parentInitScale = parentObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mousePresent) { //CHANGE LATER
            pointDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            pointDirection.Normalize();
            float angle = Mathf.Atan2(pointDirection.y, pointDirection.x) * Mathf.Rad2Deg;
            
            if(pointDirection.x >= 0) {
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
                parentObject.transform.localScale = parentInitScale;
            } else {
                transform.rotation = Quaternion.Euler(0f, 0f, angle + 180);
                parentObject.transform.localScale = new Vector2(-parentInitScale.x, parentInitScale.y);
            }
        }
    }
}
