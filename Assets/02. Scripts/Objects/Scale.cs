using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScript;

public class Scale : MonoBehaviour {

    public GameObject needle, arrow, canvas;
    public Text scale_ui;
    int scale, state;
    bool isPoket, isArrow;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (state)
        {
            // 아무것도 안할 때
            case 0:
                break;
            // 물건 올라와서 50전까지 돌리기
            case 1:
                // 50보다 크면
                if (scale >= 50)
                {
                    needle.transform.rotation = Quaternion.Slerp(needle.transform.rotation, Quaternion.Euler(0f, -330f, 180f), Time.deltaTime);
                    Invoke("Heavy", 2f);
                }

                // 50보다 작으면
                else
                {
                    needle.transform.rotation = Quaternion.Slerp(needle.transform.rotation, Quaternion.Euler(0f, -330f, -(scale * 3.6f)), Time.deltaTime);
                    Result(scale);
                }
                break;
            // 무거울 때
            case 2:
                needle.transform.rotation = Quaternion.Slerp(needle.transform.rotation, Quaternion.Euler(0f, -330f, -(scale * 3.6f)), Time.deltaTime);
                Result(scale);
                break;
            // 리셋할 때
            case 3:
                needle.transform.rotation = Quaternion.Slerp(needle.transform.rotation, Quaternion.Euler(0f, -330f, 0f), Time.deltaTime);
                break;
        }
    }

    void Heavy()
    {
        state = 2;
    }

    void Result(int scale)
    {
        if(scale != 0)
        {
            canvas.SetActive(true);
            scale_ui.text = scale + "g";
            Invoke("HideScaleUI", 2f);

            // scale값 paper로 전달
            if (GameObject.FindWithTag("Paper"))
                GameObject.FindWithTag("Paper").SendMessage("ScaleMsg", scale);
        }
    }

    void HideScaleUI()
    {
        canvas.SetActive(false);
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Poket")
        {
            print("보따리 닿음");
            isPoket = true;

            coll.gameObject.transform.SetParent(this.transform);

            // 저울 아래로 내려가는 애니메이션
            anim.SetBool("IsDown", true);

            // 서류 가져오기
            Poket poket = coll.gameObject.GetComponent<Poket>();

            // 서류에 scale값 전달
            scale = poket.MasterScale;
            print(scale);
            // scale값 만큼 바늘 돌려주기
            state = 1;
        }
    }

    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Poket")
        {
            isPoket = false;

            // 저울 위로 올라가는 애니메이션
            anim.SetBool("IsDown", false);
            // 초기화
            scale = 0;
            state = 3;
        }
    }

    
    public void ShowArrow()
    {
        if (isPoket)
        {
            arrow.SetActive(true);
            isArrow = true;
        }
    }

    public void HideArrow()
    {
        if (isArrow)
        {
            arrow.SetActive(false);
        }
    }
    
}