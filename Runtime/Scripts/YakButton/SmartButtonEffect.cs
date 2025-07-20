using UnityEngine;

namespace com.yak.ui
{
    public abstract class SmartButtonEffect : MonoBehaviour
    {
        public abstract void ToUntouched();
        public abstract void ToHover();
        public abstract void ToPressed();
    }
}
