using System.Collections;
using com.yak.ui;
using UnityEngine;
using UnityEngine.UI;

namespace com.yak.ui
{
    [RequireComponent(typeof(Image))]
    public class ImageColorEffect : SmartButtonEffect
    {
        [SerializeField] private Color hoverColor = new(1,1,1,1);
        [SerializeField] private Color pressedColor = new(0,0,0,1);
        [SerializeField] private float transitionDuration = 0.25f;
        
        private Coroutine _routine;
        private Image _image;
        private Color _untouchedColor;
        private Color _currentColor;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            _untouchedColor = _image.color;
            _currentColor = _untouchedColor;
        }
    
        public override void ToUntouched()
        {
            ClearRoutine();
            StartCoroutine(DoAction(_untouchedColor));
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
                _image.color = _currentColor;
                yield return null;
            }
            ClearRoutine();
            yield return null;
        }
    }
}
