using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace GameUtils
{
    public class PeriodicUpdater
    {
        private bool stopped;

        public void Stop() => stopped = true;

        public PeriodicUpdater(Behaviour behaviour, float updatePeriodSeconds,
            Action updateFunc, float initialDelay = 0f, PlayerLoopTiming playerLoopTiming = PlayerLoopTiming.Update)
        {
            Run(behaviour, updatePeriodSeconds, updateFunc, initialDelay, playerLoopTiming).Forget();
        }

        private async UniTaskVoid Run(Behaviour behaviour, float updatePeriodSeconds, Action updateFunc,
            float initialDelay, PlayerLoopTiming playerLoopTiming)
        {
            var lastUpdateTime = Time.time - updatePeriodSeconds + initialDelay - 0.001f;
            while (behaviour && !stopped)
            {
                var timeNow = Time.time;
                if (lastUpdateTime + updatePeriodSeconds <= timeNow && behaviour.isActiveAndEnabled)
                {
                    lastUpdateTime = timeNow;
                    try
                    {
                        updateFunc();
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }

                // Not using UniTask.Delay() since we want to update ASAP if behaviour is deactivated and then reactivated
                await UniTask.NextFrame(playerLoopTiming);
            }
        }
    }

    public static class PeriodicUpdaterExtensions
    {
        public static PeriodicUpdater UpdatePeriodically(this Behaviour behaviour, float updatePeriodSeconds,
            Action updateFunc, float initialDelay = 0f, PlayerLoopTiming playerLoopTiming = PlayerLoopTiming.Update) =>
            new PeriodicUpdater(behaviour, updatePeriodSeconds, updateFunc, initialDelay, playerLoopTiming);
    }
}