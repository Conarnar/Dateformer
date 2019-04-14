using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

//last edited - evan
public class SceneLoading : MonoBehaviour
{

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName(sceneName).buildIndex);
    }
    public void loadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void restartGame()
    {
        if(GameManager.singleton != null)
        {
            GameManager.singleton.restart(); 
        }
        loadScene(0);

    }


}
