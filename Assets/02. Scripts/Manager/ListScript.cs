using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListScript : MonoBehaviour {

    public List<string> List_Good = new List<string>();
    public List<string> List_Bad = new List<string>();
    public List<string> List_name = new List<string>();
    public List<string> List_Dead = new List<string>();

    public void Awake()
    {
        // 이름
        List_name.Add("무이");
        List_name.Add("페티");
        List_name.Add("몰랑이");
        List_name.Add("민식이");
        List_name.Add("밍키");
        List_name.Add("마루");
        List_name.Add("방울");
        List_name.Add("짜장");
        List_name.Add("솜이");
        List_name.Add("감자");
        List_name.Add("해피");
        List_name.Add("보리");
        List_name.Add("뭉치");
        List_name.Add("망치");
        List_name.Add("단추");
        List_name.Add("콩이");
        List_name.Add("두부");
        List_name.Add("아름이");
        List_name.Add("엘리자베스");
        List_name.Add("코코");
        List_name.Add("홍시");
        List_name.Add("봉구");
        List_name.Add("덕구");
        List_name.Add("흰둥이");
        List_name.Add("누렁이");

        // +
        List_Good.Add("주인님이 화장실 갈 때 문 앞에서 지켜줬다.");
        List_Good.Add("주인님에게 발을 잘 건내줬다.");
        List_Good.Add("‘앉아’를 잘 했다.");
        List_Good.Add("‘기다려’를 잘 했다.");
        List_Good.Add("간식을 바로 먹지 않고 참았다.");
        List_Good.Add("주인님이 던진 공을 잘 물어왔다.");
        List_Good.Add("우울해 하는 주인님을 응원했다.");
        List_Good.Add("목욕할 때 얌전히 앉아있었다.");
        List_Good.Add("주인님이 외출할 때 짖지 않았다.");
        List_Good.Add("주인님이 악몽을 꾸지 않게 옆을 지켜줬다.");
        List_Good.Add("낯선 사람으로부터 주인님을 지켜줬다.");
        List_Good.Add("주인님이 외출하고 돌아올 때까지 창문 앞에서 기다렸다.");

        // -
        List_Bad.Add("주인님 몰래 음식을 먹었다.");
        List_Bad.Add("주인님의 장난감을 부러트렸다.");
        List_Bad.Add("주인님의 이불에 싼 똥을 먹었다.");
        List_Bad.Add("주인님이 애지중지하던 화분을 깼다.");
        List_Bad.Add("주인님이 잡으려 해서 도망갔다.");
        List_Bad.Add("귀가 간지러워 피가 날때까지 긁었다.");
        List_Bad.Add("주인님 음식에 침을 흘렸다.");
        List_Bad.Add("주인님의 이불에 쉬야를 했다.");
        List_Bad.Add("주인님 침대에 몰래 올라갔다.");
        List_Bad.Add("주인님 집의 콘센트를 물어 뜯었다.");
        List_Bad.Add("주인이 관심을 주지 않아 소파를 물어 뜯었다.");
        List_Bad.Add("주인님이 놀아주지않아 얼굴에 쉬야를 했다.");
        List_Bad.Add("문을 열어주지않아 강제로 도망쳐나왔다.");
        List_Bad.Add("간식이 먹고싶어 쓰레기통에 쓰레기를 꺼내 먹었다.");
        List_Bad.Add("산책 중에 목줄을 빼고 도망갔었다.");
        List_Bad.Add("주인님이 꼬리를 잡아당겨 발가락을 물었다.");
        List_Bad.Add("주인님이 얼굴을 때려 손을 물었다.");

        // 죽은 이유
        List_Dead.Add("병원에서 안락사");
        List_Dead.Add("집에서 안락사");

        List_Dead.Add("바다에서 익사");

        List_Dead.Add("폐렴으로 병사");
        List_Dead.Add("심장병으로 병사");
        List_Dead.Add("폐렴으로 병사");
        List_Dead.Add("신부전증으로 병사");
        List_Dead.Add("심장사상충으로 병사");
        List_Dead.Add("피부암으로 병사");
        
        List_Dead.Add("산책길에서 돌연사");
        List_Dead.Add("집에서 돌연사");

        List_Dead.Add("도로에서 충돌사");
        List_Dead.Add("산에서 추락사");

        List_Dead.Add("집에서 자연사");
        List_Dead.Add("공원에서 자연사");
    }
}