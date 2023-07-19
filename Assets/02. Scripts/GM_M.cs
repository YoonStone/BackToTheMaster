using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 강아지 프리팹에 많이 넣어놓고 그 중에서 랜덤으로
// 강아지 프리팹에 넣을 때 이름이랑 견종, 사진 모두 결정해서 넣기
// 강아지 머릿속, 행적, 주인님의 그리움, 생년월일 간식 등만 랜덤

// 강아지한테 승인, 거부 결정해서 생성

public class GM_M : MonoBehaviour {
    
    public void SoundStop()
    {
        GameObject.FindWithTag("Bell").SendMessage("SoundStop");
    }
}
