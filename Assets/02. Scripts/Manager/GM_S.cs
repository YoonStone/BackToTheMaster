using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 강아지 프리팹에 많이 넣어놓고 그 중에서 랜덤으로
// 강아지 프리팹에 넣을 때 이름이랑 견종, 사진 모두 결정해서 넣기
// 강아지 머릿속, 행적, 주인님의 그리움, 생년월일 간식 등만 랜덤

// 강아지한테 승인, 거부 결정해서 생성

public class GM_S : MonoBehaviour {

    public GameObject[] items;
    public Transform from_pos, paper_pos, poket_pos;
    public GameObject directional, candle, candle_body, candle_fire, bird, lastPaper,
            bell_t, o_ui, x_ui,eye, eye_ui;
    public ParticleSystem candle_smoke_part;
    public Image start_t;

    Light directional_Light, candle_Light;

    bool isPass_kinds, isPass_brain, isPass_behavior, isPass_poket, isStart, isTextOut, isEnd;

    GameObject dog_prefab;
    public int gameTime;
    int picture;
    float candle_y;

    Color di_color = new Color(1, 1, 1);
    Color can_color = new Color(0.4245283f, 0.3657144f, 0.2423015f);
    Color text_color = new Color(1, 1, 1, 0);

    string[] dogList = { "beagle2","beagle3",
        "bulldog1","bulldog2","bulldog3","bulldog4",
        "Chihuahua1", "Chihuahua2", "Chihuahua3", "Chihuahua4",
        "Corgi1", "Corgi2", "Corgi3", "Pug1", "Pug2", "Pug3" };

    private void Start()
    {
        directional_Light = directional.GetComponent<Light>();
        candle_Light = candle.GetComponent<Light>();
        
        StartCoroutine("CreateItem");
        StartCoroutine("Ending");
        Invoke("CreateDog", 2f);
        
        isStart = true;
        candle_y = candle_body.transform.position.y;
    }

    // 불빛 (정확한시간적용X)
    private void Update()
    {    

        if (isTextOut)
            // 글자 서서히 사라지게
            start_t.color = Color.Lerp(start_t.color, text_color, Time.deltaTime * 2);
        
        if (isEnd)
        {
            if (GameObject.FindWithTag("Dog"))
                Destroy(GameObject.FindWithTag("Dog"));
            if (GameObject.FindWithTag("PAPER"))
                Destroy(GameObject.FindWithTag("PAPER"));
            if (GameObject.FindWithTag("Poket"))
                Destroy(GameObject.FindWithTag("Poket"));
        }
        else
        {
            // 전체
            directional_Light.color = Color.Lerp(directional_Light.color, di_color, Time.deltaTime / gameTime / 2);

            // 촛불
            candle_Light.color = Color.Lerp(candle_Light.color, can_color, Time.deltaTime / gameTime / 2);

            // 초 녹기
            candle_body.transform.position = Vector3.Lerp(candle_body.transform.position,
                new Vector3(candle_body.transform.position.x, candle_y - 0.2f, candle_body.transform.position.z),
                Time.deltaTime / gameTime);
        }

    }

