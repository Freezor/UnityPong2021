using System;
using GameWorld;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] AudioClip audioClip;

        private bool _gameIsPaused = false;

        public AudioSource AudioSource { get; set; }

        private void Start()
        {
            AudioSource = gameObject.GetComponent<AudioSource>();
            AudioSource.clip = audioClip;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape))
            {
                return;
            }

            _gameIsPaused = !_gameIsPaused;
            if (_gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public void Pause()
        {
            AudioSource.Play();
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }

        public void Resume()
        {
            AudioSource.Play();
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void MainMenu(string sceneName)
        {
            GameManager.Instance.AudioSource.mute = true;
            AudioSource.Play();
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(sceneName);
        }
    }
}