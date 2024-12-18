using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static BounceManager instance;

    public GameObject gameOverPanel, winPanel;
    public GameObject player;

    public Sprite[] vtuberPics;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearLevel()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }
}
