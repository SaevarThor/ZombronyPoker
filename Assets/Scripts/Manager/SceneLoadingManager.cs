using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    public static void LoadNewScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName); 
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
