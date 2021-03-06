﻿using System.Collections;
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
	public Text backCredits;
	public Text settingsTitle;
	public Transform spaceship;
	public Transform center;

	public float spaceshipVelocity;
	#endregion

	#region Private Attributes
    GameManagerSingleton gameManagerSingleton;
    #endregion

    #region MonoDevelop Methods
    // Use this for initialization
    void Start () 
	{
        //Assign keep values
        gameManagerSingleton = GameManagerSingleton.instance;
		switch (gameManagerSingleton.GetLanguage())
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
		sound.isOn = gameManagerSingleton.Music;
		volumeValue.value = gameManagerSingleton.Volume;

        //Restore the time scale from the previous pause
        Time.timeScale = 1.0f;
	}

	void Update()
	{
        //Rotate the spaceship in the menu
		spaceship.RotateAround (center.position, Vector3.down, spaceshipVelocity*Time.deltaTime); 
	}
    #endregion

    #region UI Methods
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
	}

	public void Slider()
	{
		gameManagerSingleton.Volume = volumeValue.value;
	}
    #endregion

    #region User Methods
    private void MenuEnglish()
	{
		play.text = "SURVIVAL";
		cont.text = "STORY";
		exit.text = "EXIT";
		lang.text = "Language";
		music.text = "Music/Sound";
		volume.text = "Volume";
		back.text = "Back";
		backCredits.text = "Back";
		settings.text = "SETTINGS";
		settingsTitle.text = "SETTINGS";
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
		backCredits.text = "Atrás";
		settings.text = "CONFIGURACION";
		settingsTitle.text = "CONFIGURACION";
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
		backCredits.text = "Return";
		settings.text = "Configuration";
		settingsTitle.text = "Configuration";
		credits.text = "Crédits";
	}
    #endregion
}
