using UnityEngine;
using UnityEngine.UI;

public class Victory : MonoBehaviour {
    public Text buttonMainMenu;
    private GameManagerSingleton gameManagerSingleton;
    private string scoreText;

    // Use this for initialization
    void Start () {
        gameManagerSingleton = GameManagerSingleton.instance;
        Language();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height * 3 / 5, 200, Screen.height * 1 / 5),
            scoreText +": " + GameManagerSingleton.instance.CurrentScore);
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
                scoreText = "Puntuación final";
                break;
            case "Français":
                buttonMainMenu.text = "Menu Principal";
                scoreText = "Final score";
                break;
        }
    }
}
