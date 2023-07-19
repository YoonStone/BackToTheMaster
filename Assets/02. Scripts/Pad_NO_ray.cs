using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad_NO_ray : MonoBehaviour {

    public AudioClip clips;
    AudioSource _audio;

    bool isUse = true;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isUse)
        {
            // ray를 아래방향으로 만든다
            Ray ray = new Ray(transform.position, -transform.up);

            // ray와 충돌한 물체의 정보를 기억할 변수
            RaycastHit hitInfo;
            GameObject pad;

            // 길이가 0.01f인 ray가 물체와 충돌했다면 (닿았다면)
            if (Physics.Raycast(ray, out hitInfo, 0.01f))
            {
                switch (hitInfo.transform.gameObject.tag)
                {
                    // 서류
                    case "Paper":
                        // 도장 소리
                        _audio.clip = clips;
                        _audio.Play();

                        // 새로운 인주 만들기
                        pad = Instantiate(Resources.Load("pad_black") as GameObject);
                        pad.transform.position = this.transform.parent.transform.position;
                        pad.transform.SetParent(this.transform.parent);
                        pad.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

                        pad.SetActive(true);

                        // 묻어있는 인주를 서류로 옮기기
                        transform.position = hitInfo.point + new Vector3(0, 0.001f, 0);
                        transform.SetParent(hitInfo.transform);
                        transform.localRotation = Quaternion.Euler(0f, -transform.localRotation.y * 180, 0f);

                        // 도장찍힌 서류로 전환
                        hitInfo.transform.gameObject.tag = "Paper_No";

                        // 거부서류라고 게임매니저에게 알리기
                        GameObject.FindWithTag("GM").SendMessage("IsPaper_No");

                        isUse = false;

                        // 서류 몇 초후에 사라지기
                        Destroy(hitInfo.transform.parent.gameObject, 3f);
                        GameObject.FindWithTag("Pan").SendMessage("ShowArrow");
                        GameObject.FindWithTag("Poket").SendMessage("ShowArrow");
                        break;

                    // 초록 인주
                    case "Pad_green":
                        // 도장 소리
                        _audio.clip = clips;
                        _audio.Play();

                        // 새로운 인주 만들기
                        pad = Instantiate(Resources.Load("pad_green") as GameObject);
                        pad.transform.position = this.transform.parent.transform.position;
                        pad.transform.SetParent(this.transform.parent);
                        pad.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

                        pad.SetActive(true);

                        // 해당 오브젝트 제거
                        Destroy(this.gameObject);
                        break;

                    case "Desk":
                    case "Desk2":
                        // 도장 소리
                        _audio.clip = clips;
                        _audio.Play();

                        // 새로운 인주 만들기
                        pad = Instantiate(Resources.Load("pad_red") as GameObject);
                        pad.transform.position = this.transform.parent.transform.position;
                        pad.transform.SetParent(this.transform.parent);
                        pad.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

                        // 묻어있는 인주를 책상으로 옮기기
                        transform.position = hitInfo.point + new Vector3(0, 0.001f, 0);
                        transform.SetParent(hitInfo.transform);
                        print(transform.localRotation.y);

                        StartCoroutine("CreatePad", pad);

                        isUse = false;

                        Destroy(this.gameObject, 2f);
                        break;
                }
            }
        } 
    }

    IEnumerator CreatePad(GameObject pad)
    {
        yield return new WaitForSeconds(0.5f);
        pad.SetActive(true);

    }
}
