using TMPro;
using UnityEngine;
using static SceneManager;
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

    [Header("UI")]
    [SerializeField] private GameObject imgBoss; // 보스 캐릭터의 대화 시 출력 이미지
    [SerializeField] private GameObject imgPlayer; // 플레이어 캐릭터의 대화 시 출력 이미지

    [Header("Test")]
    [SerializeField] private TextMeshProUGUI textName; // 대화하는 캐릭터의 이름 text
    [SerializeField] private TextMeshProUGUI textDialogue; // 캐릭터의 대사 text
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


    /// <summary>
    /// SceneManager 에서 현재의 Scene의 상태에 따라서 DatabaseManager.cs의 GetDialogues함수를 이용해
    /// 각 위치에 알맞은 데이터를 Dialogue[] nowDialogueArr의 형태로 저장하여 리턴
    /// </summary>
    /// <param name="nowSceneState"></param>
    /// <returns></returns>
    public Dialogue[] cheakEvent(SceneState nowSceneState)
    {
        Dialogue[] nowDialogueArr;
        switch (nowSceneState)
        {
            case SceneState.Start:
                nowDialogueArr = databaseManager.GetDialogues(0, 5);
                return nowDialogueArr;
            case SceneState.Phase1Talk:
                nowDialogueArr = databaseManager.GetDialogues(6, 10);
                return nowDialogueArr;
            case SceneState.MonsterDie:
                nowDialogueArr = databaseManager.GetDialogues(11, 17);
                return nowDialogueArr;
        }
        return null;
    }

    /// <summary>
    /// 현재 데이터의 count에 알맞은 이름을 출력
    /// 여기서 count는 키를 입력하여 대사를 넘긴 횟수
    /// </summary>
    /// <param name="nowDialogue"></param>
    /// <param name="count"></param>
    private void ShowTextName(Dialogue[] nowDialogue, int count)
    {
        // 대사가 두 줄 이라서 이름이 공백인 경우가 있기에 공백이 아니면 이름을 출력
        if (nowDialogue[count].name != "")
        {
            textName.text = nowDialogue[count].name.ToString();
        }
        // 공백인 경우에는 이전의 이름을 그대로 써야하므로 출력에 변동이 없음
    }

    /// <summary>
    /// 현재 데이터의 count에 알맞는 캐릭터Id를 찾아서 ID에 맞는 캐릭터는 선명하게
    /// 아닌 캐릭터는 어둡게 표시하기
    /// </summary>
    /// <param name="nowDialogue"></param>
    /// <param name="count"></param>
    private void ShowImgName(Dialogue[] nowDialogue, int count)
    {


    }
}
