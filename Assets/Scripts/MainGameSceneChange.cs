using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameSceneChange : MonoBehaviour
{
    // Function to load a new scene
    public void SwitchToSceneByIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
