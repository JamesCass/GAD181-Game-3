using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;

public class MenuFunctionality : MonoBehaviour
{
    public TextMeshProUGUI LoseText;

    public GameObject player1;
    public GameObject player2;

    private bool isGamePaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }
    private void Start()
    {
        LoseText.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;

        //Reset player position
        if (player1 != null)
        {
            player1.transform.position = new Vector2(-5f, 0f);
        }
        if (player2 != null)
        {
            player2.transform.position = new Vector2(5f, 0f);
        }
        

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        //Add return to menu functionality once menu added.
    }

    public void DisplayLoseText(string text)
    {
        LoseText.text = text;
        LoseText.gameObject.SetActive(true);
    }

    public void PlayerDestroyed(string playerName)
    {
        DisplayLoseText(playerName + " loses!");
        isGamePaused = true;
        Time.timeScale = 0f;
    }
    public bool IsGamePaused()
    {
        return isGamePaused;
    }
    

    
}
