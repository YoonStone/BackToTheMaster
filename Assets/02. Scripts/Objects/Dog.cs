using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour {

    // 책상 위                                     // 꿈 속                                        // 별
    readonly Vector3[] points = { new Vector3(-1.1f, 1.063f, 0.198f),  new Vector3(-27.5f, -0.2f, 23.8f), new Vector3(17.027f, 2.03f, 21f)};
    Transform desk_point;
    NavMeshAgent nav;
    Animator anim;
    AudioSource _audio;

    int po_int = 0;
    float radius = 0.3f;
    bool isStop, isGetAround, isReady;

    // 거부당했을 때 느려진 속도, 일어나는 속도, 먹는 속도
    public float n_speed, stand_t, eat_t, eat_walk_rot_t;

    public int rand_Snack;

    // 사진
    public int picture;

    int rand_UpDown;

    private void Awake()
    {
        rand_Snack = Random.Range(1, 3);
        print(rand_Snack);
    }

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        desk_point = GameObject.FindWithTag("Desk_point").GetComponent<Transform>();
        GameObject.FindWithTag("Desk_point1").GetComponent<BoxCollider>().enabled = false;

        // 처음부터 돌아다니는 강아지 일 것인지 앉아있는 강아지 일 것인지 랜덤 (3:2)
        rand_UpDown = Random.Range(0, 5);
        if (rand_UpDown <= 2)
        {
            print("앉아있는 강아지");
            anim.SetBool("IsSit", true);
        }
        else
        {
            print("돌아다니는 강아지");
            anim.SetBool("IsSit", false);
        }

        // 처음 목적지는 책상
        nav.destination = points[0];

        // 뛰어오는 소리
        _audio.Play();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            print(isGetAround);
            print(po_int);
            Pass();
        }
        if (Input.GetMouseButton(1))
        {
            NonPass();

        }

        if (GameObject.FindWithTag("Paper"))
            GameObject.FindWithTag("Paper").SendMessage("RandSnack", rand_Snack);

        switch (po_int)
        {
            // 처음
            case 0:
                if (isStop)
                    // 책상에 도착 후 방향 (wait이 true가 되면 X)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 118.278f, 0)), Time.deltaTime * 3f); break;
            // 책상에서 출발
            case 1:
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), Time.deltaTime * 1f); break;
            // 꿈 속
            case 2:
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, -50.655f, 0)), Time.deltaTime * 1.2f); break;
            // 별
            case 3:
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 53.244f, 0)), Time.deltaTime * 1.2f); break;
            // 책상 포인트
            case 4:
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, -90f, 0)), Time.deltaTime * 2f); break;

        }

        // 꿈이나 별에 도착하면 없어지기
        if (Vector3.Distance(transform.position, points[1]) < radius
            || Vector3.Distance(transform.position, points[2]) < radius)
            Destroy(this.gameObject);

        // Desk함수를 한 번만 호출
        if (!isStop)
        {
            // 책상에 도착하면
            if (Vector3.Distance(transform.position, points[0]) < radius)
                Desk();
        }
    }

    void Desk()
    {
        // 책상용 애니메이션 시작
        anim.SetTrigger("IsDesk");

        // 멈추기
        isStop = true;
        _audio.Stop();
        nav.destination = transform.position;

        // 서류 생기도록 GM한테 알려주기
        GameObject.FindWithTag("GM").SendMessage("CreatePaper", this.gameObject);
        GameObject.FindWithTag("GM").SendMessage("CreatePoket");        
        
        // 돌아다니는 강아지는 함수 호출
        if(rand_UpDown >= 3)
        {
            Invoke("Stand", stand_t);
        }

        Invoke("SnackReady", 5f);
    }

    void Stand()
    {
        // 목적지를 저울 앞으로 설정
        nav.speed = 0.5f; ;
        nav.destination = desk_point.position;
        GameObject.FindWithTag("Desk_point1").GetComponent<BoxCollider>().enabled = true;

        // 돌아다니고 있기 때문에 바로 출발할 수 없음을 알려주는 bool
        isGetAround = true;
    }
    
    void SnackReady()
    {
        isReady = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Desk_point1")
        {
            nav.destination = desk_point.position;
            po_int = 0;
        }

        if (other.tag == "Desk_point")
        {
            nav.destination = points[0];
            po_int = 4;
        }

        // 앉아만 있는 강아지
        if(rand_UpDown <= 2 && other.tag == "Hand")
        {
            print("쓰담");
            anim.SetTrigger("IsTouch");  
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        // 간식먹을 준비가 되면
        if (isReady)
        {
            // 앉아있는 강아지
            if (rand_UpDown <= 2)
            {
                switch (coll.gameObject.tag)
                {
                    // 껌 먹었을 때
                    case "Gum":
                        if (rand_Snack == 1)
                        {
                            Destroy(coll.gameObject);
                            GameObject.FindWithTag("Desk_point1").GetComponent<BoxCollider>().enabled = false;
                            anim.SetTrigger("IsEat");
                        }
                        break;
                    // 비스켓 먹었을 때
                    case "Biscuit":
                        if (rand_Snack == 2)
                        {
                            Destroy(coll.gameObject);
                            GameObject.FindWithTag("Desk_point1").GetComponent<BoxCollider>().enabled = false;
                            anim.SetTrigger("IsEat");
                        }
                        break;
                }
            }
            // 돌아다니는 강아지
            else
            {
                switch (coll.gameObject.tag)
                {
                    // 껌 먹었을 때
                    case "Gum":
                        if (rand_Snack == 1)
                        {
                            // 닿은 간식 없애기
                            Destroy(coll.gameObject);

                            // 먹는 애니메이션
                            nav.destination = transform.position;
                            GameObject.FindWithTag("Desk_point1").GetComponent<BoxCollider>().enabled = false;
                            anim.SetBool("IsSit", true);
                            StartCoroutine("Eat");
                        }
                        break;
                    // 비스켓 먹었을 때
                    case "Biscuit":
                        if (rand_Snack == 2)
                        {
                            // 닿은 간식 없애기
                            Destroy(coll.gameObject);

                            // 먹는 애니메이션
                            nav.destination = transform.position;
                            GameObject.FindWithTag("Desk_point1").GetComponent<BoxCollider>().enabled = false;
                            anim.SetBool("IsSit", true);
                            StartCoroutine("Eat");
                        }
                        break;
                }
            }
        }        
    }

    IEnumerator Eat()
    {
        yield return new WaitForSeconds(eat_t);

        // 콜라이더 없애고, 원래 자리로 돌아가기
        nav.destination = points[0];
        po_int = 4;

        yield return new WaitForSeconds(eat_walk_rot_t);

        po_int = 0;
        isGetAround = false;
    }

    // -------------- 승인 ----------------- //

    public void Pass()
    {
        if (isGetAround)
        {
            nav.destination = points[0];
            po_int = 4;
            anim.SetTrigger("IsGo");

            // 태그 없애기
            this.gameObject.tag = "Dog2";

            Invoke("Dream", 0.3f);
        }
        else
        {
            GameObject.FindWithTag("Desk_point1").GetComponent<BoxCollider>().enabled = false;

            // 태그 없애기
            this.gameObject.tag = "Dog2";

            // 일어난 후에        
            anim.SetTrigger("IsGo");
            StartCoroutine("DreamWait");
        }
    }

    void Dream()
    {
        StartCoroutine("DreamWait");
    }

    IEnumerator DreamWait()
    {
        // 목적지 꿈 속으로
        yield return new WaitForSeconds(0.2f);
        _audio.Play();
        anim.SetBool("IsDream", true);
        po_int = 1;

        yield return new WaitForSeconds(0.5f);
        nav.speed =2.5f;
        nav.destination = points[1];

        yield return new WaitForSeconds(2.3f);
        po_int = 2;
    }

    // -------------- 거부----------------- //
    
    public void NonPass()
    {
        // 돌아다니고 있다면 일단 왼쪽으로 보내기
        if (isGetAround)
        {
            nav.destination = points[0];
            po_int = 4;
            anim.SetTrigger("IsGo");

            // 태그 없애기
            this.gameObject.tag = "Dog2";

            Invoke("Star", 0.3f);
        }
        // 앉아있다면 바로 보내기
        else
        {
            GameObject.FindWithTag("Desk_point1").GetComponent<BoxCollider>().enabled = false;

            // 태그 없애기
            this.gameObject.tag = "Dog2";

            // 일어난 후에        
            anim.SetTrigger("IsGo");
            StartCoroutine("BackWait");
        }
    }
    
    void Star()
    {
        StartCoroutine("BackWait");
    }

    IEnumerator BackWait()
    {
        // 목적지 별로
        yield return new WaitForSeconds(0.2f);
        _audio.Play();
        _audio.pitch = 1;
        anim.SetBool("IsStar", true);
        po_int = 1;

        yield return new WaitForSeconds(0.5f);
        nav.speed = n_speed;
        nav.destination = points[2];

        yield return new WaitForSeconds(3.5f);
        po_int = 3;
    }
}

