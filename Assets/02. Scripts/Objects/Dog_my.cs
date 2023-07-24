using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog_my : MonoBehaviour {

    //강아지 도착하는위치 바꾸기(너무가까움)
                                  // 책상 위         위치변경o               // 꿈 속                                        // 별
    Vector3[] points = { new Vector3(-0.308f, 1.063f, 0.268f),  new Vector3(-27.5f, -0.2f, 23.8f), new Vector3(17.027f, 2.03f, 21f)};
    NavMeshAgent nav;
    Animator anim;
    AudioSource _audio;

    float radius = 0.3f;
    bool isStop;

    // 거부당했을 때 느려진 속도, 일어나는 속도, 먹는 속도
    public float n_speed;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        
        anim.SetBool("IsSit", true);

        // 처음 목적지는 책상
        nav.destination = points[0];

        // 뛰어오는 소리
        _audio.Play();
    }

    private void Update()
    {
        if (isStop)
            // 책상에 도착 후 방향 (wait이 true가 되면 X)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 177f, 0)), Time.deltaTime * 3f);

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
    }
        
    private void OnTriggerEnter(Collider other)
    {
        // 앉아만 있는 강아지
        if(other.tag == "Hand")
        {
            print("쓰담");
            anim.SetTrigger("IsTouch");  
        }
    }
}

