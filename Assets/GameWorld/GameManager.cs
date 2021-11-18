using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace GameWorld
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] AudioClip backgroundMusic;
        [SerializeField] [Range(0.0f, 1.0f)] float volume = 1;
        [SerializeField] int maxScore = 15;
        [SerializeField] string gameOverScreen = "";

        public static int Player1Score = 0;
        public static int Player2Score = 0;

        public static GameManager Instance { get; private set; }

        public static OpponentType OpponentType { get; set; }

        public AudioSource AudioSource { get; private set; }

        public void Awake()
        {
            Player1Score = 0;
            Player2Score = 0;

            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            AudioSource = gameObject.GetComponent<AudioSource>();
            InitializeOpponentType();
            InitializeBackgroundMusic();
        }

        private void InitializeBackgroundMusic()
        {
            if (backgroundMusic != null)
            {
                AudioSource.clip = backgroundMusic;
            }

            AudioSource.playOnAwake = false;
            AudioSource.loop = true;
            AudioSource.volume = volume;
            AudioSource.Play();
        }

        private static void InitializeOpponentType()
        {
            var opponentType = PlayerPrefs.GetString($"{nameof(OpponentType)}");
            if (string.IsNullOrEmpty(opponentType))
            {
                opponentType = OpponentType.AI.ToString();
            }

            Enum.TryParse(opponentType, out OpponentType opponentTypeEnum);
            OpponentType = opponentTypeEnum;
        }

        /// <summary>
        /// Add a score point to given player
        /// </summary>
        /// <param name="player">Name of the player who scored a point</param>
        public static void Score(string player)
        {
            if (player.IndexOf("Player1", StringComparison.CurrentCultureIgnoreCase) != 0)
            {
                Player1Score++;
            }
            else
            {
                Player2Score++;
            }

            Instance.CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            if (Player1Score < maxScore && Player2Score < maxScore)
            {
                return;
            }

            if (Player1Score < 15)
            {
                if (OpponentType == OpponentType.Player)
                {
                    PlayerPrefs.SetString("Winner", "Player1");
                }

                PlayerPrefs.SetString("Winner", "AI");
            }
            else
            {
                PlayerPrefs.SetString("Winner", "Player1");
            }

            Instance.AudioSource.mute = true;
            SceneManager.LoadScene(gameOverScreen);
        }


        public static int GetPlayerScore(string playerName)
        {
            return playerName.IndexOf("Player1", StringComparison.CurrentCultureIgnoreCase) != 0
                ? Player1Score
                : Player2Score;
        }
    }
}