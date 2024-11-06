using System.Collections;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    // ������ Ȯ�� �ʿ�
    [SerializeField] float poisionDamage; // �� �������� ũ��
    [SerializeField] float poisionDebuffTime; // �� ������ ����� �ֱ�
    // �浹�� �÷��̾��� �÷��̾� ��Ʈ�ѷ��� �����������ؼ� �ۼ�
    // �̸� ����Ƽ���� �־� ���� �͵� ��ĥ�� �����غ��� ���� ���� �� ����
    // �� ��� OnCollsionEnter2D�� samplePlayer = collision.gameObject.GetComponent<SamplePlayer>(); ������ ��
    [SerializeField] PlayerRPG playerRpg; // �÷��̾��� ��ũ��Ʈ�� ���� �ٸ��� ������ ��
    [SerializeField] Collision playerCollision; // �÷��̾� �����տ� �ִ� Collision�� �浹 üũ�� �ҷ����� ���� ����

    bool isDebuff = false; // ������� Ȱ��ȭ ���� ����

    private void Update()
    {
        if (playerCollision.onWall)
        {
            isDebuff = true;
            StartCoroutine(PoisonDebuff());
        }
        else if (playerCollision.onWall == false)
        {
            isDebuff = false;
            StopCoroutine(PoisonDebuff());
        }
    }

    /// <summary>
    /// �� �������� ���� �ֱ⿡ ���缭 �� ������� �����ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator PoisonDebuff()
    {
        while (isDebuff)
        {
            playerRpg.TakeDamage(poisionDamage); // PlayerRPG�� TakeDamage�� �÷��̾� ü�� ����
            // �÷��̾��� ü�� ����
            yield return new WaitForSeconds(poisionDebuffTime); // ���� �ð� ����
            //yield return new WaitForSeconds(poisionDebuffTime*Time.deltaTime); // ���� �ð� ����
        }
    }
}
