using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트

    private void Awake()
    {
        // Rigidbody2D 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();

        // 중력 영향을 받지 않도록 설정
        rb.gravityScale = 0;
    }

    public void SetSpeed(Vector2 speed)
    {
        if (rb != null)
        {
            rb.velocity = speed; // Rigidbody2D의 velocity를 설정하여 앞으로 이동
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject); // 트리거로 감지 시 파괴
    }
}
