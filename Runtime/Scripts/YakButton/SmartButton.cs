using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace com.yak.ui 
{
    public class SmartButton : Button
    {
#region Editor
#if UNITY_EDITOR
            [MenuItem("GameObject/UI/Smart Button", false, 0)]
            private static void CreateYakButton(MenuCommand menuCommand)
            {
                GameObject go = new GameObject("Smart Button");
                go.AddComponent<Image>();
                go.AddComponent<SmartButton>();
                GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
                Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
                Selection.activeObject = go;
            }
            protected override void OnValidate()
            {
                base.OnValidate();
                transition = Transition.None;
            }
#endif
#endregion
        
        private bool _hovering;
        private bool _pressing;

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            _hovering = true;
            ApplyEffects();
        }
        
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            _hovering = true;
            ApplyEffects();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            _hovering = false;
            ApplyEffects();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            _hovering = false;
            ApplyEffects();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _pressing = true;
            ApplyEffects();
        }
        
        public override void OnPointerUp(PointerEventData eventData)
        {
            _pressing = false;
            ApplyEffects();
        }
        
        public override void OnSubmit(BaseEventData eventData)
        {
            base.OnSubmit(eventData);
            _pressing = true;
            ApplyEffects();
            StartCoroutine(ControllerPressEvent());
        }

        private IEnumerator ControllerPressEvent()
        {
            yield return new WaitForEndOfFrame();
            _pressing = false;
            ApplyEffects();
        }

        private YakButtonState State => _pressing ? YakButtonState.Pressed : _hovering ? YakButtonState.Hovered : YakButtonState.Untouched;
        
        private void ApplyEffects()
        {
            var effects = GetComponentsInChildren<SmartButtonEffect>();
            foreach (var changeHandle in effects)
            {
                switch (State)
                {
                    default:
                    case YakButtonState.Untouched:
                        changeHandle.ToUntouched();
                        break;
                    case YakButtonState.Hovered:
                        changeHandle.ToHover();
                        break;
                    case YakButtonState.Pressed:
                        changeHandle.ToPressed();
                        break;
                }
            }
        }

        private enum YakButtonState
        {
            Untouched,
            Hovered,
            Pressed
        }
    }

}

