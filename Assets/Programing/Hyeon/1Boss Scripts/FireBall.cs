using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // �÷��̾� ������ ����
    bool spendDamage = false;
    // �÷��̾� ������
    [SerializeField] GameObject player;
    // ���̾�� ���ǵ�
    [SerializeField] float fireBallSpeed;
    // �߻� ����
    private Vector2 direction;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Collider2D fireBallCollider;
    [SerializeField] float fireBallDamage;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fireBallCollider = GetComponent<Collider2D>();

        // ���̾�� �÷��̾� ������ ���� ���
        direction = new Vector2((player.transform.position.x - transform.position.x), 0).normalized;

        // SpriteRenderer�� Collider�� ��Ȱ��ȭ
        spriteRenderer.enabled = false;
        fireBallCollider.enabled = false;
       
        
        /* ���߿� �÷��̾� ���� ����ؼ� ������ų�� �� ����
        if (direction.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1; // ������ ���� ��� x �������� ����
            transform.localScale = scale;
        }*/

        // 1.3�� �Ŀ� ���̾�� Ȱ��ȭ�ϰ� �̵� ����
        StartCoroutine(ActivateAfterDelay(1.3f));

        // 4�� �� �ڵ� �Ҹ�
        Destroy(gameObject, 4f);
    }

    private IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // SpriteRenderer�� Collider�� Ȱ��ȭ
        spriteRenderer.enabled = true;
        fireBallCollider.enabled = true;

        // �̵� ����
        rb.velocity = direction * fireBallSpeed;
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
                    playerRPG.TakeDamage(fireBallDamage);
                    Debug.Log($"�÷��̾�� {fireBallDamage} �������� �������ϴ�.");
                }
                // �ѹ��� �������� �ֱ� ���� spendDamage�� ������ ����
                spendDamage = true;
            }
        }
    
        if (collision.gameObject.CompareTag("Ground"))
        {
            // �Ҹ� �ִϸ��̼� ���� ���� (�ʿ� ��)
            Destroy(gameObject); // �ִϸ��̼� �� �Ҹ�
        }
    }

}
