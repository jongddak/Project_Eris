using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAura : MonoBehaviour
{
    // �÷��̾� ������
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainPre;
    // �˱��� ���ǵ�
    [SerializeField] float swordAuraSpeed;

    [SerializeField] float swordAuraDamage;
    private bool spendDamage = false;

    // �߻� ����
    public int direction;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        spendDamage = false;
        Destroy(gameObject, 3f);
    }   

    private void Update()
    {
        mainPre.transform.Translate(transform.forward * direction * swordAuraSpeed * Time.deltaTime);      
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
                playerRPG.TakeDamage(swordAuraDamage);
                Debug.Log($"�÷��̾�� {swordAuraDamage} �������� �������ϴ�.");
                
                // �ѹ��� �������� �ֱ� ���� spendDamage�� ������ ����
                spendDamage = true;
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            // �Ҹ� �ִϸ��̼� ���� ����
            Destroy(mainPre);
        }
    }
}
