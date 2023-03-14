using UnityEngine;
using UnityEngine.Events;

namespace MyScripts.Base
{
    public class Bulb : MonoBehaviour
    {
        private Light lightComponent;
        public Light LightComponent
        {
            get { return lightComponent; }
            private set { lightComponent = value; }
        }

        public UnityEvent OnEnable = new UnityEvent();
        public UnityEvent OnDisable = new UnityEvent();


        private void Awake()
        {
            LightComponent = gameObject.GetComponent<Light>();
        }

        public void ToSwitch()
        {
            if (lightComponent.enabled) ToDisable();
            else ToEnable();
        }
        public void ToEnable()
        {
            OnEnable.Invoke();
            LightComponent.enabled = true;
        }

        public void ToDisable()
        {
            OnDisable.Invoke();
            LightComponent.enabled = false;
        }
    }
}