using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SamplePlayer : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb; // Rigidbody Á¦¾î
    [SerializeField] float jumpPower;
    [SerializeField] float playerHp;

    private void Update()
    {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
    }
    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    public void TakeDamage(float damage)
    {
        playerHp -= damage;
    }
}
