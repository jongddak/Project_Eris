using System.Collections;
using TMPro;
using UnityEngine;
/* 대사 스크립트 관련 유튜브 영상
 * https://www.youtube.com/watch?v=DPWvoUlHbjg&list=PLUZ5gNInsv_NG_UKZoua8goQbtseAo8Ow&index=11
 * https://www.youtube.com/watch?v=1fRbGvQlIEQ
 * https://www.youtube.com/watch?v=_04sCWLHoXU
 * https://www.youtube.com/watch?v=qJjfYvEYKiE
 * 
 * 대사를 타자형식으로 출력하는 것 관련 유튜브 영상
 * https://www.youtube.com/watch?v=OjcPuEVQT6s
 */

// 
public class DialogueManager : MonoBehaviour
{

    // DatabaseManager.cs의 GetDialogues() 함수를 사용하기 위한 선언
    [SerializeField] DatabaseManager databaseManager;
    private Dialogue[] nowDialogue;
    int count = 0;
    int num = 0;
    [Header("UI")]
    [SerializeField] private GameObject imgBoss; // 보스 캐릭터의 대화 시 출력 이미지
    [SerializeField] private GameObject imgPlayer; // 플레이어 캐릭터의 대화 시 출력 이미지
    [SerializeField] private GameObject imgDialogue; // 대화창 이미지
    [SerializeField] private GameObject uiTextName; // 대화하는 캐릭터의 이름 출력 오브젝트
    [SerializeField] private GameObject uiTextDialogue; // 캐릭터가 대사 출력 오브젝트

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI textName; // 대화하는 캐릭터의 이름 text
    [SerializeField] private TextMeshProUGUI textContext; // 캐릭터의 대사 text

    /*
        /// <summary>
        /// 이벤트가 시작하는 번호와 끝나는 번호에 따라서 Text를 알맞은 위치에 출력하고
        /// 이미지를 바꾸는 함수
        /// </summary>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        public void NowDialogueArr(int startNum, int endNum)
        {
            // nowStart로 데이터베이스에서 사용할 배열의 정보를 불러옴
            Dialogue[] nowStart = databaseManager.GetDialogues(startNum, endNum);

            int i = 0; // 배열의 시작지점

            // do - while 문으로 조건을 확인하면서 반복
            do // 무조건 첫번째 배열을 출력
            {
                Debug.Log(nowStart[i].eventName);
                Debug.Log(nowStart[i].unitId);
                Debug.Log(nowStart[i].name);
                int j = 0; // nowStart[i]의 배열의 contexts[j]를 탐방하여 출력하기 위해 순서를 정함
                Debug.Log(nowStart[i].contexts[j]);
                //ShowText(nowStart, i, j);
                if (++i < nowStart.Length) // 배열의 길이가 벗어나지 않는 선에서
                {
                    while (nowStart[i].name == "") // 이름이 공란인 경우에는 같은 캐릭터가 다음 대사를 출력함
                    {
                        j++;
                        Debug.Log(nowStart[i].contexts[j]);
                        ShowText(nowStart, i, j);
                    }
                }
            } while (nowStart[i-1].eventName != "end"); // nowStart배열에 이벤트이름으로 end를 설정한 곳에 간 경우 반복문을 종료
        }

        private void Start()
        {
            NowDialogueArr(0, 4);
        }
    */

    private void Awake()
    {
        imgBoss.SetActive(true); // 보스 캐릭터의 대화 시 출력 이미지
        imgPlayer.SetActive(true); // 플레이어 캐릭터의 대화 시 출력 이미지
        imgDialogue.SetActive(true); // 대화창 이미지
        uiTextName.SetActive(true); // 대화하는 캐릭터의 이름 출력 오브젝트
        uiTextDialogue.SetActive(true); // 캐릭터가 대사 출력 오브젝트
    }

    private void Start()
    {
        nowDialogue = databaseManager.dialogues; // DatabaseManager에서 Awake()에서 저장된 Dialogues 배열을 불러와서 사용
        ShowTextName(nowDialogue, count); // 시작하자마자 이름 출력
        ShowTextContexts(nowDialogue, count, num); // 시작하자마자 첫 대사 출력
    }

    private void Update()
    {
        Debug.Log($"업데이트시작 : {count}");

        // 우선 키입력으로 작동하지 않아서 우선 마우스 좌클릭으로 구현
        if (Input.GetMouseButtonDown(0))
        {
            count++;
            Debug.Log("키 입력");
            StartCoroutine(ShowTextName());
           // StartCoroutine(ShowTextContexts());

        }

        /* 스페이스키를 누르면 다음대사 출력으로 설정하였으나 스페이스바로 작동하지 않음을 발견
         * 왜 키보드 입력이...안되지? 왜?
         * 내가 뭘 놓쳤나...?
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("스페이스 입력");
            StartCoroutine(ShowText());
            count++;
        }
        */
    }

    IEnumerator ShowTextName()
    {
        if (count >= nowDialogue.Length - 1)
        {
            Debug.Log("종료");
            yield return null;
        }
        else
        {
            ShowTextName(nowDialogue, count);
            for (num = 0; num < nowDialogue[count].contexts.Length;)
            {
                ShowTextContexts(nowDialogue, count, num);
                num++;
                if (num >= nowDialogue[count].contexts.Length - 1)
                {
                    Debug.Log("대사종료");
                    yield return null;
                }
            }
        }
        
        yield return null;
    }

    IEnumerator ShowTextContexts()
    {
        for (num = 0; num < nowDialogue[count].contexts.Length; num++)
        {
            ShowTextContexts(nowDialogue, count, num);
            if (num >= nowDialogue[count].contexts.Length - 1)
            {
                yield return null;
            }
        }

    }

    /// <summary>
    /// 현재 데이터의 count에 알맞은 이름을 출력
    /// 여기서 count는 키를 입력하여 대사를 넘긴 횟수
    /// </summary>
    /// <param name="nowDialogue"></param>
    /// <param name="count"></param>
    public void ShowTextName(Dialogue[] nowDialogue, int count)
    {
        textName.text = nowDialogue[count].name.ToString();
    }

    public void ShowTextContexts(Dialogue[] nowDialogue, int count, int num)
    {
        Debug.Log(nowDialogue[count].contexts[num]);
        textContext.text = nowDialogue[count].contexts[num].ToString();
    }

    /// <summary>
    /// 현재 데이터의 count에 알맞는 캐릭터Id를 찾아서 ID에 맞는 캐릭터는 선명하게
    /// 아닌 캐릭터는 어둡게 표시하기
    /// </summary>
    /// <param name="nowDialogue"></param>
    /// <param name="count"></param>
    public void ShowImgName(Dialogue[] nowDialogue, int count)
    {


    }


}
