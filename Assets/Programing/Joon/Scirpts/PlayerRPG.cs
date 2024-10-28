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

    private void Start()
    {
        StartHealthDecrease();
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

    public IEnumerator DecreaseHealthOverTime()
    {
        while (curHp > 0)
        {
            yield return new WaitForSeconds(1f); // 1초 대기
            TakeDamage(1); // 매초 1의 피해를 줍니다.
        }
    }

    // 이 메서드를 호출하여 코루틴을 시작할 수 있습니다.
    public void StartHealthDecrease()
    {
        StartCoroutine(DecreaseHealthOverTime());
    }
}
