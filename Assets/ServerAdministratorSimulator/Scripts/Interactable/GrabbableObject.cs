using ServerAdministratorSimulator.Player;
using UnityEngine;
using static Unity.VisualScripting.Member;

namespace ServerAdministratorSimulator.Interactable
{
	/// <summary>
	/// Класс интерактивных объектов, которые можно взять в руки
	/// </summary>
	public class GrabbableObject : BaseInteractObject
	{
		[SerializeField] private Vector3 positionObjectInHand;
		[SerializeField] private Quaternion rotationObjectInHand;
		[SerializeField] private Vector3 scaleObjectInHand;
		public override void Interact()
		{
			base.Interact();
			if (IsVisualization)
			{
				gameObject.GetComponent<Rigidbody>().isKinematic = true;
				PlayerController.Instance.ToGrab(gameObject, PlayerController.Hands.Right);
				gameObject.transform.localScale = scaleObjectInHand;
				gameObject.transform.localPosition = positionObjectInHand;
				gameObject.transform.localRotation = rotationObjectInHand;
			}
		}
	} 
}