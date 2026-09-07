using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffImageUI : MonoBehaviour
{
    public Image iconImage;
    public Image durationMask;

    public void Setup(Sprite sprite)
    {
        iconImage.sprite = sprite;
        durationMask.fillAmount = 1f;
    }

    public void UpdateMask(float ratio)
    {
        durationMask.fillAmount = 1 - ratio;
    }
}