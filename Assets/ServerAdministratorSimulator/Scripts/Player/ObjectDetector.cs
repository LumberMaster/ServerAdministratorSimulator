using ServerAdministratorSimulator.Interactable;
using ServerAdministratorSimulator.Interfaces;
using UnityEngine;


namespace ServerAdministratorSimulator.Player
{
	/// <summary>
	/// Класс отвечающий за обнаружение объектов в направлении взгляда
	/// </summary>
	public class ObjectDetector : AbstractSwitcher
	{
		[SerializeField] private GameObject cameraTarget;
		[SerializeField] private Collider lastCollider;
		[SerializeField] private LayerMask layerMask;
		private void Start() 
		{
			cameraTarget = PlayerController.Instance.cameraTarget;
		}

		private void FixedUpdate()
		{
			Vector3 direct = cameraTarget.transform.TransformDirection(Vector3.forward);
			if (lastCollider) {
				lastCollider.GetComponent<IVisualizable>().UnVisualization();
				PlayerGUI.Instance.UpdateObjectHelperText("");
				lastCollider = null;
			}
			if (Physics.Raycast(cameraTarget.transform.position, direct, out RaycastHit hit, 3f, layerMask))
			{

				IVisualizable iVisualizable = hit.collider.GetComponent<IVisualizable>();

				if (iVisualizable != null) {
					iVisualizable.Visualization();
					lastCollider = hit.collider;
				}

				BaseInteractObject interactObject = hit.collider.GetComponent<BaseInteractObject>();
				if(interactObject) PlayerGUI.Instance.UpdateObjectHelperText(interactObject.Description);
			}
		}

	}
}