using System.Collections;
using UnityEngine;

namespace com.yak.ui
{
    [RequireComponent(typeof(CanvasGroup))]
    public class OpacityEffect : SmartButtonEffect
    {
        [SerializeField] private float hoverOpacity = 0.5f;
        [SerializeField] private float pressedOpacity = 1f;
        [SerializeField] private float transitionDuration = 0.25f;
        
        private Coroutine _routine;
        private float _originalOpacity;
        private float _currentOpacity;
        
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _originalOpacity = _canvasGroup.alpha;
            _currentOpacity = _originalOpacity;
        }

        public override void ToUntouched()
        {
            ClearRoutine();
            StartCoroutine(DoAction(_originalOpacity));
        }

        public override void ToHover()
        {
            ClearRoutine();
            StartCoroutine(DoAction(hoverOpacity));
        }

        public override void ToPressed()
        {
            ClearRoutine();
            StartCoroutine(DoAction(pressedOpacity));
        }
        
        private void ClearRoutine()
        {
            if (_routine == null) return;
            StopCoroutine(_routine);
            _routine = null;
        }
        
        private IEnumerator DoAction(float to)
        {
            var timer = 0f;
            while (timer <= transitionDuration)
            {
                timer += Time.deltaTime;
                _currentOpacity = Mathf.Lerp(_currentOpacity, to, timer / transitionDuration);
                _canvasGroup.alpha = _currentOpacity;
                yield return null;
            }
            ClearRoutine();
            yield return null;
        }
    }
}
