using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] PlayerController playerController; // �÷��̾��� ��ũ��Ʈ�� ���� �ٸ��� ������ ��
    [SerializeField] PlayerRPG playerRPG;
    /// <summary>
    /// DeadZone�� trigger�� ����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /* tag�� DeadZone�� ��� 
             * �÷��̾��� ���
             * �÷��̾ ������ �ִ� ����Լ��� ������������
             * collision.�÷��̾�cs�� �÷��̾��� ������Ʈ�� �����ϰ� ����Լ� ��������
             */
            playerRPG.TakeDamage(playerRPG.maxHp);
            //playerController.Die();
        }
    }
}
