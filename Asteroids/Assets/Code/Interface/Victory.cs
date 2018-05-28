using UnityEngine;
using UnityEngine.UI;

public class Victory : MonoBehaviour {
    #region Public Attributes
    public Text buttonMainMenu;
    public Texture panel;
    public GUIStyle levelStyle;
    #endregion

    #region Private Attributes
    private GameManagerSingleton gameManagerSingleton;
    private string scoreText;
    #endregion

    // Use this for initialization
    void Start () {
        gameManagerSingleton = GameManagerSingleton.instance;

        levelStyle.fontSize = (int)(Screen.height * 0.028f);
        Language();
    }

    private void OnGUI()
    {
        //Points panel
        GUI.DrawTexture(new Rect(Screen.width * 2.9f / 10, Screen.height * 5.5f / 10, Screen.width * 4.3f / 10, Screen.height * 1 / 10), panel, ScaleMode.StretchToFill);
        //Score texts
        GUI.TextField(new Rect(Screen.width * 3.5f / 10, Screen.height * 5.8f / 10, Screen.width * 4 / 10, Screen.height * 0.5f / 10), scoreText + ": " + gameManagerSingleton.CurrentScore, levelStyle);
    }

    void Language()
    {
        switch (gameManagerSingleton.GetLanguage())
        {
            case "English":
                buttonMainMenu.text = "Main Menu";
                scoreText = "Final Score";
                break;
            case "Español":
                buttonMainMenu.text = "Menú Principal";
                scoreText = "Puntuacion final";
                break;
            case "Français":
                buttonMainMenu.text = "Menu Principal";
                scoreText = "Final score";
                break;
        }
    }
}
