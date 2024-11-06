using UnityEngine;


/// <summary>
/// ������ ���� ���� �� �ϳ���
/// �������� �� ���ǵ��� �� �Ǵ� �Ʒ��� �����̵��� ����
/// </summary>
public class PatternController : MonoBehaviour
{
    // ������ ���¿� ����
    // ������ ü���� 25% �̻��� ��� �� ���� ���� �� ������ ���� �����̵��� ����
    // ������ ü���� 25% ������ ��� �� ���� ���� �� ������ ���� �����̵��� ����

    //������ ���� ���¿� ���� UpDate()���� �б�
    // ������ ü���� 25% �̻��� ��� - normal
    // ������ ü���� 25% ������ ��� - overdive
    // ������ ü�»��¿� ���� �����Ƿ� ������ ü�� ���¸� �ҷ����ų�, ������ ü�� ���� ��ũ��Ʈ���� �ۼ��Ͽ� �޾ƿ�����
    // ������ �ʿ�
    public enum BossState { normal, overdirve }
    public BossState nowState; // ������ ���� ����  

    // ������ �����ϴ� ������ ������ ���� ��ũ��Ʈ���� �޾Ƽ� ����ϵ��� ������ �ʿ�
    //public bool isAttackP; // ���� ���� ���� ���� ����

    GameObject[] lines; // �ڽĿ�����Ʈ���� �޾ƿ��� �迭 ����

    // �ν�����â���� �����ϱ� ���� PatternController.cs���� ���ǵ带 �����ϰ�
    // ���� ����� MoveLenPlatform.cs���� ����
    [SerializeField] public float moveSpeed;
    // �ν�����â���� �����ϱ� ���� PatternController.cs���� �÷����� �����ǰ� ����� õ��� �ٴ��� �����ϰ�
    // ���� ����� LenPlatformLoop.cs���� ����
    [SerializeField] public Transform pCiling; // õ�� - ����̹������� �� �а� ������ ��
    [SerializeField] public Transform pGround; // �ٴ� - ����̹������� �� �а� ������ ��

    private void Awake()
    {
        // �迭�� �ڽ� ������Ʈ ����
        lines = new GameObject[gameObject.transform.childCount]; // ��ġ�� ���� ������Ʈ �迭 ����
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = gameObject.transform.GetChild(i).gameObject; // �� ������ �迭�� ����� ���
        }
    }
    /*
    private void Update()
    {
        // test������ �Լ��� �����Ű�� ���� �������� ���� if���� ������ ü�»��¸� �־ ����ǵ��� �����Ͽ� ���
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            isAttackP = true; // ���� ���� ���� ���� ���θ� �޾ƿ��� �̰� �� ���� ������ ����ϱ�
            switch (nowState)
            {
                case BossState.normal:
                    if (isAttackP)
                    {
                        setNormalChoice();
                    }
                    break;
                case BossState.overdirve:
                    if (isAttackP)
                    {
                        setOverdirveChoice();
                    }
                    break;
            }
        }
    }
    */
    /// <summary>
    /// ������ �⺻ ������ �� ������ �����̴� ������ ����
    /// �� ���� ���� ���ڸ� �޾Ƽ� �� ���� ���θ� ������ �̵� ���⿡�� �ݴ�� ����
    /// </summary>
    public void setNormalChoice()
    {
        int num = Random.Range(0, 4);
        Debug.Log($"���Ϻ��� {num}");
        switch (num)
        {
            case 0:
                lines[0].GetComponent<MoveLenPlatform>().isUpMove = !lines[0].GetComponent<MoveLenPlatform>().isUpMove;
                break;
            case 1:
                lines[1].GetComponent<MoveLenPlatform>().isUpMove = !lines[1].GetComponent<MoveLenPlatform>().isUpMove;
                break;
            case 2:
                lines[2].GetComponent<MoveLenPlatform>().isUpMove = !lines[2].GetComponent<MoveLenPlatform>().isUpMove;
                break;
            case 3:
                lines[3].GetComponent<MoveLenPlatform>().isUpMove = !lines[3].GetComponent<MoveLenPlatform>().isUpMove;
                break;
        }
        //isAttackP = false; // ���� ������ ���Ḧ ����
    }

    /// <summary>
    /// ������ ����ȭ ������ �� ������ �����̴� ������ ����
    /// �� ���� ���� ���ڸ� �޾Ƽ� ��ġ�� �ʴ��� Ȯ���� �� �� ���� ������ ������ �̵� ���⿡�� �ݴ�� ����
    /// </summary>
    public void setOverdirveChoice()
    {
        int num1 = Random.Range(0, 4);
        int num2 = Random.Range(0, 4);
        while (num1 == num2) // �� ���� ������
        {
            // �ٸ� �� ���� ���� �� �̱�
            num2 = Random.Range(0, 4);
        }
        // num1 ���ڿ� ���缭 ���� ����
        switch (num1)
        {
            case 0:
                lines[0].GetComponent<MoveLenPlatform>().isUpMove = !lines[0].GetComponent<MoveLenPlatform>().isUpMove;
                break;
            case 1:
                lines[1].GetComponent<MoveLenPlatform>().isUpMove = !lines[1].GetComponent<MoveLenPlatform>().isUpMove;
                break;
            case 2:
                lines[2].GetComponent<MoveLenPlatform>().isUpMove = !lines[2].GetComponent<MoveLenPlatform>().isUpMove;
                break;
            case 3:
                lines[3].GetComponent<MoveLenPlatform>().isUpMove = !lines[3].GetComponent<MoveLenPlatform>().isUpMove;
                break;
        }
        // num2 ���ڿ� ���缭 ���� ����
        switch (num2)
        {
            case 0:
                lines[0].GetComponent<MoveLenPlatform>().isUpMove = !lines[0].GetComponent<MoveLenPlatform>().isUpMove;
                break;
            case 1:
                lines[1].GetComponent<MoveLenPlatform>().isUpMove = !lines[1].GetComponent<MoveLenPlatform>().isUpMove;
                break;
            case 2:
                lines[2].GetComponent<MoveLenPlatform>().isUpMove = !lines[2].GetComponent<MoveLenPlatform>().isUpMove;
                break;
            case 3:
                lines[3].GetComponent<MoveLenPlatform>().isUpMove = !lines[3].GetComponent<MoveLenPlatform>().isUpMove;
                break;
        }
        //isAttackP = false; // ���������� ���Ḧ ����
    }





}
