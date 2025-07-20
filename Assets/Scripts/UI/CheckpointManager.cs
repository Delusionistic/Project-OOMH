using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public Transform defaultSpawn;
    public static Transform currentCheckpoint;
    public GameObject player;

    public GameObject pauseMenuUI;
    public GameObject HUD;

    public bool hasCheckpoint = false;
    public bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (hasCheckpoint)
        {
            LoadCheckpoint();
        }
        else
        {
            SetDefaultCheckpoint();
            LoadCheckpoint();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            ResetCheckpoint();
        }

        if (Input.GetKeyDown("l"))
        {
            LoadCheckpoint();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void LoadCheckpoint()
    {
        Vector3 tempSpawn = new Vector3(PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"), PlayerPrefs.GetFloat("zPos"));
        player.transform.position = tempSpawn;
    }

    void ResetCheckpoint()
    {
        SetDefaultCheckpoint();
        hasCheckpoint = false;
    }

    void SetDefaultCheckpoint()
    {
        PlayerPrefs.SetFloat("xPos", defaultSpawn.position.x);
        PlayerPrefs.SetFloat("yPos", defaultSpawn.position.y);
        PlayerPrefs.SetFloat("zPos", defaultSpawn.position.z);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // Hide the pause menu UI
        HUD.SetActive(true);
        Time.timeScale = 1f;           // Resume game time
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor
        Cursor.visible = false;        // Hide the cursor
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);   // Show the pause menu UI
        HUD.SetActive(false);
        Time.timeScale = 0f;           // Freeze game time
        Cursor.lockState = CursorLockMode.None;    // Unlock the cursor
        Cursor.visible = true;         // Show the cursor
        isPaused = true;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;            // Ensure time is unpaused
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
