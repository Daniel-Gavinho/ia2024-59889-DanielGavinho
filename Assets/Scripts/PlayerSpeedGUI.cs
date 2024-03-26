using UnityEngine;

public class PlayerSpeedGUI : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    private GUIStyle guiStyle = new GUIStyle();

    private void OnGUI()
    {
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(10, 10, 200, 50), "Speed: " + playerRigidbody.velocity.magnitude.ToString("F2"), guiStyle);
    }
}

