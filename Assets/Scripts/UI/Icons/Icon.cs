using System;
using Logic.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Icons
{
    public class Icon : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void Setup(Sprite sprite)
        {
            image.sprite = sprite;
        }
    }
}