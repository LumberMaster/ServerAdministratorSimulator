using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ServerAdministratorSimulator.Base 
{
	public class TimeInZone : Timer
	{

		[SerializeField] private string nameTriggeredCollider;
		[SerializeField] private float timeInZone;


		[SerializeField] private UnityEvent OnEndTime = new UnityEvent();

		private void OnTriggerEnter(Collider other)
		{

			if (other.name != nameTriggeredCollider) return;
			ToStart();

			StartCoroutine(Checker());
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.name != nameTriggeredCollider) return;
			

			StopCoroutine(Checker());
			ToStop();
			ToReset();
		}

		private IEnumerator Checker()
		{
			if (IsActive)
			{
				if (Time >= timeInZone) 
				{
					OnEndTime.Invoke();
					StopCoroutine(Checker());
				}
				yield return new WaitForSeconds(TimeUpdateIntensivity);
				StartCoroutine(Checker());
			}
		}
	}

}