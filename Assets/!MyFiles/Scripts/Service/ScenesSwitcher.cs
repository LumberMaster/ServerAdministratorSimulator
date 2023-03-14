using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyScripts.Service
{
    /// <summary>
    /// Класс переключения между сценами
    /// </summary>
    public class ScenesSwitcher : MonoBehaviour
    {
        private static ScenesSwitcher instance;
        public static ScenesSwitcher Instance { get => instance; }

        private void Awake()
        {

            if (instance == null) instance = this;
            else Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
            //SceneManager.LoadScene(sceneName);
        }
        public void QuitApplication() => Application.Quit();
    }
}