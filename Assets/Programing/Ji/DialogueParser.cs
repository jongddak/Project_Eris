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
            // [1] : ĳ�����̸�
            // [2] : ���

            // Dialogue.cs������ class Dialogue �������� ���� ����
            Dialogue dialogue = new Dialogue();
            dialogue.eventName = row[0]; // �̺�Ʈ�� ����
            dialogue.name = row[1]; // ��縦 �ϴ� ĳ������ �̸�
            // Dialogue.cs������ class Dialogue�� ��縦 �����ϱ� ���� string�� ����Ʈ�� ����
            List<string> contextList = new List<string>();

            // do - while �� : ���� ������ ������ �����Ų �Ŀ� ������ �Ǻ��ϰ� �̾ �������� ���θ� �Ǵ�
            // csv ������ ���� ĳ������ �̸��� ���� ��簡 �ԷµǾ��ִ� ��찡 �����Ƿ� �װ��� �Ǵ��ϱ� ���ؼ� do - while���� ���
            do
            {
                contextList.Add(row[2]);// ����Ʈ�� row[2]�� �ִ� ��� �� ���� ����
                if (++i < data.Length)// �̸� ������ i�� data�� ���̺��� �������� ���ϰ�
                {
                    row = data[i].Split(new char[] { ',' }); // ++i�� �����ؼ� �����ٷ� �Ѿ��
                }
                else // data�� ���̺��� ū ��쿡�� �ݺ����� ������ �ʿ䰡 ����
                {
                    break;
                }
            } while (row[1].ToString() == "");
            // do�� if������ �����ٷ� �Ѿ ���� ĳ������ �̸��� ���������� Ȯ���ϰ�
            // �����̸� ��� �� ���� ����

            dialogue.contexts = contextList.ToArray(); // �ϼ��� contextList�� �迭�� ��ȯ�Ͽ� contexts�� ����

            dialogueList.Add(dialogue); // �Լ� �� ó���� ������ dialogueList�� dialogue�� ����
        }
        return dialogueList.ToArray(); // ����� dialogueList�� �迭ȭ �Ͽ� ���

    }
    /*
        public DialogueEvent[] EventParser(Dialogue[] dialogueArr)
        {
            // �̺�Ʈ ����Ʈ�� ����
            List<DialogueEvent> eventList = new List<DialogueEvent>();

            // �� ��(����)�� �������� csv������ �߶� string�迭ȭ
            string[] data = csvData.text.Split(new char[] { '\n' });

            // i = 0 �� 0��°�� (����)ǥ������ �з��̹Ƿ� �����ͷμ��� �ʿ����� �����Ƿ� 1���� ����
            for (int i = 1; i < data.Length;)
            {
                string[] row = data[i].Split(new char[] { ',' }); // i��° �迭�� , ������ �ɰ��� �ٴ����� �迭�� ����
                // [0] : �̺�ƮID - Start / Phase1 / MonsterDie
                // [1] : ĳ�����̸�
                // [2] : ���

                // Dialogue.cs������ class DialogueEvent �������� ���� ����
                DialogueEvent dialogueEvent = new DialogueEvent();
                dialogueEvent.eventName = row[0]; // �߻��� �̺�Ʈ�� �̸�
                Debug.Log(row[0]);
                // do - while �� : ���� ������ ������ �����Ų �Ŀ� ������ �Ǻ��ϰ� �̾ �������� ���θ� �Ǵ�
                // csv ������ ���� �̺�Ʈ�� ���� "end"�� �����ϹǷ�
                // �̺�Ʈ�� �̸��� �����ϴ� �������� end���� ���ö������� vector2���� �����ϰ�
                // �˸��� Dialogue[] dialogueArr�� dialogueEvent.dialogues �� ����
                // �װ��� �Ǵ��ϱ� ���ؼ� do - while���� ���
                do
                {
                    int startNum = i - 1;
                    dialogueEvent.dialogues = dialogueArr;
                    int endNum = startNum + 1;
                    dialogueEvent.line = new Vector2(startNum, endNum);
                    if (++i < data.Length)// �̸� ������ i�� data�� ���̺��� �������� ���ϰ�
                    {
                        row = data[i].Split(new char[] { ',' }); // ++i�� �����ؼ� �����ٷ� �Ѿ��
                    }
                    else // data�� ���̺��� ū ��쿡�� �ݺ����� ������ �ʿ䰡 ����
                    {
                        break;
                    }
                } while (row[0].ToString() == "");
                // do�� if������ �����ٷ� �Ѿ ���� �̺�Ʈ�� �̸��� ���������� Ȯ���ϰ�
                // �����̸� ���� Dialogue[] dialogueArr�� �� ���� ����
            }

            return eventList.ToArray();

        }
        private void Start()
        {
            EventParser(Parser("DialogueBoss1Stage"));
        }
    */
    private void Start()
    {
        Parser("DialogueBoss1Stage");
    }
}