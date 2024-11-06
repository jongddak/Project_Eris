using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallScript : MonoBehaviour
{
    // �÷��̾� ������ ����
    bool spendDamage = false;
    // �÷��̾� ������
    [SerializeField] GameObject player;
    [SerializeField] float fireWallDamage;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        // �ִϸ��̼� ���� ������
        // �ұ���� 2�ʵ� ����
        Destroy(gameObject, 2f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!spendDamage)
        {
            if (collision.CompareTag("Player") && !spendDamage)
            {
                PlayerRPG playerRPG = collision.GetComponent<PlayerRPG>();
                if (playerRPG == null)
                {
                    Debug.Log("�ȵ���");
                    return;
                }
                // ������ �� �޾Ҵٸ�
                if (!spendDamage)
                {
                    // �÷��̾�� �������� �ִ� ����
                    playerRPG.TakeDamage(fireWallDamage);
                    Debug.Log($"�÷��̾�� {fireWallDamage} �������� �������ϴ�.");
                }
                // �ѹ��� �������� �ֱ� ���� spendDamage�� ������ ����
                spendDamage = true;
            }
        }

    }
}
