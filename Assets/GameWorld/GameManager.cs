using System;
using UnityEngine;

namespace GameWorld
{
    public class GameManager : MonoBehaviour
    {
        public static int Player1Score = 0;
        public static int Player2Score = 0;

        public static GameManager Instance { get; private set; }
    
        public static OpponentType OpponentType { get; private set; }

        public void Awake()
        {
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