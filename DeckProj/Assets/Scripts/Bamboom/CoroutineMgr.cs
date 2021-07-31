using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bamboom.Framework
{
    public class CoroutineMgr : MonoBehaviour
    {
        public static CoroutineMgr Instance { get {
                if (Instance == null)
                {
                    var ins = new GameObject("CoroutineMgr");
                    Instance = ins.AddComponent<CoroutineMgr>();    
                }
                return Instance;
            } 
            set {
                if (Instance != null)
                {
                    Destroy(Instance.gameObject);
                }
                Instance = value;
            } }

        public static Coroutine StartCoroutine(IEnumerator enumerator, bool doNotDestroy = false)
        {
            if (doNotDestroy) { DontDestroyOnLoad(Instance); }

            return (Instance as MonoBehaviour).StartCoroutine(enumerator);
        }

        public static new void StopCoroutine(IEnumerator enumerator)
        {
            (Instance as MonoBehaviour).StopCoroutine(enumerator);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            if (Instance == this) { Instance = null; }
        }
    }

    public class TimeHold
    {
        public static Coroutine DoActionAfterSec(Action action, float duration, bool inRealTime = false)
        {
            return CoroutineMgr.StartCoroutine(ActionDelaySec(action, duration, inRealTime));
        }

        public static Coroutine DoActionAfterFrame(Action action, int num = 1)
        {
            return CoroutineMgr.StartCoroutine(ActionDelayFrame(action, num));
        }

        private static IEnumerator ActionDelaySec(Action action, float duration, bool inRealTime)
        {
            if (inRealTime) { yield return new WaitForSecondsRealtime(duration); }
            else { yield return new WaitForSeconds(duration); }
            action.Invoke();
        }

        private static IEnumerator ActionDelayFrame(Action action, int num)
        {
            num = num > 0 ? num : 1;
            for (int i = 0; i < num; i++)
            {
                yield return null;
            }
            action.Invoke();
        }

    }
}


