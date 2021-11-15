using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameWorld
{
    public class DisplayScore : MonoBehaviour
    {
        private TextMeshProUGUI _displayedText;

        [SerializeField] public string playerName = string.Empty;

        // Use this for initialization
        void Start()
        {
            _displayedText = gameObject.GetComponent<TextMeshProUGUI>();
            _displayedText.text = GameManager.GetPlayerScore(playerName).ToString();
        }

        // Update is called once per frame
        void Update()
        {
            _displayedText.text = GameManager.GetPlayerScore(playerName).ToString();
        }
    }
}