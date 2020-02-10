using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameContoller : MonoBehaviour {

    public AudioSource[] music;
    public AudioSource gameOver;
    public AudioSource roar;
    public AudioSource yipee;
    public Gremlin gremlin;
    public Key key;
    public TextMesh guideText;
    public TextMesh titleText;
    public Timer timer;

    enum State {Paused, Playing, Menu};
    private State state;
    private AudioSource currentSong;

    private void Start()
    {
        currentSong = music[UnityEngine.Random.Range(0, music.Length)];
        ShowMenu();
    }

    private void Update() {
        OnPress(KeyCode.M,
            delegate () { currentSong.mute = !currentSong.mute; });
        OnPress(KeyCode.P, roar.Play);
   
        if (state == State.Menu)
        {
            OnEnter(StartGame);
        } else if (state == State.Paused)
        {
            OnPress(KeyCode.R, RestartGame);
            OnEnter(ResumeGame);
        } else if (state == State.Playing)
        {
            OnEnter(PauseGame);
            if (timer.Expired())
            {
                LoseGame();
            }
            if (!key)
            {
                WinGame();
            }
        }
	}

    private void OnEnter(Action action)
    {
        OnPress(KeyCode.Return, action);
    }

    private void OnPress(KeyCode keyCode, Action action)
    {
        if (Input.GetKeyDown(keyCode))
        {
            action();
        }
    }

    private void ShowMenu()
    {
        titleText.text = "Find The Bathroom Key!";
        string[] instructions = {
            "Press Enter to Start/Pause",
            "Move with the Directional Keys/ASWD",
            "Rotate with the Mouse",
            "Jump with the Spacebar",
            "Roar with P",
            "Toggle sound with M",
        };
        guideText.text = String.Join("\n", instructions);
        gremlin.Freeze();
        state = State.Menu;
    }

    private void StartGame()
    {
        state = State.Playing;
        currentSong.Play();
        BlankText();
        timer.Begin();
        gremlin.UnFreeze();
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        state = State.Playing;
    }
 
    private void ResumeGame()
    {
        BlankText();
        currentSong.Play();
        timer.Resume();
        gremlin.UnFreeze();
        state = State.Playing;
    }

    private void PauseGame()
    {
        state = State.Paused;
        currentSong.Pause();
        timer.Pause();
        gremlin.Freeze();
        titleText.text = "Paused";
        guideText.text = "Press Enter to Resume\nPress R to Restart";
    }

    private void WinGame()
    {
        PauseGame();
        yipee.Play();
        titleText.text = "You Won!!!";
        guideText.text = "Press R to play again";
    }

    private void LoseGame()
    {
        gremlin.Die();
        PauseGame();
        gameOver.Play();
        titleText.text = "Game Over!";
        guideText.text = "You Didn't Make It In Time!\n\nPress 'R' to Retry";
    }

    private void BlankText()
    {
        titleText.text = "";
        guideText.text = "";
    }
}
