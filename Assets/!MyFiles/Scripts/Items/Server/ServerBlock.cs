using MyScripts.Service;
using UnityEngine;

namespace MyScripts.Items.Server
{
    /// <summary>
    /// Класс оболочка для серверного блока 
    /// </summary>
    public class ServerBlock : MonoBehaviour
    {
        public void TakeMistake()
        {
            StagesManager.Instance.PlayTips(0);
            ErrorChecker.Add();
        }
    }
}