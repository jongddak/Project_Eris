using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoStepUp : MonoBehaviour
{
    [SerializeField] AreaEffector2D effector;

    private void Awake()
    {
        effector = GetComponent<AreaEffector2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            effector.forceAngle = collision.relativeVelocity.x;
        }
    }
}
