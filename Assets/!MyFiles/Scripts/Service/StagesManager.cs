using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace MyScripts.Service
{
    /// <summary>
    /// Класс управляющий этапами сценария
    /// </summary>
    public class StagesManager : MonoBehaviour
    {
        private static StagesManager instance;
        public static StagesManager Instance
        {
            get { return instance; }
            private set { instance = value; }
        }


        [SerializeField] private List<AudioClip> clipsTips;
        [SerializeField] private AudioSource sourceTips;


        [SerializeField] private int stageActive = -1;
        public int StageActive
        {
            get
            {
                return stageActive;
            }
            private set
            {
                stageActive = value;
                StartCoroutine(stages[stageActive].StartStage());
                if (outDescription) outDescription.text = stages[stageActive].description;
            }
        }

        [SerializeField] private bool launchOnStart;

        [SerializeField] private List<Stage> stages;
        [SerializeField] private TMP_Text outDescription;


        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if (launchOnStart) NextStage(0);
        }

        /// <summary>
        /// Метод который переходит к следующему этапу, при этом он не может перескочить предыдущие этапы
        /// </summary>
        /// <param name="codeStage">Код следующего этапа</param>
        public void NextStage(int codeStage)
        {

            if (stageActive + 1 == codeStage) StageActive = codeStage;
        }

        public void PlayTips(int indexTip)
        {
            if ((indexTip < 0) || (indexTip >= clipsTips.Count)) return;
            sourceTips.Stop();
            sourceTips.PlayOneShot(clipsTips[indexTip]);
        }
    }

    [System.Serializable]
    public class Stage
    {
        public string description;
        public AudioClip startAudio;
        public AudioSource source;
        public UnityEvent OnStartStage;
        public UnityEvent OnEndStage;


        public IEnumerator StartStage()
        {
            OnStartStage.Invoke();
            source.Stop();
            if (source) source.PlayOneShot(startAudio);
            yield return new WaitForSeconds(startAudio.length);
            OnEndStage.Invoke();
        }
    }
}