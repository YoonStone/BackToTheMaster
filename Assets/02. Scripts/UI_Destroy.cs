using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Destroy : MonoBehaviour {

    public Image start_t;
    Color text_color = new Color(1, 1, 1, 0);
    
    private void Update()
    {
        // 글자 서서히 사라지게
        start_t.color = Color.Lerp(start_t.color, text_color, Time.deltaTime * 1.5f);
    }
}
