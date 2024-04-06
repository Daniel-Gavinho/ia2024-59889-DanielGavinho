using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GUIScript : MonoBehaviour
{
    public Texture2D dotTexture;
    public GameObject Canvas;
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI TutorialText;
    public Image[] Hearts;
    public int dotSize = 10;
    private GameMasterScript gm;

    public void Deactivate()
    {
        Canvas.SetActive(false);
        this.enabled = false;
    }

    public void WriteText(string text)
    {
        StartCoroutine(WriteCoroutine(text));
    }

    void Start()
    {
        gm = GameMasterScript.Instance;
    }

    void OnGUI()
    {
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Rect dotRect = new Rect(center.x - dotSize / 2, center.y - dotSize / 2, dotSize, dotSize);
        GUI.DrawTexture(dotRect, dotTexture);
    }

    private void Update()
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i + 1 <= gm.Player.GetComponent<PlayerStats>().Health)
            {
                Hearts[i].color = Color.white;
            }
            else
            {
                Hearts[i].color = Color.black;
            }
        }

        TimerText.text = "Time: " + FormatTimer(gm.Timer);
    }

    private IEnumerator WriteCoroutine(string text)
    {
        TutorialText.text = "";

        foreach (char c in text)
        {
            TutorialText.text += c;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(2f);

        TutorialText.text = "";
    }

    string FormatTimer(float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}

