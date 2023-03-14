using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyScripts.Service
{
    /// <summary>
    /// Класс подсчитывающий кол-во ошибок
    /// </summary>
    public class ErrorChecker : MonoBehaviour
    {
        private static int countErrors = 0;
        public static int CountErrors
        {
            get { return countErrors; }
            private set { countErrors = value; }
        }

        private void Start()
        {
            countErrors = 0;
        }

        public static void Add() => CountErrors += 1;
        public static void Add(int count) => CountErrors += count;


    }
}