using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    public void playGame()
    {
        Debug.Log("hi");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
