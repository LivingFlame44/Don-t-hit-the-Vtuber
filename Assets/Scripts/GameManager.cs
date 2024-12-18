using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PauseManager pauseManager;

    public Sprite[] holoPics;
    public List<GameObject> inactiveMembers = new List<GameObject>();
    public List<GameObject> activeMembers = new List<GameObject>();

    public List<Sprite> inactivePics = new List<Sprite>();
    public List<Sprite> activePics = new List<Sprite>();

    public Transform[] spawnPos;
    public int maxMemberNum;
    public GameObject memberPrefab;

    public float spawnTimer, spawnMaxTime;
    public float timer;

    public TextMeshProUGUI scoreTxt;
    public int score;
    public enum GameState
    {
        RUNNING,
        PAUSED,
        GAMEOVER
    }

    public GameState gameState;
    // Start is called bSfore the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        foreach (Sprite s in holoPics)
        {
            inactivePics.Add(s);
        }

        SpawnMember(1);
        SpawnMember(2);
    }

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = score.ToString();

        if (timer > 60)
        {
            spawnMaxTime = 3.5f;
        }
        else if (timer > 30)
        {
            spawnMaxTime = 4f;
        }
        else if (timer > 15)
        {
            spawnMaxTime = 4.5f;
        }
        else
        {
            spawnMaxTime = 5f;
        }

        switch (gameState)
        {
            case GameState.RUNNING:
                if (spawnTimer > spawnMaxTime)
                {
                    SpawnMember(1);
                    SpawnMember(2);
                    spawnTimer = 0;
                }
                spawnTimer += Time.deltaTime;
                break;

            case GameState.PAUSED:

                break;
            case GameState.GAMEOVER:

                break;
        }

        timer += Time.deltaTime;
    }
    public int spawnNum;
    public void SpawnMember(int num)
    {
        GameObject spawnedMem;

        if (num == 1)
        {
            spawnNum = Random.Range(0, 4);
        }
        else
        {
            spawnNum = Random.Range(4,8);
        }
        

        if(inactiveMembers.Count > 0)
        {
            spawnedMem = inactiveMembers[0];
            PullFromPool(spawnedMem);
            if (spawnNum > 3)
            {
                spawnedMem.GetComponent<Member>().goLeft = false;
            }
            else
            {
                spawnedMem.GetComponent<Member>().goLeft = true;
            }
            spawnedMem.SetActive(true);
            spawnedMem.transform.position = new Vector3(spawnPos[spawnNum].position.x, spawnPos[spawnNum].position.y, spawnPos[spawnNum].position.z);
            
        }

        else
        {
            spawnedMem = Instantiate(memberPrefab, new Vector3(spawnPos[spawnNum].position.x, spawnPos[spawnNum].position.y, spawnPos[spawnNum].position.z), Quaternion.identity);
            spawnedMem.transform.Rotate(90,0,0);
            activeMembers.Add(spawnedMem);
            if (spawnNum > 3)
            {
                spawnedMem.GetComponent<Member>().goLeft = false;
            }
            else
            {
                spawnedMem.GetComponent<Member>().goLeft = true;
            }
        }

    }


    public void BackToPool(GameObject obj)
    {
        activeMembers.Remove(obj);
        inactiveMembers.Add(obj);
    }

    public void PullFromPool(GameObject obj)
    {
        inactiveMembers.Remove(obj);
        activeMembers.Add(obj);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        CheckHighScore();
        pauseManager.gameOverPanel.SetActive(true);
        AssignScores();
    }

    public void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

    public void AssignScores()
    {
        pauseManager.scoreTxt.text = score.ToString();
        pauseManager.highscoreTxt.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
    }
}
