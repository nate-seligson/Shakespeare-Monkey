using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fullscreen : MonoBehaviour
{
    public GameObject darken;
    public TextMeshProUGUI fullscreenText;
    public float startposition;
    bool activated = false;
    public void TextSelected(TextMeshProUGUI text)
    {
        darken.SetActive(true);
        fullscreenText.text = text.text;
        activated = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && activated)
        {
            activated = false;
            fullscreenText.transform.position = new Vector2(fullscreenText.transform.position.x, startposition);
            darken.SetActive(false);
        }
    }
}
