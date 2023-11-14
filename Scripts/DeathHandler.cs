using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        Debug.Log("HandleDeath");
        Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        gameOverCanvas = canvases.ToList().Find((Canvas c) => c.tag == "GameOverCanvas");
        
        Debug.Log(gameOverCanvas);
        Debug.Log(gameOverCanvas.tag);
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
