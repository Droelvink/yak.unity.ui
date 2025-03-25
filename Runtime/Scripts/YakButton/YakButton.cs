using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace com.yak.ui
{
    public class YakButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        private bool _hovering = false;
        private bool _pressing = false;

        public UnityEvent onClick = new();
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _hovering = true;
            ApplyHoverEffects(_pressing ? 2 : 1);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hovering = false;
            ApplyHoverEffects(_pressing ? 2 : 0);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressing = true;
            ApplyHoverEffects(2);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pressing = false;
            ApplyHoverEffects(_hovering ? 1 : 0);
            if (_hovering)
            {
                onClick?.Invoke();
            }
        }

        private void ApplyHoverEffects(int effect)
        {
            var effects = GetComponentsInChildren<ChangeHandle>();
            foreach (var changeHandle in effects)
            {
                switch (effect)
                {
                    default:
                        changeHandle.ToOriginal();
                        break;
                    case 1:
                        changeHandle.ToHover();
                        break;
                    case 2:
                        changeHandle.ToPressed();
                        break;
                }
            }
        }
    }
}