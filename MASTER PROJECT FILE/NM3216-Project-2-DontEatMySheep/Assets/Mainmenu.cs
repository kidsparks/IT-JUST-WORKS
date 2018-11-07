using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour {

    public void PlayGame ()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitGame ()
    {
        Debug.Log("Quit"); //this is to show that the game quit//

            Application.Quit();
    }
}


