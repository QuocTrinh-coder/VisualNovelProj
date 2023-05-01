using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    // create a game ended flag which can change if endGame() is called
    bool gameHasEnded = false;

    // create a variable to hold the restartDelay value
    public float restartDelay = 0.5f;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    // A function that ends the game and restarts
    public void endGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");

            // Invoke is a function that calls another function with arguments of that function's name, and a certain delay
            Invoke("Restart", restartDelay);
        }
    }


    // Function to call when the game ends to restart the level
    void Restart()
    {
        // Using a scene manager object we can load the active scene using LoadScene() which takes a scene name, but here we just get the last active scene's name with GetActiveScene.name
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
