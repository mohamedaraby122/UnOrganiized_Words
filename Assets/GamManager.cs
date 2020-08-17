using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamManager : MonoBehaviour
{
    public Transform UnArrangedParent;
    public Transform ArrangedParent;
    public int currentWordNumber = 0;
    public int currentLevel = 0;
    public GameObject WinPanel;
    public GameObject GameOverPanel;
    float tempTime = 0.0f;
    public Image CurrentImage;
    public Sprite[] LevelsImages;
    public int maxLevel = 0;
    /*****************************************************************/
    public GameObject[] WordslevelsPanel;
    /*****************************************************************/
    public Text timerText;
    public Text winnerText;
    public string[] finalSentens;
    public Text scoreText;
    public int score = 0;
    public CanvasGroup _MianBoard;
    /*****************************************************************/
    public AudioSource audioSource;
    public AudioClip clickClip;
    public AudioClip winClip;
    public AudioClip LoseClip;

    private static GamManager _instance;

        public static GamManager Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
    
    private void Start()
    {
        currentWordNumber = 0;
        scoreText.text = score + " Points";
        tempTime = timeLeft;
        // currentLevel = PlayerPrefs.GetInt("Level");
        if(maxLevel < currentLevel || LevelsImages.Length-1< currentLevel)
        {
            currentLevel = 0;
            print("hhh");
        }
       // HideAllTheWordsAndShowCurrentWords();
       // CurrentImage.sprite = LevelsImages[currentLevel];
       // WordslevelsPanel[currentLevel].SetActive(true);
        ArrangedParent = WordslevelsPanel[currentLevel].transform.GetChild(0);
        UnArrangedParent = WordslevelsPanel[currentLevel].transform.GetChild(1);
    }
    public void OnClicImage(GameObject img)
    {
       
        if (!CheckIsHeIsRight(img))
        {
            //He Is un Correct
            GameOver();
            return;
        }
        audioSource.clip = clickClip;
        audioSource.Play();
        if (img.transform.parent == ArrangedParent)
        {
            img.transform.parent = UnArrangedParent.transform;
        }
        else if(img.transform.parent == UnArrangedParent)
        {
            img.transform.parent = ArrangedParent.transform;

        }
        currentWordNumber++;
        if (CheckIfPlayerWin())
        {
            WinPanl();
        }
    }


    public bool CheckIsHeIsRight(GameObject img)
    {
        if (currentWordNumber == img.gameObject.GetComponent<WordNumber>().number)
        {
            return true;
        }
        return false;
    }
    public bool CheckIfPlayerWin()
    {
        if (UnArrangedParent.childCount <= 0)
        {
            return true;
        }
        return false;
    }

   public float timeLeft = 30.0f;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            GameOver();
        }
        timerText.text = (int)timeLeft + " Seconds";
    }

    public void GameOver()
    {
        audioSource.clip = LoseClip;
        audioSource.Play();
        score = 0;
        scoreText.text = score + " Points";
        GameOverPanel.SetActive(true);
        WinPanel.SetActive(false);
        ResetWordsInGameOver();
            timeLeft = tempTime + 9;
        CurrentImage.sprite = null;
        foreach (GameObject i in WordslevelsPanel)
        {
            i.SetActive(false);
        }
        currentWordNumber = 0;
    }
    public void WinPanl()
    {
        audioSource.clip = winClip;
        audioSource.Play();
        winnerText.text = finalSentens[currentLevel];
        WinPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        score++;
        scoreText.text = score + " Points";
        ResetWordsInGameOver();
        CurrentImage.sprite = null;
        foreach (GameObject i in WordslevelsPanel)
        {
            i.SetActive(false);
        }
        if (currentLevel == maxLevel)
        {
            //Reach the final Question (repeat ) 
            return;
        }
        // currentLevel++;
        // PlayerPrefs.SetInt("level", currentLevel);

        timeLeft = tempTime + 9;
      ////  HideAllTheWordsAndShowCurrentWords();
       // CurrentImage.sprite = LevelsImages[currentLevel];
        currentWordNumber = 0;


    }
   public void  ActivateMainBoardBlockRayCaster()
    {
        _MianBoard.GetComponent<CanvasGroup>().blocksRaycasts = true;

    }
    public void HideAllTheWordsAndShowCurrentWords()
    {
        foreach(GameObject i in WordslevelsPanel)
        {
            i.SetActive(false);
        }
        print(currentLevel);
        print(WordslevelsPanel[currentLevel].name);
        WordslevelsPanel[currentLevel].SetActive(true);
        ArrangedParent = WordslevelsPanel[currentLevel].transform.GetChild(0);
        UnArrangedParent = WordslevelsPanel[currentLevel].transform.GetChild(1);
    }
    public void ResetWordsInGameOver()
    {
        for (int i = currentWordNumber-1; i >= 0; i--)
        {
            ArrangedParent.GetChild(i).transform.SetParent(UnArrangedParent);
        }
    }

    public void SetCurrentLevel(int level)
    {
        print("Change Level");
        currentLevel = level;
        timeLeft = tempTime + 9;
        HideAllTheWordsAndShowCurrentWords();
        CurrentImage.sprite = LevelsImages[currentLevel];
        currentWordNumber = 0;
    }
}
