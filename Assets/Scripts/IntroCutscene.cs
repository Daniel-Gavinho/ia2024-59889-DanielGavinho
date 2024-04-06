using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class IntroCutscene : MonoBehaviour
{
    public Sprite Image1;
    public Sprite Image2;
    public TextMeshProUGUI Text;
    public Image Image;
    public string Text1;
    public string Text2; 

    private void Start()
    {
        StartCoroutine(Cutscene());
    }

    private IEnumerator Cutscene()
    {
        yield return StartCoroutine(WriteText(Text1));
        Image.sprite = Image2;
        yield return StartCoroutine(WriteText(Text2));
        SceneManager.LoadScene(1);
    }

    private IEnumerator WriteText(string text)
    {
        foreach (char c in text)
        {
            Text.text += c;
            yield return new WaitForSeconds(0.1f);
        }

        while(!Input.anyKeyDown)
        {
            yield return null;
        }

        Text.text = "";
    }
}
