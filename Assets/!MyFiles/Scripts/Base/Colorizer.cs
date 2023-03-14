using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyScripts.Base
{
    [RequireComponent(typeof(Image))]
    public class Colorizer : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private List<Color> colors = new List<Color>();

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void ChangeColor(int index)
        {
            if ((index < 0) || (index >= colors.Count)) return;

            image.color = colors[index];
        }
        public void ChangeColor(Color color) => image.color = color;
    }
}