using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    [SerializeField] GameObject player;


    private void Update()
    {
        HomingMissile();
    }


    private void HomingMissile() 
    {
        Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        transform.Rotate(0, 0, -rotateAmount * 400f * Time.deltaTime);


        transform.Translate(Vector2.up * 10f * Time.deltaTime);
    }

    private void Flying() {}

}
