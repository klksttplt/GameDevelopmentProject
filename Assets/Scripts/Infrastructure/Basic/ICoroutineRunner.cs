using System.Collections;
using UnityEngine;

namespace Infrastructure.Basic
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}