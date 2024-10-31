using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // 플레이어 데미지 판정
    bool spendDamage = false;
    // 플레이어 프리펩
    [SerializeField] GameObject player;
    // 파이어볼의 스피드
    [SerializeField] float fireBallSpeed;
    // 발사 방향
    private Vector2 direction;
    private Rigidbody2D rb;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        // 파이어볼과 플레이어 사이의 방향 계산
        direction = new Vector2((player.transform.position.x - transform.position.x), 0).normalized;
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        rb.velocity = direction * fireBallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !spendDamage)
        {
            // 데미지 안받았다면
            if (!spendDamage)
            {                
                // 플레이어에게 데미지를 주는 로직
            }
            // 한번만 데미지를 주기위해 spendDamage로 데미지 판정
            spendDamage = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 소멸 애니메이션 적용 가능 (필요 시)
            Destroy(gameObject); // 애니메이션 후 소멸
        }
    }
}
