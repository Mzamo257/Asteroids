using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour {
	public Text pause;
	public Text resume;
	public Text mainMenu;
	public Text exit;

	// private GameObject gameManager;
	// private GameManager gmScript;
    private GameManagerSingleton gameManagerSingleton;

    // Use this for initialization
    void Start () {
        // gameManager = GameObject.Find ("GameManager");
        // gmScript = gameManager.GetComponent<GameManager> ();
        gameManagerSingleton = GameManagerSingleton.GetInstance();
		MenuPauseLanguage ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void MenuPauseLanguage()
	{
		switch (gameManagerSingleton.GetLanguage())
		{
		case "English":
			pause.text = "Pause";
			resume.text = "Resume";
			mainMenu.text = "Main Menu";
			exit.text = "Exit Game";
			break;
		case "Español":
			pause.text = "Pausa";
			resume.text = "Reanudar";
			mainMenu.text = "Menú Principal";
			exit.text = "Salir del juego";
			break;
		case "Français":
			pause.text = "Pause";
			resume.text = "Reprendre";
			mainMenu.text = "Menu Principal";
			exit.text = "Jeu de Sortie";
			break;
		}
	}
}
