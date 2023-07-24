using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Eye1 : MonoBehaviour
{
    public Material eye;
    bool one;
    
    private void Update()
    {
        if (one)
        {
            this.gameObject.transform.localScale = Vector3.Lerp(this.gameObject.transform.localScale,
                new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y - 0.1f, this.gameObject.transform.localScale.z), Time.deltaTime * 10);
        }
    }

    public void Close()
    {
        one = true;
        StartCoroutine("EyeClose");
    }

    IEnumerator EyeClose()
    {
        yield return new WaitForSeconds(4.5f);
        one = false;
        this.GetComponent<Image>().material = eye;
    }
}