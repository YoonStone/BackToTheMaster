using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ViveHold_Start : MonoBehaviour
{
    public GameObject bis, gu, bell_arrow;
    //SteamVR_TrackedObject를 저장할 변수
    private SteamVR_TrackedObject trackedObj;

    //SteamVR_Controller.Input 클래스의 접근성을 위한 프로퍼티 설정
    private SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }


    Animator anim;
    private GameObject target;

    private bool isGripped = false;
    private bool isNoDragging;

    void Awake()
    {
        //컨트롤러에 포함된 SteamVR_TrackedObject 스크립트 저장
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller is not detected");
            return;
        }

        //트리거 버튼을 클릭 했을 경우
        if (controller.GetHairTriggerDown())
        {
            anim.SetBool("IsPick", true);

            if (target != null)
            {
                isGripped = true;

                if (!isNoDragging) //닿은 물체를 드래그 할 때
                {
                    if (target.GetComponent<Rigidbody>() != null)
                    {
                        target.transform.SetParent(this.transform);
                        target.GetComponent<Rigidbody>().isKinematic = true;

                        if (target.tag == "Bell")
                        {
                            if (target.transform.rotation.x < -130f || target.transform.rotation.x > -30f)
                            {
                                bell_arrow.SetActive(false);
                                // 게임매니저한테 다음 강아지 들어오라고 메시지 전달
                                GameObject.FindWithTag("GM").SendMessage("NextDog");
                                GameObject.FindWithTag("Bell").SendMessage("SoundPlay");
                            }
                        }
                    }
                }
                else //새로운 물체를 생성시켜 드래그 할 때
                {
                    switch (target.tag)
                    {
                        case "Biscuit_m":
                            target = Instantiate(Resources.Load("biscuit") as GameObject);
                            target.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.05f, this.transform.position.z);
                            break;
                        case "Gum_m":
                            target = Instantiate(Resources.Load("gum") as GameObject);
                            target.transform.position = new Vector3(this.transform.position.x, this.transform.position.y -0.05f, this.transform.position.z);
                            break;
                    }

                    target.transform.SetParent(this.transform);
                    target.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
        //트리거 버튼을 릴리즈 했을 경우
        if (controller.GetHairTriggerUp() )
        {
            anim.SetBool("IsPick", false);

             if(target != null)
            {
                if (target.GetComponent<Rigidbody>() != null)
                {
                    if(target.tag == "Bell")
                    {
                        target.SendMessage("SoundStop");
                    }
                    var rb = target.GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.velocity = controller.velocity * 2f;
                    rb.angularVelocity = controller.angularVelocity;
                }

                target.transform.SetParent(null);
                isGripped = false;
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!isGripped)
        {
            switch (coll.tag)
            {
                // 컨트롤러에 새로운 오브젝트
                // 개 껌, 비스켓, 종, 돋보기
                case "Biscuit_m":
                    bis.SetActive(true);
                    target = coll.gameObject;
                    isNoDragging = true;
                    break;
                case "Gum_m":
                    gu.SetActive(true);
                    target = coll.gameObject;
                    isNoDragging = true;
                    break;

                // 잡은 오브젝트
                //case "PAPER":
                case "Poket":
                case "Bell":
                case "Gum":
                case "Biscuit":
                    target = coll.gameObject;
                    isNoDragging = false;
                    break;
            }
        }

        if(coll.tag == "Dog" || coll.tag == "MyDog")
        {
            StartCoroutine(LongVibration(0.3f,3999));
            // 강아지와 닿으면 진동 500(기본)
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Biscuit_m")
        {
            bis.SetActive(false);
        }
        else if (coll.tag == "Gum_m")
        {
            gu.SetActive(false);
        }

        if (target)
        {
            if (target.gameObject == coll.gameObject)
            {
                target = null;
                isGripped = false;
            }
        }
    }

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            controller.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }
}