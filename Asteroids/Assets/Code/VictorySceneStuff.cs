using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySceneStuff : MonoBehaviour {
    

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width/2 - 100, Screen.height * 3 / 5, 200, Screen.height * 1 / 5), 
            "Final Score: " + GameManagerSingleton.instance.CurrentScore);
    }
}
