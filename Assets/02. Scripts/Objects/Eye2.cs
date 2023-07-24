using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Eye2 : MonoBehaviour
{
    public Material eye;
    bool one;

    private void Start()
    {
        one = true;
        StartCoroutine("EyeOpen");
    }

    private void Update()
    {
        if (one)
        {
            this.gameObject.transform.localScale = Vector3.Lerp(this.gameObject.transform.localScale,
                new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y + 0.1f, this.gameObject.transform.localScale.z), Time.deltaTime * 13);
        }
    }

    IEnumerator EyeOpen()
    {
        yield return new WaitForSeconds(6f);
        one = false;
    }
}