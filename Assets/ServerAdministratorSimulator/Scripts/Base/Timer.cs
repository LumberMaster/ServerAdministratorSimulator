using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;



namespace ServerAdministratorSimulator.Base
{

	public class Timer : MonoBehaviour
	{

		[SerializeField] private bool isActive;
		public bool IsActive
		{
			get
			{
				return isActive;
			}

			private set
			{
				isActive = value;
				if (isActive == true) StartCoroutine(TimerUpdater());
				else StopCoroutine(TimerUpdater());
			}
		}

		[SerializeField] private float time;

		public float Time
		{
			get
			{
				return time;
			}

			private set
			{
				time = value;
			}
		}

		[SerializeField] private float timeUpdateIntensivity;

		[SerializeField] private TMP_Text timeOut;

		public UnityEvent<float> OnUpdate;

		private void Awake()
		{
			ToReset();
			OnUpdate = new UnityEvent<float>();
		}


		public void ToStart()
		{
			Time = 0;
			IsActive = true;
		}

		public void ToStop()
		{
			IsActive = false;
		}

		public void ToContinue()
		{
			IsActive = true;
		}
		public void ToReset()
		{
			Time = 0;
			IsActive = false;
		}

		private void TimeUpdate()
		{
			OnUpdate.Invoke(time);
			time += timeUpdateIntensivity;
			timeOut.text = GetTimeByString();

		}

		public string GetTimeByString()
		{
			if ((int)(time % 60) < 10) return string.Format("{0}:0{1}", (int)(time / 60), (int)(time % 60));
			else return string.Format("{0}:{1}", (int)(time / 60), (int)(time % 60));
		}

		private IEnumerator TimerUpdater()
		{
			if (isActive)
			{
				TimeUpdate();
				yield return new WaitForSeconds(timeUpdateIntensivity);
				StartCoroutine(TimerUpdater());
			}
		}
	}
}
