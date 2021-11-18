using UnityEngine;

namespace UI
{
    public class ClickSound : MonoBehaviour
    {
        [SerializeField] AudioClip audioClip;

        public AudioSource AudioSource { get; set; }
    
        private void Start()
        {
            AudioSource = gameObject.GetComponent<AudioSource>();
            AudioSource.clip = audioClip;
        }

        public void PlaySound()
        {
            AudioSource.Play();
        }
    }
}