using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallScript : MonoBehaviour
{
    // 플레이어 데미지 판정
    bool spendDamage = false;
    // 플레이어 프리펩
    [SerializeField] GameObject player;
    [SerializeField] float fireWallDamage;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        // 애니메이션 실행 있으면
        // 불기둥은 2초뒤 제거
        Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerRPG playerRPG = collision.gameObject.GetComponent<PlayerRPG>();
            if (!spendDamage)
            {
                // 플레이어에게 데미지를 주는 로직 
                playerRPG.TakeDamage(fireWallDamage);
                Debug.Log($"플레이어에게 {fireWallDamage} 데미지를 입혔습니다.");
                // 한번만 데미지를 주기위해 spendDamage로 데미지 판정
            }
            spendDamage = true;
        }

    }
}
