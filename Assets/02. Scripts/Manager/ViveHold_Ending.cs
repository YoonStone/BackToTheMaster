using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViveHold_Ending : MonoBehaviour
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
            if (target != null)
            {
                isGripped = true;

                switch (target.tag)
                {
                    case "Retry":
                        Invoke("Retry", 2f);
                        break;
                    case "GameEnd":
                        Invoke("End", 1f);
                        break;
                    default:
                        anim.SetBool("IsPick", true);
                        if (target.GetComponent<Rigidbody>() != null)
                        {
                            target.transform.SetParent(this.transform);
                            target.GetComponent<Rigidbody>().isKinematic = true;
                        }
                        break;
                }
            }
            else
            {
                anim.SetBool("IsPick", true);
            }
        }

        //트리거 버튼을 릴리즈 했을 경우
        if (controller.GetHairTriggerUp())
        {
            anim.SetBool("IsPick", false);

            if (target != null)
            {
                if (target.tag == "Retry" || target.tag == "GameEnd")
                {
                    isGripped = false;
                }
                else
                {
                    if (target.GetComponent<Rigidbody>() != null)
                    {
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
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!isGripped)
        {
            switch (coll.tag)
            {
                case "Retry":
                case "GameEnd":
                    StartCoroutine(LongVibration(0.2f, 3999));
                    anim.SetBool("IsPoint", true);
                    target = coll.gameObject;
                    break;
                case "Mouse":
                case "Keyboard":
                case "Stand":
                    target = coll.gameObject;
                    break;
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Retry" || coll.tag == "GameEnd")
        {
            anim.SetBool("IsPoint", false);
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

    void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    void End()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }
}