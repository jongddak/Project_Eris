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
public class DialogueManager : MonoBehaviour
{
    // DatabaseManager.cs의 GetDialogues() 함수를 사용하기 위한 선언
    [SerializeField] DatabaseManager databaseManager;
/*
    [Header("UI")]
    [SerializeField] public GameObject imgBoss; // 보스 캐릭터의 대화 시 출력 이미지
    [SerializeField] public GameObject imgPlayer; // 플레이어 캐릭터의 대화 시 출력 이미지
    [SerializeField] public GameObject imgDialogue; // 대화창 이미지

    [SerializeField] public GameObject uiTextName; // 대화하는 캐릭터의 이름 출력 오브젝트
    [SerializeField] public GameObject uiTextDialogue; // 캐릭터가 대사 출력 오브젝트
*/
    [Header("Test")]
    [SerializeField] private TextMeshProUGUI textName; // 대화하는 캐릭터의 이름 text
    [SerializeField] private TextMeshProUGUI textDialogue; // 캐릭터의 대사 text
    /*
    private void Awake()
    {
        imgBoss.SetActive(false); // 보스 캐릭터의 대화 시 출력 이미지
        imgPlayer.SetActive(false); // 플레이어 캐릭터의 대화 시 출력 이미지
        imgDialogue.SetActive(true); // 대화창 이미지
        uiTextName.SetActive(false); // 대화하는 캐릭터의 이름 출력 오브젝트
        uiTextDialogue.SetActive(false); // 캐릭터가 대사 출력 오브젝트
    }*/

    /// <summary>
    /// 이벤트가 시작하는 번호와 끝나는 번호에 따라서 Text를 알맞은 위치에 출력하고
    /// 이미지를 바꾸는 함수
    /// </summary>
    /// <param name="startNum"></param>
    /// <param name="endNum"></param>
    public void NowDialogue(int startNum, int endNum)
    {
        // nowStart로 데이터베이스에서 사용할 배열의 정보를 불러옴
        Dialogue[] nowStart = databaseManager.GetDialogues(startNum, endNum);

        int i = 0; // 배열의 시작지점

        // do - while 문으로 조건을 확인하면서 반복
        do // 무조건 첫번째 배열을 출력
        {
            Debug.Log(nowStart[i].eventName);
            Debug.Log(nowStart[i].name);
            FindNameImage(nowStart, i); // 이름별로 이미지 오브젝트 활성화
            int j = 0; // nowStart[i]의 배열의 contexts[j]를 탐방하여 출력하기 위해 순서를 정함
            Debug.Log(nowStart[i].contexts[j]);
            ShowText(nowStart, i, j);
            if (Input.GetMouseButtonDown(0))
            {
                if (++i < nowStart.Length) // 배열의 길이가 벗어나지 않는 선에서
                {
                    while (nowStart[i].name == "") // 이름이 공란인 경우에는 같은 캐릭터가 다음 대사를 출력함
                    {
                        j++;
                        Debug.Log(nowStart[i].contexts[j]);
                        ShowText (nowStart, i, j);
                    }
                }
            }
        } while (nowStart[i].eventName != "end"); // nowStart배열에 이벤트이름으로 end를 설정한 곳에 간 경우 반복문을 종료
    }

    private void FindNameImage(Dialogue[] nowStart, int i)
    {
        if (nowStart[i].name == "homunculus")
        {
            textName.text = "호문클루스"; // 이름 Text 출력
        }
        else if (nowStart[i].name == "Eris")
        {

            textName.text = "에리스";
        }
        else return;
    }
    private void ShowText(Dialogue[] nowStart, int i, int j)
    {

        textDialogue.text = nowStart[i].contexts[j].ToString();
    }
}
