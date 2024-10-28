using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRPG : MonoBehaviour
{
    [Header("PlayerStat")]
    [SerializeField] public float attackDamage;
    [SerializeField] public float curHp;
    [SerializeField] public float maxHp;

    private PlayerController playerController;

    private void Awake()
    {
        curHp = maxHp;
        // PlayerController 컴포넌트 참조
        playerController = GetComponent<PlayerController>();
    }

    public void Dealdamage(float damage)
    {

    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;

        // 플레이어의 체력이 0 이하가 되면 상태를 Die로 변경
        if (curHp <= 0)
        {
            playerController.Die();
        }
    }
}
