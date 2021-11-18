using System;
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

        public AudioSource audioSource { get; set; }

        private void Start()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(audioClip);
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }

        public void Resume()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void MainMenu(string sceneName)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(sceneName);
        }
    }
}