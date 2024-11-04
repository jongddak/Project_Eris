using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTest : MonoBehaviour
{
    public bool IsBossInRange { get; private set; } = false;
    public Collider2D attackRangeCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss")) // 보스와의 충돌만 감지
        {
            IsBossInRange = true;
            Debug.Log("보스가 공격 범위에 들어왔습니다.");
        }
    }
}
