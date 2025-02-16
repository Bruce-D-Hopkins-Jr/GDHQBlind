using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image _gunSights;

    public void SetGunSights(Vector3 uiPosition, float distance)
    {
        _gunSights.enabled = true;
        _gunSights.rectTransform.anchoredPosition = (Vector2)uiPosition;

        // We probably should change the size of the sight based on the distance
    }

    internal void DisableGunSights()
    {
        _gunSights.enabled = false;
    }
}
