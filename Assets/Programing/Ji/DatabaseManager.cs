using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;

    [SerializeField] string csvFileName; // CSV 파일의 이름을 지정

    // Dictionary를 <string, Dialogue> 로 제작
    // string = EventName 으로 각 상황별로 불러와질 Dialogue의 딕셔너리
    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false; // 데이터 파싱 후 제대로 저장이 되었는지 여부를 확인할 수 있는 변수

    private void Awake()
    {
        if(instance == null) // instance가 null 값이면
        {
            instance = this; // 현재 인스턴스로 저장
            // DatabaseManager와 DialogueParser.cs는 같은 오브젝트에 넣어 한번에 theParser를 찾을 수 있도록 선언
            DialogueParser theParser = GetComponent<DialogueParser>();

            Dialogue[] dialogues = theParser.Parser(csvFileName); // DialogueParser의 Parser함수를 실행
            // dialogues에 csv파일의 데이터가 전부 담기게 됨

            // i가 배열의 줄 수 = Dialogue.cs에서 사용하는 vector2 line과 동일한 값 
            // x번째부터 y번째의 대사를 출력하도록 할때 사용함
            for(int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i, dialogues[i]);
            }
            isFinish = true; // 데이터의 저장이 완료됨
        }

    }

    /// <summary>
    /// 한 이벤트에 사용되는 대사 Dialogue를 가져오는 함수
    /// Dialogue[] : 한 이벤트에 사용되는 대사가 여러줄이므로 배열로 설정
    /// StartNum : 이벤트 대사가 시작하는 번째의 줄 수 / 위의 dialogueDic<int,strint>에서 int값에 들어갈 것
    /// EndNum : 이벤트 대사가 끝나는 하는 번째의 줄 수 / 위의 dialogueDic<int,strint>에서 int값에 들어갈 것
    /// 
    /// 즉, Start 이벤트에 필요한 대사는 0번째부터 5번째이므로
    ///     StartNum = 0, EndNum = 5
    ///     phase1 이벤트에 필요한 대사는 6번째부터 10번째
    ///     StartNum = 6, EndNum = 10
    ///     MonsterDie 이벤트에 필요한 대사는 11번째부터 16번째
    ///     StartNum = 11, EndNum = 16
    /// </summary>
    /// <param name="StartNum"></param>
    /// <param name="EndNum"></param>
    /// <returns></returns>
    public Dialogue[] GetDialogues(int StartNum, int EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); // 대사의 양이 이벤트 별로 다르기 때문에 리스트로 저장

        // 필요한 대사의 줄 수 = EndNum - StartNum + 1
        for(int i = 0; i <= EndNum - StartNum; i++)
        {
            // StartNum에 i값을 더하여 사용해야 원하는 대사의 정확한 줄을 dialogueDic에 저장 가능
            dialogueList.Add(dialogueDic[StartNum+i]);
            /*Debug.Log(dialogueList[i].eventName);
            Debug.Log(dialogueList[i].name);
            Debug.Log(dialogueList[i].contexts);*/
        }

        return dialogueList.ToArray(); // 리스트를 배열로 변환하여 출력하기
    }

}
