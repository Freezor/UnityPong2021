using UnityEngine;
using UnityEngine.Serialization;

namespace Racket
{
    public class MoveRacket : MonoBehaviour
    {
        public float bodySpeed = 30;
        public string axis = "Vertical";

        void FixedUpdate()
        {
            var verticalAxisInput = Input.GetAxisRaw(axis);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, verticalAxisInput) * bodySpeed;
        }
    }
}