    // 물건 하나씩 생기기
    IEnumerator CreateItem()
    {
        yield return new WaitForSeconds(0.5f);
        items[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        items[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        items[2].SetActive(true);
        isTextOut = true;
        yield return new WaitForSeconds(0.5f);
        items[3].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        items[4].SetActive(true);
    }

    int rand1;
    Dog dog_s;
    // 강아지 생성 -> 승인 or 거부 결정
    void CreateDog()
    {
        if (GameObject.FindWithTag("Dog"))
        {}
        else
        {
            // 프리팹중에서 강아지 고르기
            int dog_rand = Random.Range(0, dogList.Length);
            dog_prefab = Resources.Load("Dog/" + dogList[dog_rand].ToString()) as GameObject;
            //dog_prefab = Resources.Load("Dog/Beagle2") as GameObject;
            Instantiate(dog_prefab);

            // from 위치에 강아지 생성
            dog_prefab.transform.position = from_pos.position;

            // 강아지한테 들어있는 스크립트에서 승인인지 거부인지 정보 가져오기
            dog_s = dog_prefab.gameObject.GetComponent<Dog>();

            rand1 = Random.Range(0, 5);
            if (rand1 <= 2)
            {
                isPass_kinds = true;
                isPass_brain = true;
                isPass_behavior = true;
                isPass_poket = true;
                print("합격");
            }
            else
            {
                int rand2 = Random.Range(1, 4);
                if (rand2 == 1)
                {
                    isPass_kinds = false;
                    isPass_brain = true;
                    isPass_behavior = true;
                    isPass_poket = true;
                    print("견종으로 불합격");
                }
                else if (rand2 == 2)
                {
                    isPass_kinds = true;
                    isPass_brain = false;
                    isPass_behavior = true;
                    isPass_poket = true;
                    print("뇌사진으로 불합격");
                }
                else
                {
                    isPass_kinds = true;
                    isPass_brain = true;
                    isPass_behavior = true;
                    isPass_poket = false;
                    print("보따리로 불합격");
                }
            }

            // 사진
            picture = dog_s.picture;
        }
    }

    GameObject paper_prefab, dogdog;
    Paper paper;

    // 강아지가 책상에 도착 -> 서류 만들기
    void CreatePaper(GameObject dog)
    {
        // 서류 생성하기
        GameObject paper_prefab = Instantiate(Resources.Load("paper2") as GameObject);
        paper_prefab.transform.position = paper_pos.position;        
        
        // 스크릡트 불러오기
        paper = paper_prefab.GetComponentInChildren<Paper>();

        // 서류의 태그 및 통과여부 결정
        paper.IsPass_brain = isPass_brain;

        // 견종 다르게 할거면 여기서 바꾸기
        if (isPass_kinds)
        {
            paper.picture_int = picture;
        }
        else
        {
            paper.picture_int = 18 - picture;
            if (picture == 9)
                paper.picture_int = Random.Range(10, 18) - picture;
        }

        // 강아지 게임오브젝트 저장
        dogdog = dog;
    }

    int poket_scale;
    GameObject poket_prefab;

    // 강아지가 책상에 도착 -> 보따리 만들기
    void CreatePoket()
    {
        // 보따리 생성하기
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            poket_prefab = Instantiate(Resources.Load("Poket/poket1") as GameObject);
        }
        else if (rand == 1)
        {
            poket_prefab = Instantiate(Resources.Load("Poket/poket2") as GameObject);
        }
        else
        {
            poket_prefab = Instantiate(Resources.Load("Poket/poket3") as GameObject);
        }

        poket_prefab.transform.position = poket_pos.position;

        // 스크립트 불러오기
        Poket poket = poket_prefab.GetComponent<Poket>();

        // 보따리가 합격이면
        if (isPass_poket)
            poket_scale = Random.Range(32, 99);
        // 보따리가 불합격이면
        else
            poket_scale = Random.Range(2, 29);

        poket.MasterScale = poket_scale;
    }

    int good, bad;

    // 승인으로 결정
    void IsPaper_Ok()
    {
        // 합격서류라면
        if(rand1 <= 2)
        {
            // 잘함
            good++;
            o_ui.SetActive(true);
            StartCoroutine(SetFalse(o_ui, 2));
        }
        // 불합격서류라면
        else
        {
            // 못함
            bad++;
            x_ui.SetActive(true);
            StartCoroutine(SetFalse(x_ui, 2));
        }

        // 강아지한테 전달해서 목적지 결정해주기
        dogdog.SendMessage("Pass");
    }

    // 거부로 결정
    void IsPaper_No()
    {
        // 합격서류라면
        if (rand1 <= 2)
        {
            // 못함
            bad++;
            x_ui.SetActive(true);
            StartCoroutine(SetFalse(x_ui, 2));
        }
        // 불합격서류라면
        else
        {
            //잘함
            good++;
            o_ui.SetActive(true);
            StartCoroutine(SetFalse(o_ui, 2));
        }

        // 강아지한테 전달해서 목적지 결정해주기
        dogdog.SendMessage("NonPass");
    }

    // 종 흔들어서 1초에서 3초사이 후다음 강아지 부르기
    public void NextDog()
    {
        if (isStart)
        {
            if (GameObject.FindWithTag("Poket"))
            {
                if (!GameObject.FindWithTag("UI"))
                    bell_t.SetActive(true);
                StartCoroutine(SetFalse(bell_t, 5));
            }
            else if (GameObject.FindWithTag("Dog"))
            {
                print("이미 강아지가 있거나 새가 있음");
            }
            else
            {
                Invoke("CreateDog", Random.Range(1, 4));
            }
        }
    }

    // 다시 유아이 안보이게
    IEnumerator SetFalse(GameObject txt, int time)
    {
        yield return new WaitForSeconds(time);
        txt.SetActive(false);
    }

    // 종소리끝
    public void SoundStop()
    {
        GameObject.FindWithTag("Bell").SendMessage("SoundStop");
    }


    // 게임 끝
    IEnumerator Ending()
    {
        yield return new WaitForSeconds(gameTime);

        candle_fire.SetActive(false);
        candle_smoke_part.Play();

        isEnd = true;
    
        // 새 날아오라는 메세지
        bird.SendMessage("IsEnd");
        isStart = false;
    }

    // 영수증
    public void ShowPaper()
    {
        lastPaper.SetActive(true);
        StartCoroutine("PaperWait");
    }

    public Text good_t, bad_t, dog_num;
    public Image stamp;
    public Material great, badbad;
    IEnumerator PaperWait()
    {
        yield return new WaitForSeconds(1);
        good_t.text = good.ToString() + " 개"; // 맞춘 강아지 수
        yield return new WaitForSeconds(1);
        bad_t.text = bad.ToString() + " 개"; ; // 틀린 강아지 수
        yield return new WaitForSeconds(1);
        dog_num.text = (good + bad).ToString() + " 마리"; // 총 강아지 수
        yield return new WaitForSeconds(1);
        if (good >= bad && good + bad != 0)
        {
            stamp.material = great;
            yield return new WaitForSeconds(2);
            lastPaper.SetActive(false);
            MyDog();
        }
        else
        {
            stamp.material = badbad;
            yield return new WaitForSeconds(2);
            lastPaper.SetActive(false);
            NoDog();
        } // 결과도장
    }

    // 내 강아지 오게
    void MyDog()
    {
        // 강아지 뛰어오기
        dog_prefab = Resources.Load("Dog/myDog") as GameObject;
        Instantiate(dog_prefab);
        // from 위치에 강아지 생성
        dog_prefab.transform.position = from_pos.position;
       
        // 새한테 전달
        bird.SendMessage("DogOk");

        // 눈깜빡깜빡
        StartCoroutine("EyeSceneChange", 10);
    }

    // 강아지 못 만남
    void NoDog()
    {
        // 새한테 전달
        bird.SendMessage("DogNo");
        // 눈깜빡깜빡
        StartCoroutine("EyeSceneChange",  7);
    }

    IEnumerator EyeSceneChange(int time)
    {
        yield return new WaitForSeconds(time);
        eye_ui.SetActive(true);
        eye.SendMessage("Close");
        yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
