using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	#region Public Attributes
	public Dropdown language;
	public Toggle sound;
	public Slider volumeValue;

	public Text play;
	public Text cont;
	public Text exit;
	public Text lang;
	public Text music;
	public Text settings;
	public Text credits;
	public Text volume;
	public Text back;
	public Transform spaceship;
	public Transform center;

	public float spaceshipVelocity;
	#endregion

	#region Private Attributes
    GameManagerSingleton gameManagerSingleton;
	#endregion

	#region typedef
	#endregion

	#region Properties
	#endregion

	// Use this for initialization
	void Start () 
	{
        gameManagerSingleton = GameManagerSingleton.GetInstance();
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

        //
        Time.timeScale = 1.0f;
	}

	void Update()
	{
		spaceship.RotateAround (center.position, Vector3.down, spaceshipVelocity*Time.deltaTime); 
	}

	public void DropdownLanguage()
	{
		gameManagerSingleton.ChangeLanguage(language.value);
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
			gameManagerSingleton.SetSound(true);
		}
		else
		{
			gameManagerSingleton.SetSound(false);
		}
		//gmScript.SavePreferences ();
	}

	public void Slider()
	{
		gameManagerSingleton.Volume = volumeValue.value;
	}

	private void MenuEnglish()
	{
		play.text = "SURVIVAL";
		cont.text = "STORY";
		exit.text = "EXIT";
		lang.text = "Language";
		music.text = "Music/Sound";
		volume.text = "Volume";
		back.text = "Back";
		settings.text = "SETTINGS";
		credits.text = "CREDITS";
	}

	private void MenuSpanish()
	{
		play.text = "SUPERVIVENCIA";
		cont.text = "HISTORIA";
		exit.text = "SALIR DEL JUEGO";
		lang.text = "Idioma";
		music.text = "Musica/Sonido";
		volume.text = "Volumen";
		back.text = "Atrás";
		settings.text = "CONFIGURACION";
		credits.text = "CREDITOS";
	}

	private void MenuFrench()
	{
		play.text = "Survie";
		cont.text = "Récit";
		exit.text = "Jeu de Sortie";
		lang.text = "Langage";
		music.text = "Musique /Son";
		volume.text = "Volum";
		back.text = "Return";
		settings.text = "Configuration";
		credits.text = "Crédits";
	}
}
