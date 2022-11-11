using System;
using System.Collections;
using Infrastructure.Basic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) => _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string nextSceneName, Action onLoaded = null)
        {

      
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextSceneName);

            while (!waitNextScene.isDone)
            {
                yield return null;
            }
      
            onLoaded?.Invoke();
        }
    }
}