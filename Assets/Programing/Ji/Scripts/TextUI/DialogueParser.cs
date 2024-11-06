using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    [SerializeField] private TextAsset csvData; // csv������ �ν����� â���� �����Ͽ� ���

    /// <summary>
    /// DialogueEvent[]�� �� Dialogue[]�� dialogues �迭�� �����ϴ� Parser �Լ�
    /// </summary>
    /// <param name="csvFileName"></param>
    /// <returns></returns>
    public Dialogue[] Parser(string csvFileName)
    {
        // ��ȭ ����Ʈ�� ����
        List<Dialogue> dialogueList = new List<Dialogue>();

        // �� ��(����)�� �������� csv������ �߶� string�迭ȭ
        string[] data = csvData.text.Split(new char[] { '\n' });

        // i = 0 �� 0��°�� (����)ǥ������ �з��̹Ƿ� �����ͷμ��� �ʿ����� �����Ƿ� 1���� ����
        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }); // i��° �迭�� , ������ �ɰ��� �ٴ����� �迭�� ����
            // [0] : �̺�ƮID - Start / Phase1 / MonsterDie
            // [1] : ĳ����ID
            // [2] : ĳ�����̸�
            // [3] : ���

            // Dialogue.cs������ class Dialogue �������� ���� ����
            Dialogue dialogue = new Dialogue();
            dialogue.eventName = row[0]; // �̺�Ʈ�� ����
            dialogue.unitId = row[1]; // ��縦 �ϴ� ĳ������ ID����
            dialogue.name = row[2]; // ���ϸ� �ϴ� ĳ������ �̸� ����
            // Dialogue.cs������ class Dialogue�� ��縦 �����ϱ� ���� string�� ����Ʈ�� ����
            List<string> contextList = new List<string>();

            // do - while �� : ���� ������ ������ �����Ų �Ŀ� ������ �Ǻ��ϰ� �̾ �������� ���θ� �Ǵ�
            // csv ������ ���� ĳ������ �̸��� ���� ��簡 �ԷµǾ��ִ� ��찡 �����Ƿ� �װ��� �Ǵ��ϱ� ���ؼ� do - while���� ���
            do
            {
                contextList.Add(row[3]);// ����Ʈ�� row[2]�� �ִ� ��� �� ���� ����
                if (++i < data.Length)// �̸� ������ i�� data�� ���̺��� �������� ���ϰ�
                {
                    row = data[i].Split(new char[] { ',' }); // ++i�� �����ؼ� �����ٷ� �Ѿ��
                }
                else // data�� ���̺��� ū ��쿡�� �ݺ����� ������ �ʿ䰡 ����
                {
                    break;
                }
            } while (row[1].ToString() == "");
            // do�� if������ �����ٷ� �Ѿ ���� ĳ������ ID�� ���������� Ȯ���ϰ�
            // �����̸� ��� �� ���� ����

            dialogue.contexts = contextList.ToArray(); // �ϼ��� contextList�� �迭�� ��ȯ�Ͽ� contexts�� ����

            dialogueList.Add(dialogue); // �Լ� �� ó���� ������ dialogueList�� dialogue�� ����
        }
        return dialogueList.ToArray(); // ����� dialogueList�� �迭ȭ �Ͽ� ���

    }
}
