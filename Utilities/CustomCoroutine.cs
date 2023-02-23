using System.Collections;
using UnityEngine;
using System;

namespace OrangeBear.Utilities
{
    public static class CustomCoroutine
    {
        public static IEnumerator WaitForFrames(this MonoBehaviour monoBehaviour, int frameCount)
        {
            for (int i = 0; i < frameCount; i++)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        public static IEnumerator WaitOneFrame(Action callback = null)
        {
            yield return new WaitForEndOfFrame();
            
            callback?.Invoke();
        }
    }
}
