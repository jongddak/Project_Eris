using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AttackTest : MonoBehaviour
{
    public Collider2D attackRangeCollider;
    public PlayerRPG playerRPG;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            // �浹�� ������ Ÿ���� ������ PlayerRPG�� �������� ����
            string bossType = other.gameObject.name;
            playerRPG.DealDamageToBoss(bossType, playerRPG.attackDamage);
        }
    }
}
