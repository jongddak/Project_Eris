using System.Collections;
using TMPro;
using UnityEngine;
/* ��� ��ũ��Ʈ ���� ��Ʃ�� ����
 * https://www.youtube.com/watch?v=DPWvoUlHbjg&list=PLUZ5gNInsv_NG_UKZoua8goQbtseAo8Ow&index=11
 * https://www.youtube.com/watch?v=1fRbGvQlIEQ
 * https://www.youtube.com/watch?v=_04sCWLHoXU
 * https://www.youtube.com/watch?v=qJjfYvEYKiE
 * 
 * ��縦 Ÿ���������� ����ϴ� �� ���� ��Ʃ�� ����
 * https://www.youtube.com/watch?v=OjcPuEVQT6s
 */

/// <summary>
/// �������� ��ȭ �ý����� �ؽ�Ʈ�� �̹����� ����� ����ϴ� �Ŵ���
/// Assets > Programing > Ji > Prefab > TextUI�� DialogueManager�� ������ȭ�Ͽ� ����
/// �� ��ȭ ����� �̹��� ������ �����ϰ� �����͸� �ν����� â���� ��Ī���� ���
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [Header ("Don't Change")]
    // DatabaseManager.cs�� GetDialogues() �Լ��� ����ϱ� ���� ����
    DatabaseManager databaseManager;
    private Dialogue[] nowDialogue;
    [SerializeField] string unitId_Boss; // .csv������ ������ unitId�� �ν�����â���� �Է�
    [SerializeField] string unitId_Player; // .csv������ �÷��̾��� unitId�� �ν�����â���� �Է�

    ChangeController changeController;

    [Header("Status")]
    [SerializeField] float typingSpace; // Ÿ���� ��� ����


    [Header("UI")]
    [SerializeField] private GameObject uiBoss; // ���� ĳ������ ��ȭ �� ��� ������Ʈ
    [SerializeField] private GameObject uiPlayer; // �÷��̾� ĳ������ ��ȭ �� ��� ������Ʈ
    [SerializeField] private GameObject imgDialogue; // ��ȭâ �̹��� ������Ʈ
    [SerializeField] private GameObject uiTextName; // ��ȭ�ϴ� ĳ������ �̸� ��� ������Ʈ
    [SerializeField] private GameObject uiTextDialogue; // ĳ���Ͱ� ��� ��� ������Ʈ

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI textName; // ��ȭ�ϴ� ĳ������ �̸� text
    [SerializeField] private TextMeshProUGUI textContext; // ĳ������ ��� text

    [Header("Image")]
    [SerializeField] private SpriteRenderer imgBoss; // ���� ĳ������ ��ȭ �� ��� �̹���
    [SerializeField] private SpriteRenderer imgPlayer; // �÷��̾� ĳ������ ��ȭ �� ��� �̹���

    private bool isTyping = false; // ����� Ÿ������ ������ ���� �Ǵ��Ͽ� ���� ���� �Ѿ���� �����ϴ� ����

    int count = 0; // nowDialogue[count]�� ���
    int num = 0; // nowDialogue[count].contexts[num] ���� ���

    private void Awake()
    {
        changeController = transform.GetChild(0).GetComponent<ChangeController>();
        databaseManager = transform.GetChild(3).GetComponent<DatabaseManager>();

        uiBoss.SetActive(true); // ���� ĳ������ ��ȭ �� ��� �̹���
        uiPlayer.SetActive(true); // �÷��̾� ĳ������ ��ȭ �� ��� �̹���
        imgDialogue.SetActive(true); // ��ȭâ �̹���
        uiTextName.SetActive(true); // ��ȭ�ϴ� ĳ������ �̸� ��� ������Ʈ
        uiTextDialogue.SetActive(true); // ĳ���Ͱ� ��� ��� ������Ʈ
    }
    private void Start()
    {
        nowDialogue = databaseManager.dialogues; // DatabaseManager���� Awake()���� ����� Dialogues �迭�� �ҷ��ͼ� ���
        // ���� �������ڸ��� ù��° ���(�̸�, �̹���, ���)�� ���
        ShowImgName(nowDialogue, count);
        ShowTextName(nowDialogue, count);
        // ShowTextContexts(nowDialogue, count, num);
        StartCoroutine(ShowTextTyping());
    }

    private void Update()
    {
        if (!isTyping) // Ÿ������ ���� ��쿡
        {
            // �����̽��ٸ� ������ ���
            if (Input.GetKeyDown(KeyCode.Space))
            {
                num++;
                // ���� ��簡 �����ϴ��� �Ǵ��Ͽ�
                if (num < nowDialogue[count].contexts.Length)
                {
                    // �����ϴ� ��� ��縸 ���
                    // ShowTextContexts(nowDialogue, count, num);
                    StartCoroutine(ShowTextTyping());
                }
                else // ���� ��簡 �������� �ʴ� ���
                {
                    count++; // ���� ȭ�ڷ� �Ѿ��
                    num = 0; // ��� Ƚ���� �ʱ�ȭ

                    // �̺�Ʈ�� ������ �ʾ�����
                    if (nowDialogue[count].eventName.ToString() != "end")
                    {
                        // �˸��� ���(ĳ���� �̸�, ���, ĳ�����̹���)�� ���
                        ShowTextName(nowDialogue, count);
                        ShowImgName(nowDialogue, count);
                        StartCoroutine(ShowTextTyping()); // ���� Ÿ���� ȿ���� �ֱ� ���� �ڷ�ƾ���� ����
                    }
                    else // �̺�Ʈ�� �����ٸ�
                    {
                        changeController.ChangeScene(nowDialogue[0].unitId, nowDialogue[0].eventName);
                        //GameManager.Instance.LoadSceneByName("Boss1SPhase1");
                        //Debug.Log("����ȯ"); // ���� �� ��ȯ�ϴ� ��ɾ�� �����Ͽ� ����� ��
                    }
                }
                StopCoroutine(ShowTextTyping());
            }
        }
    }

    IEnumerator ShowTextTyping()
    {
        isTyping = true;
        string context = nowDialogue[count].contexts[num];
        for (int i = 0; i < context.Length; i++)
        {
            // ���� ȿ�� ������ ���� <�� ������ ��� ����� �����ϴ� ���ǹ�
            if (context[i] == '<')
            {
                while (true)
                {
                    if (i == context.Length)
                    {
                        break;
                    }
                    if (context[i] == '>')
                    {
                        break;
                        // > �� ���ö����� ������� �ʾƾ� ��
                    }
                    i++;
                }
            }
            if (context[i] == '>')
            {
                // > �� ��� for���� ���� ���� ���� < �� ��µ��� �ʴ��� �߰� Ȯ���� �ʿ�
                continue;
            }
            if (context.Contains('*'))
            {
                context = context.Replace('*', ',');
            }
            if (context.Contains('@'))
            {
                context = context.Replace('@', '\n');
            }
            textContext.text = context.Substring(0, i).ToString();
            // Substring(��������, ������)�� �����Ͽ� ������ �ϳ��� ��µǴ� ���
            yield return new WaitForSeconds(typingSpace); // ��µ����� �ð�
        }
        isTyping = false;
    }

    /// <summary>
    /// ���� �������� count�� �˸��� �̸��� ���
    /// ���⼭ count�� Ű�� �Է��Ͽ� ��縦 �ѱ� Ƚ��
    /// </summary>
    /// <param name="nowDialogue"></param>
    /// <param name="count"></param>
    public void ShowTextName(Dialogue[] nowDialogue, int count)
    {
        textName.text = nowDialogue[count].name.ToString();
    }

    /// <summary>
    /// ���� �������� count�� �˸´� ĳ����Id�� ã�Ƽ� ID�� �´� ĳ���ʹ� �����ϰ�
    /// �ƴ� ĳ���ʹ� ��Ӱ� ǥ���ϱ�
    /// ������ ������ unitId�� ����Ͽ� �����Ͽ�����, �����ϰ��� �ϴ� ĳ������ unitId�� Ȯ���ϰ� �����Ͽ� ����� ��
    /// </summary>
    /// <param name="nowDialogue"></param>
    /// <param name="count"></param>
    public void ShowImgName(Dialogue[] nowDialogue, int count)
    {
        ;
        if (nowDialogue[count].unitId == unitId_Boss)
        {
            imgBoss.material.color = new Color(1, 1, 1);
            imgPlayer.material.color = new Color(55 / 255f, 55 / 255f, 55 / 255f);
        }
        else if (nowDialogue[count].unitId == unitId_Player)
        {
            imgPlayer.material.color = new Color(1, 1, 1);
            imgBoss.material.color = new Color(55 / 255f, 55 / 255f, 55 / 255f);
        }
    }
}

