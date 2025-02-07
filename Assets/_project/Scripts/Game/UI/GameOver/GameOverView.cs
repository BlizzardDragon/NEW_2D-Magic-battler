using TMPro;
using UnityEngine;

namespace _project.Scripts.Game.UI.GameOver
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void RenderText(string text)
        {
            _text.text = text;
        }
    }
}