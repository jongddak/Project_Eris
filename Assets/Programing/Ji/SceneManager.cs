using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager; // DialogueSystem.cs를 할당
    // DatabaseManager.cs의 GetDialogues() 함수를 사용하기 위한 선언
    [SerializeField] DatabaseManager databaseManager;
    private Dialogue[] nowDialogue;
    int count = 0;
    /*
    [Header("UI")]
    [SerializeField] private GameObject imgBoss; // 보스 캐릭터의 대화 시 출력 이미지
    [SerializeField] private GameObject imgPlayer; // 플레이어 캐릭터의 대화 시 출력 이미지
    */
    [SerializeField] private GameObject imgDialogue; // 대화창 이미지
    [SerializeField] private GameObject uiTextName; // 대화하는 캐릭터의 이름 출력 오브젝트
    [SerializeField] private GameObject uiTextDialogue; // 캐릭터가 대사 출력 오브젝트
    /*
     [Header("Audio")]
     [SerializeField] private AudioSource Dialogue;
     [SerializeField] private AudioSource Phase01;
     [SerializeField] private AudioSource Phase02;
     [SerializeField] private AudioSource BossDie;
     [SerializeField] private AudioSource GameOver;
    */

    private void Awake()
    {
        /*  imgBoss.SetActive(true); // 보스 캐릭터의 대화 시 출력 이미지
          imgPlayer.SetActive(true); // 플레이어 캐릭터의 대화 시 출력 이미지*/
        imgDialogue.SetActive(true); // 대화창 이미지
        uiTextName.SetActive(true); // 대화하는 캐릭터의 이름 출력 오브젝트
        uiTextDialogue.SetActive(true); // 캐릭터가 대사 출력 오브젝트
    }

    private void Start()
    {
        nowDialogue = databaseManager.dialogues; // DatabaseManager에서 Awake()에서 저장된 Dialogues 배열을 불러와서 사용
        dialogueManager.ShowTextName(nowDialogue, count); // 시작하자마자 이름 출력
    }
    /*   private void Update()
       {
           switch (nowSceneState)
           {
               case SceneState.Start:
                   imgBoss.SetActive(true); // 보스 캐릭터의 대화 시 출력 이미지
                   uiTextName.SetActive(true); // 대화하는 캐릭터의 이름 출력 오브젝트
                   imgPlayer.SetActive(true); // 플레이어 캐릭터의 대화 시 출력 이미지
                   uiTextName.SetActive(true); // 대화하는 캐릭터의 이름 출력 오브젝트
                   uiTextDialogue.SetActive(true);
                   dialogueManager.NowDialogue(0, 5);
                   imgBoss.SetActive(false); // 보스 캐릭터의 대화 시 출력 이미지
                   uiTextName.SetActive(false); // 대화하는 캐릭터의 이름 출력 오브젝트
                   imgPlayer.SetActive(false); // 플레이어 캐릭터의 대화 시 출력 이미지
                   uiTextName.SetActive(false); // 대화하는 캐릭터의 이름 출력 오브젝트
                   uiTextDialogue.SetActive(false);
                   nowSceneState = SceneState.Phase1Attack;
                   break;
               case SceneState.Phase1Attack:
                   break;
               case SceneState.Phase1Talk:

                   break;
               case SceneState.Phase2:
                   break;
               case SceneState.MonsterDie:

                   break;
           }

       }*/
    private void Update()
    {
        Debug.Log($"업데이트시작 : {count}");

        // 우선 키입력으로 작동하지 않아서 우선 마우스 좌클릭으로 구현
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("키 입력");
            StartCoroutine(ShowText());
            count++;
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

    IEnumerator ShowText()
    {
        if (count >= nowDialogue.Length - 1)
        {
            Debug.Log("종료");
            yield return null;
        }
        else
        {
            dialogueManager.ShowTextName(nowDialogue, count);
        }

        for (int num = 0; num < nowDialogue[count].contexts.Length; num++)
        {
            dialogueManager.ShowTextContexts(nowDialogue, count, num);
        }

        yield return null;
    }



}
