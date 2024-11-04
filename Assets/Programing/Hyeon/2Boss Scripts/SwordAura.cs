using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAura : MonoBehaviour
{
    // 플레이어 프리펩
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainPre;
    // 검기의 스피드
    [SerializeField] float swordAuraSpeed;

    [SerializeField] float swordAuraDamage;
    private bool spendDamage = false;

    // 발사 방향
    public int direction;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        Destroy(gameObject, 3f);
    }   

    private void Update()
    {
        mainPre.transform.Translate(transform.forward * direction * swordAuraSpeed * Time.deltaTime);
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !spendDamage)
        {
            // 플레이어에게 데미지를 주는 로직
            PlayerRPG playerRPG = collision.GetComponent<PlayerRPG>();
            if (playerRPG != null)
            {
                playerRPG.TakeDamage(swordAuraDamage);
                Debug.Log($"플레이어에게 {swordAuraDamage} 데미지를 입혔습니다.");
            }
            spendDamage = true;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            // 소멸 애니메이션 적용 가능
            Destroy(mainPre);
        }
    }
}
