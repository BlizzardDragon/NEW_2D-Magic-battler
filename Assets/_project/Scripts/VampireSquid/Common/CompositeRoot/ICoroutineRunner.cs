using System.Collections;
using UnityEngine;

namespace VampireSquid.Common.CompositeRoot
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine coroutine);
    }
}