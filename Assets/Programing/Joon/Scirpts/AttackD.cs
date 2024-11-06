using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackD : MonoBehaviour
{
    public PlayerRPG playerRPG;

    private void Awake()
    {
        // PlayerController ������Ʈ ����
        playerRPG = FindObjectOfType<PlayerRPG>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss") && playerRPG != null)
        {
            string bossType = other.gameObject.name;
            playerRPG.DealDamageToBoss(bossType, playerRPG.dronDamage);
        }
    }
}
