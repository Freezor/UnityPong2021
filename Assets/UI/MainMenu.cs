using GameWorld;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public string sceneName;

        public void StartGameAgainstAI()
        {
            PlayerPrefs.SetString($"{nameof(OpponentType)}", OpponentType.AI.ToString());
            SceneManager.LoadScene(sceneName);
        }

        public void StartGameAgainstPlayer()
        {
            PlayerPrefs.SetString($"{nameof(OpponentType)}", OpponentType.Player.ToString());
            SceneManager.LoadScene(sceneName);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}