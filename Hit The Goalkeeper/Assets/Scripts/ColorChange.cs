using DG.Tweening;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    #region Singleton

    public static ColorChange main;

    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(gameObject);
            return;
        }

        main = this;
    }

    #endregion

    [SerializeField] private Gradient _gradient;
    [SerializeField] private SpriteRenderer arrow;

    public void DoColorChange()
    {
        arrow.enabled = true;
        arrow.DOGradientColor(_gradient, 2).SetLoops(-1, LoopType.Yoyo).SetAutoKill(false);
    }

    public void DoColorChangeStop()
    {
        transform.DOPause();
        arrow.DOPause();
        arrow.enabled = false;
    }
}