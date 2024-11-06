using UnityEngine;

/// <summary>
/// ������ �����ӿ� ���� ������ õ��� �ٴ�(����� �̹������� �� ���� �����Ǿ��־����)�� ��ġ��
/// y�� ������ �Ѿ�� ��� ������ ��ġ�� �������ϴ� ��ũ��Ʈ
/// </summary>
public class LenPlatformLoop : MonoBehaviour
{
    // MoveLenPlatform���� ���� ������ ���, �ϰ����θ� �Ǻ��ϱ� ���� �ʿ�
    [SerializeField] MoveLenPlatform moveLenPlatform;
    // �ڽĿ�����Ʈ�� ������ ������ ����(num)�� �����ߴ� ����(space)���� ������Ʈ�� ��ġ�� �����ϱ� ���� �ʿ�
    [SerializeField] CreatePlatform createPlatform;

    Transform pCiling; // õ��
    Transform dGround; // �ٴ�
    Transform makingPos; // ���� ���ġ�� ���� ���� ��ġ ����
    int num; // ������ ����
    float space; // ������ ���� 

    private void Awake()
    {

    }
    private void Start()
    {
        // PatternController.cs���� ������ ��ġ�� �ҷ�����
        pCiling = moveLenPlatform.pCiling;
        dGround = moveLenPlatform.pGround;
        // CreatePlatform���� ������ ��ġ�� �ҷ�����
        makingPos = createPlatform.SetPos;
        num = createPlatform.num - 1; // ������ ������ num �̹Ƿ� �������� ���� num - 1
        space = createPlatform.space;
    }
    private void Update()
    {
        // �����
        if (moveLenPlatform.isUpMove)
        {
            // õ���� y������ ������Ʈ�� y ���� ��������
            if (gameObject.transform.position.y > pCiling.transform.position.y)
            {
                // ���ӿ�����Ʈ�� ��ġ ����
                gameObject.transform.position = new Vector2(gameObject.transform.position.x,
                                                            -(makingPos.position.y + space * num));
            }
        }
        // �ϰ���
        if (!moveLenPlatform.isUpMove)
        {
            // �ٴ��� y������ ������Ʈ�� y���� ��������
            if (gameObject.transform.position.y < dGround.transform.position.y)
            {
                // ���� ������Ʈ�� ��ġ ����
                gameObject.transform.position = new Vector2(gameObject.transform.position.x,
                                                            makingPos.position.y + space * num);
            }
        }
    }
}

