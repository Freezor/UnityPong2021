using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOverMenu : MonoBehaviour
    {
        public string sceneName;

        private void Awake()
        {
            ClickScript = GetComponent<ClickSound>();
        }

        private ClickSound ClickScript { get; set; }

        public void RestartGame()
        {
            ClickScript.PlaySound();
            SceneManager.LoadScene(sceneName);
        }

        public void BackToMainMenu()
        {
            ClickScript.PlaySound();
            SceneManager.LoadScene(sceneName);
        }
    }
}