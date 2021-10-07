using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsEvents : MonoBehaviour {
	   
    public void StartGame()
    {
        SceneManager.LoadScene("Build");
    }
    public void Credit()
    {
		SceneManager.LoadScene("Credits");
    }       
}
