using System.Collections.Generic;
using UnityEngine;
using MyScripts.Base;

namespace MyScripts.Service
{
    /// <summary>
    ///  ласс управл€ющий всеми лампами
    /// </summary>
    public class BulbManager : MonoBehaviour
    {
        [SerializeField] private List<Bulb> bulbs;

        private void Awake()
        {
            bulbs = new List<Bulb>(FindObjectsOfType<Bulb>());
        }

        public void ToSwitchAllBulb()
        {
            foreach (Bulb bulb in bulbs)
            {
                bulb.ToSwitch();
            }
        }

    }
}