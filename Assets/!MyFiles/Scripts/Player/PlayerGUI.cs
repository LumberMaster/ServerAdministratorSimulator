using TMPro;
using UnityEngine;

namespace MyScripts.Player
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

        public void UpdateObjectHelperText(string text)
        {
            objectHelper.text = text;
        }
    }
}