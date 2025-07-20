using UnityEngine;

namespace com.yak.ui
{
    [RequireComponent(typeof(TMPro.TMP_Text))]
    public class TextEffect : SmartButtonEffect
    {
        [SerializeField] private string hoverText = "";
        [SerializeField] private string pressedText = "";
    
        private TMPro.TMP_Text _text;
        private string _originalText;
        private string _currentText;

        private void Awake()
        {
            _text = GetComponent<TMPro.TMP_Text>();
            _originalText = _text.text;
            _currentText = _originalText;
        }

        public override void ToUntouched()
        {
            _currentText = _originalText;
            _text.text = _currentText;
        }

        public override void ToHover()
        {
            _currentText = hoverText;
            _text.text = _currentText;
        }

        public override void ToPressed()
        {
            _currentText = pressedText;
            _text.text = _currentText;
        }
    }
}