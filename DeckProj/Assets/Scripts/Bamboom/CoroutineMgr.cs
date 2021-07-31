using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bamboom.Framework
{
    public class CoroutineMgr : MonoBehaviour
    {
        public static CoroutineMgr Instance;

        public static Coroutine StartCoroutine(IEnumerator enumerator, bool doNotDestroy = false)
        {
            if (Instance == null)
            {
                var ins = new GameObject("CoroutineMgr");
                Instance = ins.AddComponent<CoroutineMgr>();

                if (doNotDestroy) { DontDestroyOnLoad(Instance.gameObject); }
            }

            return (Instance as MonoBehaviour).StartCoroutine(enumerator);
        }

        private void OnDestroy()
        {
            if (Instance == this) { Instance = null; }
        }
    }
}


