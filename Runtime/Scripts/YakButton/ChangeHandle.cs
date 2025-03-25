using UnityEngine;

namespace com.yak.ui
{
    public abstract class ChangeHandle : MonoBehaviour
    {
        public abstract void ToOriginal();
        public abstract void ToHover();
        public abstract void ToPressed();
    }
}
