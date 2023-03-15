using TMPro;
using UnityEngine;

namespace ServerAdministratorSimulator.Player
{
	/// <summary>
	/// Класс графического интерфейса игрока
	/// </summary>
	public class PlayerGUI : MonoBehaviour
	{
		private static PlayerGUI instance;
		public static PlayerGUI Instance
		{
			get { return instance; }
			private set { instance = value; }
		}

		private void Awake()
		{
			Instance = this;
		}

		[SerializeField] private TMP_Text objectHelper;
		[SerializeField] private TMP_Text objectInHandHelper;

		public void UpdateObjectHelperText(string text)
		{
			objectHelper.text = text;
		}
		public void UpdateObjectInHandHelperText(string text)
		{
			objectInHandHelper.text = text;
		}
	}
}