using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] CapsuleCollider2D capsuleCollider;
    [SerializeField] GameObject main;
    [SerializeField] GameObject exEffect;
    [SerializeField] float damage;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 30f);
    }


    private void Update()
    {
        HomingMissile();
    }


    private void HomingMissile()  // �ƹ� ������Ʈ�� ���̰� �÷��̾� ���� ������Ʈ�� �ҷ����� ����ź�� �� 
    {
        Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z; // ���ؾȰ� ; �ƹ�ư ������ ���� ���ؼ� �� �������� ȸ�� 
        transform.Rotate(0, 0, -rotateAmount * 100f * Time.deltaTime); // ������ ���� �÷��̾� �������� ������ Ʋ�� (y���� �÷��̾�� ���ϰ�) �̵� 
                                                                       // ���� ������ ���߷��� �̵��ӵ�, �� ȸ���ӵ� , �Լ��� ȣ��Ƚ��?(������ ���ٰ� �ƴ� �ڷ�ƾ���� ���ʸ��� �̷������� ����)�� �����ؾ���
        transform.Translate(Vector2.up * 10f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            capsuleCollider.enabled = false;
            main.SetActive(false);
            exEffect.SetActive(true);
            audioSource.clip = audioClips[0];
            audioSource.Play();
            Destroy(gameObject, 0.4f);

            PlayerRPG player = collision.GetComponent<PlayerRPG>();
            player.TakeDamage(damage);
        }
        else if (collision.tag == "Test") 
        {
            capsuleCollider.enabled = false;
            main.SetActive(false);
            exEffect.SetActive(true);
            audioSource.clip = audioClips[0];
            audioSource.Play();
            Destroy(gameObject, 0.4f);
        }
    }
}
