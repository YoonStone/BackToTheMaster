using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour {

    // Dog -> GM -> isPass를 정해줌
    // 3. 머릿속
    private bool isPass_brain;

    public bool IsPass_brain
    {
        get { return isPass_brain; }
        set { isPass_brain = value; }
    }
    
    // 사진
    public int picture_int, snack;
    public Image picture;

    // 2. 견종 -> 강아지의 태그에 따라
    public string kinds;

    string think_master = "주인님 생각";
    string think_snack = "간식 생각";
    string think_out = "산책 생각";

    public Text name_Txt, kind_Txt, birth_Txt, snack_Txt, dead_Txt,
        brain1_Txt, brain2_Txt, brain3_Txt,
        scale_Txt, scale_Txt2, scale1_Txt, scale2_Txt, scale3_Txt, scale4_Txt;

    ListScript List_S;

    private void Start()
    {
        List_S = GetComponent<ListScript>();

        // 사진
        Picture();

        // 이름
        Name();

        // 견종
        Kinds();

        // 생년월일
        Birth();

        // 죽은 이유
        DeadReason();

        // 뇌
        Brain();

        // 주인님의 생각
        scale_Txt.text = "주인님의 그리움 보따리를";
        scale_Txt2.text = "저울 위에 올려주세요";
        scale1_Txt.text = "";
        scale2_Txt.text = "";
        scale3_Txt.text = "";
        scale4_Txt.text = "";
    }

    // 사진
    void Picture()
    {
        switch (picture_int)
        {
            case 1:
                picture.material = Resources.Load("Dog_Picture/Beagle1") as Material;
                break;
            case 2:
                picture.material = Resources.Load("Dog_Picture/Beagle2") as Material;
                break;
            case 3:
                picture.material = Resources.Load("Dog_Picture/Beagle3") as Material;
                break;
            case 4:
                picture.material = Resources.Load("Dog_Picture/Bulldog1") as Material;
                break;
            case 5:
                picture.material = Resources.Load("Dog_Picture/Bulldog2") as Material;
                break;
            case 6:
                picture.material = Resources.Load("Dog_Picture/Bulldog3") as Material;
                break;
            case 7:
                picture.material = Resources.Load("Dog_Picture/Bulldog4") as Material;
                break;
            case 8:
                picture.material = Resources.Load("Dog_Picture/Chihuahua1") as Material;
                break;
            case 9:
                picture.material = Resources.Load("Dog_Picture/Chihuahua2") as Material;
                break;
            case 10:
                picture.material = Resources.Load("Dog_Picture/Chihuahua3") as Material;
                break;
            case 11:
                picture.material = Resources.Load("Dog_Picture/Chihuahua4") as Material;
                break;
            case 12:
                picture.material = Resources.Load("Dog_Picture/Corgi1") as Material;
                break;
            case 13:
                picture.material = Resources.Load("Dog_Picture/Corgi2") as Material;
                break;
            case 14:
                picture.material = Resources.Load("Dog_Picture/Corgi3") as Material;
                break;
            case 15:
                picture.material = Resources.Load("Dog_Picture/Pug1") as Material;
                break;
            case 16:
                picture.material = Resources.Load("Dog_Picture/Pug2") as Material;
                break;
            case 17:
                picture.material = Resources.Load("Dog_Picture/Pug3") as Material;
                break;
        }
    }

    // 이름
    void Name()
    {
        name_Txt.text = List_S.List_name[Random.Range(0, List_S.List_name.Count)];
    }

    // 견종 -> 사진따라감
    void Kinds()
    {
        if (picture_int < 4) kind_Txt.text = "비글";
        else if (picture_int < 8) kind_Txt.text = "불독";
        else if (picture_int < 12) kind_Txt.text = "치와와";
        else if (picture_int < 15) kind_Txt.text = "웰시코기";
        else kind_Txt.text = "퍼그";
    }

    // 생년월일 -> 랜덤
    void Birth()
    {
        int year = Random.Range(2003, 2016);
        int month = Random.Range(1, 13);
        int day = Random.Range(1, 31);

        birth_Txt.text = year + "넌 " + month + "월 " + day + "일";
    }
    
    // 죽은 이유
    void DeadReason()
    {
        dead_Txt.text = List_S.List_Dead[Random.Range(0, List_S.List_Dead.Count)];
    }

    // 간식
    void RandSnack(int randSnack)
    {
        snack = randSnack;

        if (snack == 1) snack_Txt.text = "개 껌";
        else snack_Txt.text = "비스켓";
    }

    // 머릿속
    void Brain()
    {
        // 주인님 생각을 가장 많이 할 경우
        if (isPass_brain)
        {
            brain1_Txt.text = think_master;

            if (Random.Range(0, 2) == 0)
            {
                brain2_Txt.text = think_snack;
                brain3_Txt.text = think_out;
            }
            else
            {
                brain2_Txt.text = think_out;
                brain3_Txt.text = think_snack;
            }
        }
        // 다른 생각을 많이 할 경우
        else
        {
            // 간식생각
            if (Random.Range(0,2) == 0)
            {
                brain1_Txt.text = think_snack;
                if (Random.Range(0, 2) == 0)
                {
                    brain2_Txt.text = think_master;
                    brain3_Txt.text = think_out;
                }
                else
                {
                    brain2_Txt.text = think_out;
                    brain3_Txt.text = think_master;

                }
            }
            // 산책생각
            else
            {
                brain1_Txt.text = think_out;
                if (Random.Range(0, 2) == 0)
                {
                    brain2_Txt.text = think_master;
                    brain3_Txt.text = think_snack;
                }
                else
                {
                    brain2_Txt.text = think_snack;
                    brain3_Txt.text = think_master;

                }
            }
        }
    }

    // 저울한테 scale값 받아옴
    public void ScaleMsg(int scale)
    {
        scale_Txt.text = "";
        scale_Txt2.text = "";

        // 30g이상 생각하고 있으면
        if (scale >= 30)
        {
            scale1_Txt.text = "축하합니다!";
            scale2_Txt.text = "주인님이 당신을";
            scale3_Txt.text = scale + "g";
            scale4_Txt.text = "그리워하고 있습니다!";
        }

        // 29g이하 생각하고 있으면
        else
        {
            scale1_Txt.text = "안탑깝게도";
            scale2_Txt.text = "주인님이 당신을";
            scale3_Txt.text = scale + "g";
            scale4_Txt.text = "그리워하고 있습니다.";
        }
    }
}
