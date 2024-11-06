using System.Collections;
using UnityEngine;

public class DisapperPlatform : MonoBehaviour
{
    // playerCheck bool������ �־ �÷����� ����ִ� ��� true�� ��ȯ
    // - �̰ɷ� �÷��̾ true�϶� �Ʒ��� �������� �Լ��� ����� �� �ֵ��� ����


    // �ڷ�ƾ���� �ð� Ÿ�̹��� ����
    // �ð� ���� �� ������Ʈ ����
    [Header("State")]
    [SerializeField] float DeleteTime; // �������� �ɸ��� �ð� ����
    [SerializeField] SpriteRenderer spriteRenderer; // ������ �̹���
    [SerializeField] Collision playerCollision; // �÷��̾� �����տ� �ִ� Collision�� �浹 üũ�� �ҷ����� ���� ����

    /// <summary>
    /// HealFlat�� �浹ü�� �浹�� ��
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /* tag�� Player�� ���
             * OnCollisionEnter�� �߻��ϱ� ���� collision�� relativeVelocity(�浹������ �ӵ�)�� �����ͼ�
             * relativeVelocity.y <0�� ��쿡�� �����ϴ� �ڷ�ƾ�� ����ϵ��� ��
             * 
             * ������ ����� ������ �ڷ�ƾ�� �۵��ؾ��ϹǷ�
             * �÷��̾��� �ӵ��� ������ ��� = ������ �Ʒ��� �������� ��쿡
             * �÷��̾��� ���� �÷����� ���� OnPlatform = true ������ �� �ڷ�ƾ �۵�
             */
            if (collision.relativeVelocity.y < 0)
            {
                if (playerCollision.onPlatform) // �÷��̾� ��Ʈ�ѷ����� onPlatform ������ �����ͼ� ���
                {
                    // collision�� �浹 ������ �ӵ��� 0���� �۴ٴ� ����
                    // �浹ü�� ������ �Ʒ��� �������� �ִٴ� ���̹Ƿ�
                    // �浹ü�� ������ ����� ������ �����ð� �� �����ϴ� �ڷ�ƾ �۵�
                    StartCoroutine(FlatDelete());
                }
            }
        }
    }

    /// <summary>
    /// HealFlat�� �������� �����ϴ� �ڷ���
    /// </summary>
    /// <returns></returns>
    IEnumerator FlatDelete()
    {
        float timeTerm = 0.3f; // �����̴� ������ �����ϴ� Ÿ�̸� - ���ϴ� �ð����� ���� ����
        while (DeleteTime > 0) // ���������� �ð��� 0���� ū ���ȿ� ������ �ݺ�
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); // �̹����� ���� 50% ����
            yield return new WaitForSeconds(timeTerm); // �����ð� ����
            DeleteTime -= timeTerm; // timeTerm�� ������ ��ŭ DeletTime ���� 
            spriteRenderer.color = new Color(1, 1, 1, 1f); // �̹����� ���� 100% ����
            yield return new WaitForSeconds(timeTerm); // �����ð� ����
            DeleteTime -= timeTerm; // timeTerm�� ������ ��ŭ DeletTime ���� 
        }
        // DeleteTime �� 0�̵Ǹ� ������Ʈ�� ������
        Destroy(gameObject); // �������� �ð��� ������ HealFlat ������Ʈ ����
    }

    /// <summary>
    /// ������Ʈ�� �ݶ��̴��� �Ͻ���(0.5f)���� ��Ȱ��ȭ���·� �ϰ� �ǵ����� �Լ�
    /// </summary>
    public void ChangeColliderState()
    {
        StartCoroutine(DisColliderTime());
        gameObject.GetComponent<Collider>().enabled = false;
        StopCoroutine(DisColliderTime());
    }

    IEnumerator DisColliderTime()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(0.5f);
    }
}
