using UnityEngine;

public class MoveLenPlatform : MonoBehaviour
{
    // PatternController.cs���� �����ϰ� ��������
    PatternController patternController;
    public float moveSpeed;
   
    // patternController.cs���� �����ϰ� MoveLenPlatform.cs�� ���� LenPlatformLoop.cs���� ���
    public Transform pCiling; // õ�� - ����̹������� �� �а� ������ ��
    public Transform pGround; // �ٴ� - ����̹������� �� �а� ������ ��
    // ���⺰�� �̵�
    public bool isUpMove;

    private void Awake()
    {
        patternController = transform.parent.GetComponent<PatternController>();
        pCiling = patternController.pCiling;
        pGround = patternController.pGround;
    }
    private void Start()
    {
        moveSpeed = patternController.moveSpeed;
        int num = Random.Range(0, 2);
        switch (num)
        {
            case 0:
                isUpMove = false;
                Debug.Log("�ٿ�");
                break;
            case 1:
                isUpMove = true;
                Debug.Log("��");
                break;
        }

    }

    private void Update()
    {
        if (isUpMove)
        {
            MoveUp();
        }
        else if (!isUpMove)
        {
            MoveDown();
        }
    }

    /// <summary>
    /// ������ ������ �ӵ��� ���� ��� �̵�
    /// </summary>
    public void MoveUp()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    public void MoveDown()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

}
