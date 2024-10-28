using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DatabaseManager 에서 저장한 대사 데이터를 불러내기 위함
public class InteractionEvent : MonoBehaviour
{
    // Dialogue.cs의 DialogueEvent를 사용하기 위함
    [SerializeField] DialogueEvent dialogueEvent;




    /// <summary>
    /// 저장한 대사 데이터를 가져오기 위한 함수
    /// </summary>
    /// <returns></returns>
    public Dialogue[] GetDialogue()
    {
        // DialogueEvent의 dialogues는 Dialogue[]의 형태로 출력하기위한 대사를 저장하는 것으로
        // DatabaseManager의 GetDialogues함수에서 한 이벤트에 사용되는 대사를 가져오도록 하며
        // Dialogue.cs에서 DialogueEvent에 만들었던 Vector2 line의 값을 int로 강제 형변환 후 사용
        dialogueEvent.dialogues = DatabaseManager.instance.GetDialogues((int)dialogueEvent.line.x, (int)dialogueEvent.line.y);
        return dialogueEvent.dialogues;
    }
}
