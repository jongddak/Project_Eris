using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // �ν����� â���� ���� �����ϵ��� ����
public class Dialogue
{
    [Tooltip("�̺�Ʈ �̸�")] // �ν����� â���� Ȯ�� �����ϵ��� ����
    public string eventName; // �̺�Ʈ�� �̸��� ���� - �и��� ������
    [Tooltip("��� ġ�� ĳ���� ���̵�")]
    public string unitId;
    [Tooltip("��� ġ�� ĳ���� �̸�")] 
    public string name;
    [Tooltip("��� ����")]
    public string[] contexts; // ���� string[]�� ����
}
/* �����ڷῡ���� ����Ͽ����� ������� �ʰ� �� �ڵ�
[System.Serializable] // �ν����� â���� ���� �����ϵ��� ����
/// <summary>
/// ���� ĳ���Ͱ� ��ȭ�� �����ؾ��ϹǷ� Dialgue Class�� �迭�� �����ؾ� ��
/// </summary>
public class DialogueEvent
{
    public string eventName; // �̺�Ʈ�� �̸��� ���� - �и��� ������
    public Vector2 line; // x ~ y��° ���� ��縦 ���� �ؿ����� ��� ex. csv������ 2(x��)��°���� 5(y��)������ ��� ����
    public Dialogue[] dialogues; // Dialogue Ŭ������ �迭�� �ٷ��
                                 // System.Serializable ����Ǿ������Ƿ� �ν�����â���� ��������
}*/

