using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] float destorytime;
    [SerializeField] float effectDamage;
    private bool spendDamage = false;
    private void Start()
    {
        Destroy(gameObject, destorytime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !spendDamage)
        {
            // 플레이어에게 데미지를 주는 로직
            PlayerRPG playerRPG = collision.GetComponent<PlayerRPG>();
            if (playerRPG != null)
            {
                playerRPG.TakeDamage(effectDamage);
                Debug.Log($"플레이어에게 {effectDamage} 데미지를 입혔습니다.");
            }
            spendDamage = true;
        }
    }
}
