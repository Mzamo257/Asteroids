﻿using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour {
    #region Public Attributes
    public Text pause;
	public Text pauseTitle;
	public Text resume;
	public Text mainMenu;
	public Text exit;
	public Text mainMenuLose;
	public Text mainMenuWin;
	public Text nextLevel;
	public Text retry;
    #endregion

    #region Private Attributes
    private GameManagerSingleton gameManagerSingleton;
    #endregion

    #region MonoDevelop Methods
    // Use this for initialization
    void Start ()
    {
        gameManagerSingleton = GameManagerSingleton.instance;
		MenuPauseLanguage ();
	}
    #endregion

    #region User Methods
    private void MenuPauseLanguage()
	{
		switch (gameManagerSingleton.GetLanguage())
		{
		case "English":
			pause.text = "Pause";
			pauseTitle.text = "PAUSE";
			resume.text = "Resume";
			mainMenu.text = "Main Menu";
			exit.text = "Exit Game";
			mainMenuLose.text = "Main Menu";
			mainMenuWin.text = "Main Menu";
			nextLevel.text = "Next Level";
			retry.text = "Retry";
			break;
		case "Español":
			pause.text = "Pausa";
			pauseTitle.text = "PAUSA";
			resume.text = "Reanudar";
			mainMenu.text = "Menú Principal";
			exit.text = "Salir del juego";
			mainMenuLose.text = "Menú Principal";
			mainMenuWin.text = "Menú Principal";
			nextLevel.text = "Siguiente nivel";
			retry.text = "Reintentar";
			break;
		case "Français":
			pause.text = "Pause";
			pauseTitle.text = "PAUSE";
			resume.text = "Reprendre";
			mainMenu.text = "Menu Principal";
			exit.text = "Jeu de Sortie";
			mainMenuLose.text = "Menu Principal";
			mainMenuWin.text = "Menu Principal";
			nextLevel.text = "Next Level";
			retry.text = "Retry";
			break;
		}
	}
    #endregion
}
