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
    private Collision coll;

    private void Awake()
    {
        curHp = maxHp;
        // PlayerController 컴포넌트 참조
        playerController = GetComponent<PlayerController>();
        coll = GetComponent<Collision>();
    }

    private void Update()
    {
        StampedeUpdate();
        //테스트용
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1f);
        }
    }

    public void Dealdamage(float damage)
    {

    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;
        Debug.Log($"현재 체력 : {curHp}");

        // 플레이어의 체력이 0 이하가 되면 상태를 Die로 변경
        if (curHp <= 0)
        {
            playerController.Die();
        }
    }

    public void StampedeUpdate()
    {
        if(coll.onPlatform && coll.onCeiling)
        {
            playerController.Die();
        }
    }
}
