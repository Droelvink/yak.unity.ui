using System.Collections;
using UnityEngine;

namespace com.yak.ui
{
    [RequireComponent(typeof(TMPro.TMP_Text))]
    public class TextColorChange : ChangeHandle
    {
        [SerializeField] private Color hoverColor = new(1,1,1,1);
        [SerializeField] private Color pressedColor = new(0,0,0,1);
        [SerializeField] private float transitionDuration = 0.25f;
        
        private Coroutine _routine;
        private TMPro.TMP_Text _text;
        private Color _originalColor;
        private Color _currentColor;
        
        private void Awake()
        {
            _text = GetComponent<TMPro.TMP_Text>();
            _originalColor = _text.color;
            _currentColor = _originalColor;
        }
    
        public override void ToOriginal()
        {
            ClearRoutine();
            StartCoroutine(DoAction(_originalColor));
        }
    
        public override void ToHover()
        {
            ClearRoutine();
            StartCoroutine(DoAction(hoverColor));
        }
    
        public override void ToPressed()
        {
            ClearRoutine();
            StartCoroutine(DoAction(pressedColor));
        }
    
        private void ClearRoutine()
        {
            if (_routine == null) return;
            StopCoroutine(_routine);
            _routine = null;
        }
        
        private IEnumerator DoAction(Color to)
        {
            var timer = 0f;
            while (timer <= transitionDuration)
            {
                timer += Time.deltaTime;
                var r = Mathf.Lerp(_currentColor.r, to.r, timer / transitionDuration);
                var g = Mathf.Lerp(_currentColor.g, to.g, timer / transitionDuration);
                var b = Mathf.Lerp(_currentColor.b, to.b, timer / transitionDuration);
                _currentColor = new Color(r, g, b);
                _text.color = _currentColor;
                yield return null;
            }
            ClearRoutine();
            yield return null;
        }
    }
}