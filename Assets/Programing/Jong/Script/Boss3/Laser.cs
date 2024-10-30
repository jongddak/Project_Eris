using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    void Update()
    {
        transform.Translate(Vector2.up * 10f * Time.deltaTime);
    }
}
