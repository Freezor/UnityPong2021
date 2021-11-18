using System;
using GameWorld;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public string sceneName;

        private void Awake()
        {
            ClickScript = GetComponent<ClickSound>();
        }

        private ClickSound ClickScript { get; set; }

        public void StartGameAgainstAI()
        {
            ClickScript.PlaySound();
            PlayerPrefs.SetString($"{nameof(OpponentType)}", OpponentType.AI.ToString());
            SceneManager.LoadScene(sceneName);
        }

        public void StartGameAgainstPlayer()
        {
            ClickScript.PlaySound();
            PlayerPrefs.SetString($"{nameof(OpponentType)}", OpponentType.Player.ToString());
            SceneManager.LoadScene(sceneName);
        }

        public void ExitGame()
        {
            ClickScript.PlaySound();
            Application.Quit();
        }
    }
}