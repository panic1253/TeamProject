using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {

    string button;

    public void UIEventControll(string button)
    {
        Debug.Log(button);
        switch(button)
        {
            case "Start":
                SceneManager.LoadScene("stage sellect");
                break;
            
            case "OptionButton":
                SceneManager.LoadScene("option");
                break;
            case "ExitButton":
                SceneManager.LoadScene("stage sellect");
                break;
            case "stage 1button":
                SceneManager.LoadScene("Stage1");
                break;
        }

    }
   public void Stage()
    {
        gameObject.SetActive (true);
    }
}
