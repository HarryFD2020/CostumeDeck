using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bamboom.Framework
{
    public class CoroutineMgr : MonoBehaviour
    {
        private static CoroutineMgr _ins;

        /// <summary>
        /// 运行Coroutine的实列
        /// </summary>
        public static CoroutineMgr Instance {
            get {
                if (_ins == null)
                {
                    var gm = new GameObject("CoroutineMgr");
                    _ins = gm.AddComponent<CoroutineMgr>();    
                }
                return _ins;
            } 
            set {
                if (_ins != null)
                {
                    Destroy(_ins.gameObject);
                }
                _ins = value;
            } }

        public static Coroutine StartCoroutine(IEnumerator enumerator, bool doNotDestroy)
        {
            if (doNotDestroy) { DontDestroyOnLoad(Instance); }

            return Instance.StartCoroutine(enumerator);
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
        /// <summary>
        /// 在若干秒后执行操作
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="duration">延迟秒</param>
        /// <param name="inRealTime">true: 真实时间 false: 游戏时间</param>
        /// <returns></returns>
        public static Coroutine DoActionAfterSec(Action action, float duration, bool inRealTime = false)
        {
            return CoroutineMgr.StartCoroutine(ActionDelaySec(action, duration, inRealTime), false);
        }

        /// <summary>
        /// 在若干帧后执行操作
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="num">延迟帧</param>
        /// <returns></returns>
        public static Coroutine DoActionAfterFrame(Action action, int num = 1)
        {
            return CoroutineMgr.StartCoroutine(ActionDelayFrame(action, num), false);
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


