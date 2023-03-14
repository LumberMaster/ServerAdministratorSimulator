using UnityEngine;
using UnityEngine.Events;

namespace MyScripts.Base
{
    public class Switcher : MonoBehaviour
    {
        [SerializeField] private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public UnityEvent OnEnable = new UnityEvent();
        public UnityEvent OnDisable = new UnityEvent();

        public void ToSwitch()
        {
            if (IsActive) ToDisable();
            else ToEnable();
        }
        public void ToEnable()
        {
            OnEnable.Invoke();
            IsActive = true;
        }

        public void ToDisable()
        {
            OnDisable.Invoke();
            IsActive = false;
        }
    }
}