using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;

    [SerializeField] string csvFileName; // CSV ������ �̸��� ����
    [SerializeField] public Dialogue[] dialogues; // Dialogue�� dialogues�迭�� ���� DialogueParser�� �����͸� �迭�� ����

    // Dictionary�� <string, Dialogue> �� ����
    // dialogueDic�� �����͸� �����Ͽ� ���
    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false; // ������ �Ľ� �� ����� ������ �Ǿ����� ���θ� Ȯ���� �� �ִ� ����

    private void Awake()
    {
        if (instance == null) // instance�� null ���̸�
        {
            instance = this; // ���� �ν��Ͻ��� ����
            // DatabaseManager�� DialogueParser.cs�� ���� ������Ʈ�� �־� �ѹ��� theParser�� ã�� �� �ֵ��� ����
            DialogueParser theParser = GetComponent<DialogueParser>();

            dialogues = theParser.Parser(csvFileName); // DialogueParser�� Parser�Լ��� ����
            // dialogues�� csv������ �����Ͱ� ���� ���� ��
            
            isFinish = true; // �������� ������ �Ϸ��
        }

    }
}
