using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleAnimate : MonoBehaviour
{
    private TMP_Text myText;
    // Start is called before the first frame update
    void Start()
    {
        myText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        myText.fontSize = 36 + 6 * Mathf.Sin(Time.time * 2);
    }
}
