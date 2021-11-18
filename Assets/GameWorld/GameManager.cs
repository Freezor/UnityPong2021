using System;
using UnityEngine;

namespace GameWorld
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] AudioClip backgroundMusic;
        [SerializeField] [Range(0.0f, 1.0f)] float _volume = 1;

        public static int Player1Score = 0;
        public static int Player2Score = 0;

        public static GameManager Instance { get; private set; }

        public static OpponentType OpponentType { get; set; }

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
        }

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

            InitializeOpponentType();
            InitializeBackgroundMusic();
        }

        private void InitializeBackgroundMusic()
        {
            if (backgroundMusic != null)
            {
                _audioSource.clip = backgroundMusic;
            }

            _audioSource.playOnAwake = false;
            _audioSource.loop = true;
            _audioSource.volume = _volume;
            _audioSource.Play();
        }

        private static void InitializeOpponentType()
        {
            var opponentType = PlayerPrefs.GetString($"{nameof(OpponentType)}");
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
        }

        public static int GetPlayerScore(string playerName)
        {
            return playerName.IndexOf("Player1", StringComparison.CurrentCultureIgnoreCase) != 0
                ? Player1Score
                : Player2Score;
        }
    }
}