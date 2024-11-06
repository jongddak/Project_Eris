using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D ������Ʈ
    private Animator bulletAnimator;

    [SerializeField] float destroyDelay = 2f; // �ı��� �ð�

    private void Awake()
    {
        // Rigidbody2D ������Ʈ ��������
        rb = GetComponent<Rigidbody2D>();

        // Animator ������Ʈ ��������
        bulletAnimator = GetComponent<Animator>();

        // �߷� ������ ���� �ʵ��� ����
        rb.gravityScale = 0;
    }
    private void Start()
    {
        // �ı� ���� �ڷ�ƾ ����
        StartCoroutine(DestroyAfterDelay(destroyDelay));
    }

    public void SetSpeed(Vector2 speed)
    {
        if (rb != null)
        {
            rb.velocity = speed; // Rigidbody2D�� velocity�� �����Ͽ� ������ �̵�
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return; // Player �±��� ��� ����
        }

        /*// �ǰ� ����Ʈ ����
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }*/

        Destroy(gameObject); // ������Ʈ �ı�
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ���� �ð� ���

        // ������Ʈ�� ���� �ı����� �ʾ��� ��쿡�� �ı�
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
