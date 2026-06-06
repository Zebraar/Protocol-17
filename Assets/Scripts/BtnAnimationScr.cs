using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using DG.Tweening;

public class BtnAnimationScr : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private Vector3 targetScale = new Vector3(1.2f, 1.2f, 1f);
    [SerializeField] private float duration = 0.1f;

    private Vector3 originalScale;
    private RectTransform parentRect;
    private bool isScaled = false;

    void Start()
    {
        if (transform.parent != null)
        {
            parentRect = transform.parent.GetComponent<RectTransform>();
        }

        if (parentRect != null)
        {
            originalScale = parentRect.localScale;
        }
    }

    void Update()
    {
        if (Pointer.current != null && Pointer.current.press.isPressed)
        {
            bool currentHover = IsPointerOverThisInUI();
            if (currentHover && !isScaled)
            {
                AnimateScale(targetScale, Ease.OutQuad);
                isScaled = true;
            }
            else if (!currentHover && isScaled)
            {
                AnimateScale(originalScale, Ease.InQuad);
                isScaled = false;
            }
        }
        else if (isScaled)
        {
            AnimateScale(originalScale, Ease.InQuad);
            isScaled = false;
        }
    }

    private bool IsPointerOverThisInUI()
    {
        if (EventSystem.current == null || Pointer.current == null) return false;

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Pointer.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        if (results.Count > 0)
        {
            return results[0].gameObject == gameObject;
        }

        return false;
    }

    private void AnimateScale(Vector3 target, Ease easeType)
    {
        if (parentRect == null) return;
        parentRect.DOKill();
        parentRect.DOScale(target, duration).SetEase(easeType);
    }

    void OnDestroy()
    {
        if (parentRect != null)
        {
            parentRect.DOKill();
        }
    }
}
