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
        if (!spendDamage)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerRPG playerRPG = collision.GetComponent<PlayerRPG>();
                if (playerRPG == null)
                {
                    Debug.Log("�ȵ���");
                    return;
                }
                // ������ �� �޾Ҵٸ�

                // �÷��̾�� �������� �ִ� ����
                playerRPG.TakeDamage(effectDamage);
                Debug.Log($"�÷��̾�� {effectDamage} �������� �������ϴ�.");

                // �ѹ��� �������� �ֱ� ���� spendDamage�� ������ ����
                spendDamage = true;
            }
        }
    }
}
