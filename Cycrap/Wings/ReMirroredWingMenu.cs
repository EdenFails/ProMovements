﻿using System;
using UnityEngine;
using Object = System.Object;

namespace RenamedButton69.Wings
{
    public class ReMirroredWingMenu
    {
        private ReWingMenu _leftMenu;
        private ReWingMenu _rightMenu;

        public bool Active
        {
            get => _leftMenu.Active && _rightMenu.Active;
            set
            {
                _leftMenu.Active = value;
                _rightMenu.Active = value;
            }
        }

        public ReMirroredWingMenu(string text, string tooltip, Transform leftParent, Transform rightParent, Sprite sprite = null, bool arrow = true, bool background = true, bool separator = false)
        {
            _leftMenu = new ReWingMenu(text);
            _rightMenu = new ReWingMenu(text, false);

            ReWingButton.Create(text, tooltip, _leftMenu.Open, leftParent, sprite, arrow, background, separator);
            ReWingButton.Create(text, tooltip, _rightMenu.Open, rightParent, sprite, arrow, background, separator);
        }

        public static ReMirroredWingMenu Create(string text, string tooltip, Sprite sprite = null, bool arrow = true,
            bool background = true, bool separator = false)
        {
            return new ReMirroredWingMenu(text, tooltip,
                QuickMenuEx.LeftWing.transform.Find("Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup"),
                QuickMenuEx.RightWing.transform.Find("Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup"),
                sprite, arrow, background, separator);
        }

        public ReMirroredWingButton AddButton(string text, string tooltip, Action onClick, Sprite sprite = null, bool arrow = true, bool background = true,
            bool separator = false)
        {
            if (_leftMenu == null || _rightMenu == null)
            {
                throw new NullReferenceException("This wing menu has been destroyed.");
            }

            return new ReMirroredWingButton(text, tooltip, onClick, _leftMenu.Container, _rightMenu.Container, sprite, arrow, background, separator);
        }

        public ReMirroredWingToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue)
        {
            if (_leftMenu == null || _rightMenu == null)
            {
                throw new NullReferenceException("This wing menu has been destroyed.");
            }

            return new ReMirroredWingToggle(text, tooltip, onToggle, _leftMenu.Container, _rightMenu.Container,
                defaultValue);
        }

        public ReMirroredWingMenu AddSubMenu(string text, string tooltip, Sprite sprite = null, bool arrow = true,
            bool background = true, bool separator = false)
        {
            if (_leftMenu == null || _rightMenu == null)
            {
                throw new NullReferenceException("This wing menu has been destroyed.");
            }

            return new ReMirroredWingMenu(text, tooltip, _leftMenu.Container, _rightMenu.Container, sprite, arrow,
                background, separator);
        }

        public void Destroy()
        {
            _leftMenu.Destroy();
            _rightMenu.Destroy();

            _leftMenu = null;
            _rightMenu = null;
        }
    }
}