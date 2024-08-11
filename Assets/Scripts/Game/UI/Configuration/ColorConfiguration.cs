using System;
using UnityEngine;

namespace Game.UI.Configuration
{
    [Serializable]
    public class ColorConfiguration
    {
        [SerializeField]
        private Color color = Color.red;

        // Scriptable objects actually serialize private fields
        [NonSerialized]
        private string colorHex;
		
        public Color Color => color;

        public string ColorHex
        {
            get
            {
                if (string.IsNullOrEmpty(colorHex))
                {
                    colorHex = "#" + ColorUtility.ToHtmlStringRGBA(Color);
                }
                return colorHex;
            }
        }
    }
}