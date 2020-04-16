using System.Net;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Accessables
{
    public static class DoTweenController
    {
        //Moving an objects local position in 3 dimensional space.
        internal static void DoLocalMove3D(Transform thisTransform, Vector3 endValue, float duration)
        {
            thisTransform.DOLocalMove(endValue, duration);
        }

        internal static void DoLocalMove3DWithLoop(Transform thisTransform, Vector2 endValue, float duration, Ease ease,
            int setLoops, LoopType loop)
        {
            thisTransform.DOLocalMove(endValue, duration).SetEase(ease).SetLoops(-1, loop);
        }

        internal static void SeqMoveRotateCallBack(Transform thisTransform, Vector3 endValue,
            Vector3 quaternion,
            float duration, TweenCallback callback, Ease ease)
        {
            var seq = DOTween.Sequence();
            seq.SetDelay(4)
                .Append(thisTransform.DOLocalMove(endValue, duration))
                .Join(thisTransform.DOLocalRotate(quaternion, duration - 1.5f).SetEase(ease).SetAutoKill(false)
                    .OnComplete(callback));
        }

        internal static void CameraFieldOfViewChange(Camera main, int endValue, int firstValue, float duration)
        {
            var seq = DOTween.Sequence();
            seq.Append(main.DOFieldOfView(endValue, duration)).Append(main.DOFieldOfView(firstValue, duration));
        }

        internal static void CameraFieldOfViewChangeWithEase(Camera main, int endValue, int firstValue,
            float duration, Ease ease)
        {
            var seq = DOTween.Sequence();
            seq.Append(main.DOFieldOfView(endValue, duration)).SetEase(ease)
                .Append(main.DOFieldOfView(firstValue, duration));
        }

        internal static void SequenceMoveAndRotateWithExtraDelay(Transform thisTransform, Vector3 endValue,
            Vector3 lastBackwardsEndValue,
            Vector3 rotation, float duration)
        {
            var seq = DOTween.Sequence();
            seq.Append(thisTransform.DOLocalMove(endValue, duration))
                .Join(thisTransform.DOLocalRotate(rotation, duration)).SetEase(Ease.OutExpo)
                .Append(thisTransform.DOLocalMove(lastBackwardsEndValue,  duration - .25f));
            //Bunu outexpor yaptımız zaman zıbın diye yaklaşıyor
        }
        
        internal static void SequenceMoveAndRotate(Transform thisTransform, Vector3 endValue,
            Vector3 rotation, float duration)
        {
            var seq = DOTween.Sequence();
            seq.Append(thisTransform.DOLocalMove(endValue, duration))
                .Join(thisTransform.DOLocalRotate(rotation, duration)).SetEase(Ease.OutExpo);
            //Bunu outexpor yaptımız zaman zıbın diye yaklaşıyor
        }
        internal static void FirstDelayThenMoveAndRotate(Transform thisTransform, Vector3 endValue, Vector3 rotation,
            float delayDuration, float duration)
             {
                 var seq = DOTween.Sequence();
                 seq.SetDelay(delayDuration)
                    .Append(thisTransform.DOLocalMove(endValue, duration))
                    .Join(thisTransform.DOLocalRotate(rotation, duration));
        }
    }
}