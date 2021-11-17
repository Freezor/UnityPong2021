using GameWorld;
using UnityEngine;
using UnityEngine.Serialization;

namespace Racket
{
    public class RacketAI : MonoBehaviour
    {
        public GameObject ball;
        public float speed = 30;
        public float lerpSpeed = 1f;
        private Rigidbody2D _rigidBody;
 
        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            if (GameManager.OpponentType == OpponentType.AI)
            {
                GetComponent<MoveRacket>().enabled = false;
            }
            else
            {
                enabled = false;
            }
        }
   
        void FixedUpdate () {
            if (ball.transform.position.y > transform.position.y)
            {
                if (_rigidBody.velocity.y < 0) _rigidBody.velocity = Vector2.zero;
                _rigidBody.velocity = Vector2.Lerp(_rigidBody.velocity, Vector2.up * speed, lerpSpeed * Time.deltaTime);
            }
            else if (ball.transform.position.y < transform.position.y)
            {
                if (_rigidBody.velocity.y > 0) _rigidBody.velocity = Vector2.zero;
                _rigidBody.velocity = Vector2.Lerp(_rigidBody.velocity, Vector2.down * speed, lerpSpeed * Time.deltaTime);
            }
            else
            {
                _rigidBody.velocity = Vector2.Lerp(_rigidBody.velocity, Vector2.zero * speed, lerpSpeed * Time.deltaTime);
            }
        }
    }
}
