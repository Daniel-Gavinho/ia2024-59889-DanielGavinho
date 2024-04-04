using UnityEngine;

public class GUIScript : MonoBehaviour
{
    public Texture2D dotTexture;
    public int dotSize = 10;

    void OnGUI()
    {
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Rect dotRect = new Rect(center.x - dotSize / 2, center.y - dotSize / 2, dotSize, dotSize);
        GUI.DrawTexture(dotRect, dotTexture);
    }
}

