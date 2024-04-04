using UnityEngine;

public class GUIScript : MonoBehaviour
{
    public Texture2D dotTexture;
    public int dotSize = 10;
    private GameMasterScript gm;

    void Start()
    {
        gm = GameMasterScript.Instance;
    }

    void OnGUI()
    {
        GUIStyle timerStyle = new GUIStyle();
        timerStyle.fontSize = 32;
        timerStyle.normal.textColor = Color.white;
        timerStyle.alignment = TextAnchor.UpperRight;
        Vector2 topRight = new Vector2(Screen.width, 0);
        Rect timerRect = new Rect(topRight.x - 200, 0, 200, 50);
        GUI.Label(timerRect, "Time: " + FormatTimer(gm.Timer), timerStyle);
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Rect dotRect = new Rect(center.x - dotSize / 2, center.y - dotSize / 2, dotSize, dotSize);
        GUI.DrawTexture(dotRect, dotTexture);
    }

    string FormatTimer(float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        int milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}

