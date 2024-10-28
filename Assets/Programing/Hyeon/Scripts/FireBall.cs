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
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        // 파이어볼과 플레이어 사이의 방향 계산
        direction = new Vector2((player.transform.position.x - transform.position.x), 0).normalized;
    }

    private void Update()
    {
        transform.Translate(direction * fireBallSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!spendDamage)
            {
                // 플레이어에게 데미지를 주는 로직 

                // 한번만 데미지를 주기위해 spendDamage로 데미지 판정
            }
            spendDamage = true;            
        }
        // 벽에 부딛치면 소멸시키기
        if (collision.gameObject.tag == "Ground")
        {
            // 소멸 애니메이션 있다면 적용

            // 애니메이션을 적용한다면 부딛친 위치에 정지하는코드
            // 삭제를 애니메이션 재생시간동안 유해
            Destroy(gameObject);
        }
    }
}
