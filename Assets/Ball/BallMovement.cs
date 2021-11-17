using System.Collections;
using GameWorld;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ball
{
    public class BallMovement : MonoBehaviour
    {
        private const int InitialSpeed = 30;

        public float speed = InitialSpeed;
        public int speedIncreaseTick = 1;
        public int secondsAfterBallSpeedIncreases = 3;
        public int delaySecondsAfterScoring = 2;
        
        public bool GameRunning { get; set; }
        private Rigidbody2D Ball { get; set; }

        void Start()
        {
            GameRunning = true;
            Ball = GetComponent<Rigidbody2D>();
            StartBall();
            StartCoroutine(ExampleCoroutine());
        }

        IEnumerator ExampleCoroutine()
        {
            while (GameRunning)
            {
                yield return new WaitForSeconds(secondsAfterBallSpeedIncreases);

                speed += speedIncreaseTick;

                Debug.Log($"BallSpeed increased to {speed}");
            }
        }

        private void StartBall()
        {
            float rand = Random.Range(0, 2);
            if (rand < 1)
            {
                Ball.velocity = Vector2.right * speed;
            }
            else
            {
                Ball.velocity = Vector2.left * speed;
            }
        }

        void ResetBall()
        {
            speed = InitialSpeed;

            float rand = Random.Range(-12, 12);
            Ball.velocity = new Vector2(0, 0);
            transform.position = new Vector2(0, rand);
        }

        void OnCollisionEnter2D(Collision2D collision2D)
        {
            // Note: 'col' holds the collision information. If the
            // Ball collided with a racket, then:
            //   col.gameObject is the racket
            //   col.transform.position is the racket's position
            //   col.collider is the racket's collider

            switch (collision2D.gameObject.name)
            {
                case "RacketLeft":
                {
                    CalculateHitFactor(collision2D, 1);
                    break;
                }
                case "RacketRight":
                {
                    CalculateHitFactor(collision2D, -1);
                    break;
                }
                case "WallLeft":
                {
                    StartCoroutine(ScoreAndRestart("Player2"));
                    break;
                }
                case "WallRight":
                {
                    StartCoroutine(ScoreAndRestart("Player1"));
                    break;
                }
            }
        }

        private void CalculateHitFactor(Collision2D col, int xAxisValue)
        {
            // Calculate hit Factor
            var y = HitFactor(transform.position,
                col.transform.position,
                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            var dir = new Vector2(xAxisValue, y).normalized;

            // Set Velocity with dir * speed
            Ball.velocity = dir * speed;
        }

        private static float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
        {
            // ascii art:
            // ||  1 <- at the top of the racket
            // ||
            // ||  0 <- at the middle of the racket
            // ||
            // || -1 <- at the bottom of the racket
            return (ballPos.y - racketPos.y) / racketHeight;
        }
        
        
        IEnumerator ScoreAndRestart(string playerName)
        {
            GameManager.Score(playerName);
            ResetBall();
            yield return new WaitForSeconds(delaySecondsAfterScoring);
            StartBall();
        }
    }
}