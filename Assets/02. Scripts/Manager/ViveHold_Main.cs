using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViveHold_Main : MonoBehaviour
{
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
                if (target.GetComponent<Rigidbody>() != null)
                {
                    target.transform.SetParent(this.transform);
                    target.GetComponent<Rigidbody>().isKinematic = true;
                    

                    if (target.transform.rotation.x < -130f || target.transform.rotation.x > -30f)
                    {
                        if (target.tag == "Bell_Start")
                        {
                            GameObject.FindWithTag("Bell_Start").SendMessage("SoundPlay");
                            Invoke("GameScene", 2f);
                        }
                    }
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
                    target.SendMessage("SoundStop");
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
            if (coll.tag == "Bell_Start" || coll.tag == "PAPER")
            {
                target = coll.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (target)
        {
            if (target.gameObject == coll.gameObject)
            {
                target = null;
                isGripped = false;
            }
        }
    }
    
    void GameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}