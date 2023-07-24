using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour {

    Animator anim;
    AudioSource audi;
    public Transform tree, desk, paper_pos;
    public GameObject gm, desk_coll, a, b, c, d, e, f, g, h, i, ww, we, we2, wr;
    public GameObject[] paper_ex;
    public float speed;

    bool isStart, isEnd, isDesk;
    void Start () {
        anim = GetComponent<Animator>();
        audi = GetComponent<AudioSource>();
    }

    public void IsStart()
    {
        isStart = true;
    }

    void Update()
    {
        if (isStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, desk.position, Time.deltaTime * speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, -164.319f, 0)), Time.deltaTime * 3f);
        }

        if (isEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, tree.position, Time.deltaTime * speed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 26.088f, 0)), Time.deltaTime * 3f);
        }

        if (isDesk)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(-16.249f, -223.976f, 0)), Time.deltaTime * 3f);

        // ray를 아래방향으로 만든다
        Ray ray = new Ray(transform.position, -transform.up);

        // ray와 충돌한 물체의 정보를 기억할 변수
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, 5f))
        {
            if (hitInfo.transform.gameObject.tag == "Desk")
            {
                isStart = false;
                isDesk = true;
                anim.SetTrigger("IsStart");
                hitInfo.transform.gameObject.tag = "Desk2";
                Talk_end();
                audi.Play();
            }
        }
    }

    // 게임 끝나서 새 날아옴
    public void IsEnd()
    {
        isEnd = false;
        isStart = true;
        desk_coll.SetActive(true);
    }
    
    // 끝난 이야기 시작
    void Talk_end()
    {
        // 영수증 보여주기
        gm.SendMessage("ShowPaper");
    }

    void DogOk()
    {
        StartCoroutine("Talk_DogOk");
    }

    IEnumerator Talk_DogOk()
    {
        yield return new WaitForSeconds(0.5f);
        ww.SetActive(true);
        yield return new WaitForSeconds(4);
        ww.SetActive(false);
        we.SetActive(true);
        yield return new WaitForSeconds(4);
        we.SetActive(false);
        we2.SetActive(true);
        // 영수증 내려간 후의 말
    }

    void DogNo()
    {
        StartCoroutine("Talk_DogNo");
    }

    IEnumerator Talk_DogNo()
    {
        yield return new WaitForSeconds(0.5f);
        ww.SetActive(true);
        yield return new WaitForSeconds(4);
        ww.SetActive(false);
        wr.SetActive(true);
    }
}
