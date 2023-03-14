using MyScripts.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyScripts.UI {
    public class Menu : MonoBehaviour
    {
        public void ChangeScene(string name) 
        {
            ScenesSwitcher.Instance.ChangeScene(name);
        }

        public void QuitApplication()
        {
            ScenesSwitcher.Instance.QuitApplication();
        }
    }

}