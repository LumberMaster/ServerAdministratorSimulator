using UnityEngine;
using UnityEngine.Events;

namespace ServerAdministratorSimulator.Base
{
    public class Bulb : AbstractSwitcher
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

        public override void ToSwitch()
        {
            if (lightComponent.enabled) ToDisable();
            else ToEnable();
        }
        public override void ToEnable()
        {
            OnEnable.Invoke();
            LightComponent.enabled = true;
        }

        public override void ToDisable()
        {
            OnDisable.Invoke();
            LightComponent.enabled = false;
        }
    }
}