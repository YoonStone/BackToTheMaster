using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad_ray : MonoBehaviour
{
    private void Update()
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
                // 빨간 인주
                case "Pad_red":
                    // 새로운 인주 만들기
                    pad = Instantiate(Resources.Load("pad_red") as GameObject);
                    pad.transform.position = this.transform.parent.transform.position;
                    pad.transform.SetParent(this.transform.parent);
                    pad.transform.localRotation = Quaternion.Euler(90f, this.transform.parent.transform.rotation.y, this.transform.parent.transform.rotation.z);

                    pad.SetActive(true);

                    // 해당 오브젝트 제거
                    Destroy(this.gameObject);
                    break;

                // 초록 인주
                case "Pad_green":
                    // 새로운 인주 만들기
                    pad = Instantiate(Resources.Load("pad_green") as GameObject);
                    pad.transform.position = this.transform.parent.transform.position;
                    pad.transform.SetParent(this.transform.parent);
                    pad.transform.localRotation = Quaternion.Euler(90f, this.transform.parent.transform.rotation.y, this.transform.parent.transform.rotation.z);
                    pad.SetActive(true);

                    // 해당 오브젝트 제거
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
