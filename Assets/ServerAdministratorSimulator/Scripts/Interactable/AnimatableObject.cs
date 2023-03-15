using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ServerAdministratorSimulator.Interactable
{
	/// <summary>
	/// Класс интерактивных объектов, имеющих анимацию при взаимодействии
	/// </summary>
	public class AnimatableObject : BaseInteractObject
	{
		[SerializeField] private Animator animator;
		[SerializeField] private int currentAnimation = 0;
		[SerializeField] private List<TransitionAnimation> animations = new List<TransitionAnimation>(1);



		public override void Interact()
		{

			base.Interact();
			if (IsVisualization)
			{
				animator.SetTrigger(animations[currentAnimation].triggerName);
				animations[currentAnimation].startAnimation.Invoke();
				currentAnimation = animations[currentAnimation].nextAnimationIndex;
			}

		}

		[System.Serializable]
		public struct TransitionAnimation
		{
			public int nextAnimationIndex;
			public string triggerName;
			public UnityEvent startAnimation;

		}
	}
}