using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagementScript : MonoBehaviour {

    void StartGame()
    {
        GameManagementScript.instance.SetCurrentApplicationState("LEVEL1PLAY");
    }

    void MainMenu()
    {
        GameManagementScript.instance.SetCurrentApplicationState("MAINMENU");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
