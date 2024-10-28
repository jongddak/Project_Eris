using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Scene의 상태를 저장
    // 게임시작 / 1페이즈 결투 / 1페이즈 종료 / 2페이즈 결투 / 몬스터의 사망 / 플레이어의 사망
    public enum SceneState { Start, Phase1Attack, Phase1Talk, Phase2, MonsterDie, PlayerDie };

    public SceneState nowSceneState; // 현재 씬의 상태를 저장

    [SerializeField] DialogueManager dialogueManager; // DialogueSystem.cs를 할당
    /*
    [Header("UI")]
    [SerializeField] private GameObject imgBoss; // 보스 캐릭터의 대화 시 출력 이미지
    [SerializeField] private GameObject imgPlayer; // 플레이어 캐릭터의 대화 시 출력 이미지
    */
    [SerializeField] private GameObject imgDialogue; // 대화창 이미지
    [SerializeField] private GameObject uiTextName; // 대화하는 캐릭터의 이름 출력 오브젝트
    [SerializeField] private GameObject uiTextDialogue; // 캐릭터가 대사 출력 오브젝트
   
    [Header("Audio")]
    [SerializeField] private AudioSource Dialogue;
    [SerializeField] private AudioSource Phase01;
    [SerializeField] private AudioSource Phase02;
    [SerializeField] private AudioSource BossDie;
    [SerializeField] private AudioSource GameOver;
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
        nowSceneState = SceneState.Start; // 씬이 시작하자마자 Start 상태로 시작
    }

    private void Update()
    {
       
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


}
