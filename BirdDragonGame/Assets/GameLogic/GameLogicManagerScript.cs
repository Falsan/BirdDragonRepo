using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicManagerScript : MonoBehaviour {

    public static GameLogicManagerScript instance;
    int gameScore;
    public GameObject scoreCounter;

    void Start()
    {
        gameScore = 0;

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		if(gameScore == 5)
        {
            GameManagementScript.instance.SetCurrentApplicationState("WINGAME");
        }
	}

    public void IncreaseScore()
    {
        gameScore++;
        scoreCounter.GetComponent<Text>().text = "Score: " + gameScore;
    }
}
