using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Accessables
{
    public static class DoTweenController
    {
        //Moving an object in 3 dimensional space.
        internal static void DoMove3D(Transform thisTransform, Vector2 endValue, float duration, Ease ease, int setLoops, LoopType loop)
        {
            thisTransform.DOMove(endValue, duration).SetEase(ease).SetLoops(-1,loop);
        }

        //Moving an objects local position in 3 dimensional space.
        internal static void DoLocalMove3D(Transform thisTransform, Vector2 endValue, float duration, Ease ease, int setLoops, LoopType loop)
        {
            thisTransform.DOLocalMove(endValue, duration).SetEase(ease).SetLoops(-1,loop);
        }

        //Moving an object in 2 dimensional space.
        internal static void DoMove2D(Transform thisTransform, Vector2 endValue, float duration, Ease ease, int setLoops, LoopType loop)
        {
            thisTransform.DOMove(endValue, duration).SetEase(ease).SetLoops(-1,loop);
        }

        //Moving and rotating an object in 3 dimensional space.
        public static void SequenceMoveAndRotate3D(Transform thisTransform, Vector3 endValuePos, Vector3 endValueRot,
            float duration)
        {
            var seq = DOTween.Sequence();
            seq.Append(thisTransform.DOLocalRotate(endValueRot, duration));
        }

        //Moving an objects y position and changing its color with ease.
        internal static void SequenceMoveYandChangeColorWithEase(Transform thisTransform, float endValuePos,
            MeshRenderer meshMat, Color color, float duration)
        {
            var ease = Ease.OutBounce;
            var seq = DOTween.Sequence();
            seq.Append(thisTransform.DOMoveY(endValuePos, duration)).Join(meshMat.material.DOColor(color, duration))
                .SetEase(ease);
        }

        //Scale an objects x axis.
        internal static void ScaleX(Transform thisTransform, float endValue, float duration)
        {
            thisTransform.DOScaleX(endValue, duration).SetEase(Ease.Unset);
        }

        //Scale an objects z axis.
        internal static void ScaleZ(Transform thisTransform, float endValue, float duration)
        {
            thisTransform.DOScaleZ(endValue, duration).SetEase(Ease.Unset);
        }

        //Filling the Bar.
        internal static void BarFill(Image image, float endValue, float duration)
        {
            image.DOFillAmount(endValue, duration);
        }

        //Scale an objects x and y axises.
        internal static void ScaleXy(Transform thisTransform, float scaleValueX, float scaleValueY, float duration)
        {
            var seq = DOTween.Sequence();
            seq.Append(thisTransform.DOScaleX(scaleValueX, duration))
                .Join(thisTransform.DOScaleY(scaleValueY, duration))
                .SetAutoKill(true);
        }

        //Scaling an objects x, y axis and moving it in 2 dimensions
        internal static void ScaleXyAndMove2D(Transform thisTransform, Behaviour behaviour, float endValue,
            float scaleEndValueX, float scaleEndValueY, float duration)
        {
            var seq = DOTween.Sequence();
            seq.Append(thisTransform.DOScaleX(scaleEndValueX, duration))
                .Join(thisTransform.DOScaleY(scaleEndValueY, duration))
                .Join(thisTransform.transform.DOLocalMoveY(endValue, duration))
                .OnComplete(() => DestroyObjBehaviour((behaviour)));
        }

        //Deactivating an objects behaviour.
        internal static void DestroyObjBehaviour(Behaviour component)
        {
            component.enabled = false;
        }

        //Moving a GUI objects anchored position.
        internal static void AnchoredPosMove(RectTransform rectTransform, Vector2 endValue, float duration, Ease ease,
            int setLoops, LoopType loopType)
        {
            rectTransform.DOAnchorPos(endValue, duration).SetLoops(setLoops, loopType).SetEase(ease);
        }
    }
}