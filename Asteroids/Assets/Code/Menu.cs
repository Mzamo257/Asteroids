using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	#region Public Attributes
	public Dropdown language;
	public Toggle sound;

	public Text play;
	public Text cont;
	public Text exit;
	public Text lang;
	public Text music;
	public Text settings;
	public Text credits;
	#endregion

	#region Private Attributes
	GameObject gameManager;
	GameManager gmScript;
	#endregion

	#region typedef
	#endregion

	#region Properties
	#endregion

	// Use this for initialization
	void Start () 
	{
		gameManager = GameObject.Find ("GameManager");
		gmScript = gameManager.GetComponent<GameManager> ();
		/*if (language) 
		{		
			switch (gmScript.GetLanguage ()) 
			{
			case "English":
				language.value = 0;
				MenuEnglish ();
				break;
			case "Español":
				language.value = 1;
				MenuSpanish ();
				break;
			case "Français":
				language.value = 2;
				break;
			}
			MenuPauseLanguage();
		}
		if (sound) 
		{
			sound.isOn = gmScript.GetSound ();
		}*/
	}

	public void DropdownLanguage()
	{
		gmScript.ChangeLanguage(language.value);
		switch (language.value) 
		{
		case 0:
			MenuEnglish ();
			break;
		case 1:
			MenuSpanish ();
			break;
		case 2:
			MenuFrench ();
			break;
		}
		//gmScript.SavePreferences ();
	}

	public void ToggleSound()
	{
		if(sound.isOn)
		{
			gmScript.SetSound(true);
		}
		else
		{
			gmScript.SetSound(false);
		}
		//gmScript.SavePreferences ();
	}

	private void MenuEnglish()
	{
		play.text = "New Game";
		cont.text = "Continue";
		exit.text = "Exit Game";
		lang.text = "Language";
		music.text = "Music/Sound";
		settings.text = "Settings";
		credits.text = "Credits";
	}

	private void MenuSpanish()
	{
		play.text = "Nueva Partida";
		cont.text = "Continuar";
		exit.text = "Salir del juego";
		lang.text = "Idioma";
		music.text = "Musica/Sonido";
		settings.text = "Configuración";
		credits.text = "Créditos";
	}

	private void MenuFrench()
	{
		play.text = "Nouveau Jeu";
		cont.text = "Continuer";
		exit.text = "Jeu de Sortie";
		lang.text = "Langage";
		music.text = "Musique /Son";
		settings.text = "Configuration";
		credits.text = "Crédits";
	}

	private void MenuPauseLanguage()
	{
		Text resume = GameObject.Find("Resume").GetComponent<Text>();
		Text mainMenu = GameObject.Find("MainMenu").GetComponent<Text>();
		Text exit = GameObject.Find("Exit").GetComponent<Text>();

		switch (language.value)
		{
		case 0:
			resume.text = "Resume";
			resume.text = "Main Menu";
			resume.text = "Exit Game";
			break;
		case 1:
			resume.text = "Reanudar";
			resume.text = "Menú Principal";
			resume.text = "Salir del juego";
			break;
		case 2:
			resume.text = "Resume";
			resume.text = "Main Menu";
			resume.text = "Exit Game";
			break;
		}
	}
}
