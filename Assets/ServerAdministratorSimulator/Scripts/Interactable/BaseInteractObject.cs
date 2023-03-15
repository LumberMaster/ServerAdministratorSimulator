using ServerAdministratorSimulator.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace ServerAdministratorSimulator.Interactable
{
	/// <summary>
	/// Базовый класс интерактивных объектов
	/// </summary>
	public class BaseInteractObject : MonoBehaviour, IInteractable, IVisualizable
	{
		[SerializeField] private UnityEvent OnInteract = new UnityEvent();
		[SerializeField] private UnityEvent OnVisualization = new UnityEvent();
		[SerializeField] private UnityEvent OnUnVisualization = new UnityEvent();
		[SerializeField] private bool isVisualization;

		[Header("Audio")]
		[SerializeField] private AudioSource source;
		[SerializeField] private AudioClip audioClipInteract;

		public bool IsVisualization
		{
			get { return isVisualization; }
			set { isVisualization = value; }
		}

		[SerializeField] private string description;
		public string Description
		{
			get { return description; }
		}


		public virtual void Interact()
		{
			if (IsVisualization)
			{
				OnInteract.Invoke();
				if (source)
				{
					source.Stop();
					source.PlayOneShot(audioClipInteract);
				}
			}
		}

		public virtual void Visualization()
		{
			OnVisualization.Invoke();
			IsVisualization = true;
		}

		public virtual void UnVisualization()
		{
			OnUnVisualization.Invoke();
			IsVisualization = false;
		}
	}
}