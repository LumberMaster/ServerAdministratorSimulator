using MyScripts.Interactable;
using UnityEngine;


namespace MyScripts.Player
{
    /// <summary>
    /// Класс отвечающий за обнаружение объектов в направлении взгляда
    /// </summary>
    public class ObjectDetector : MonoBehaviour
    {

        [SerializeField] private bool isEnable;
        [SerializeField] private Collider trigger;
        public bool IsEnable
        {
            get { return isEnable; }
            private set
            {
                isEnable = value;

                if (isEnable) trigger.enabled = true;
                else
                {
                    lastCollider = null;
                    trigger.enabled = false;
                }

            }
        }

        [SerializeField] private Collider lastCollider;

        private void Awake()
        {
            trigger = gameObject.GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {

            BaseInteractObject interactObject = other.GetComponent<BaseInteractObject>();
            if (interactObject == null) return;

            if (lastCollider) lastCollider.GetComponent<BaseInteractObject>().UnVisualization();
            lastCollider = other;

            PlayerGUI.Instance.UpdateObjectHelperText(interactObject.Description);
            interactObject.Visualization();

        }

        private void OnTriggerExit(Collider other)
        {

            BaseInteractObject interactObject = other.GetComponent<BaseInteractObject>();
            if (interactObject == null) return;

            if (lastCollider == other) lastCollider = null;


            if (lastCollider == null) PlayerGUI.Instance.UpdateObjectHelperText("");
            interactObject.UnVisualization();
        }

        public void ToSwitch()
        {
            if (IsEnable) ToDisable();
            else ToEnable();
        }

        public void ToEnable() => IsEnable = true;
        public void ToDisable() => IsEnable = false;

    }
}