using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controllers.Misc;
using Data;
using Data.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Ui
{
    public class ResourcesManagerOveraly : MonoBehaviour
    {
        public GameObject ButtonPrefab;
        public GameObject InputFieldPrefab;
        private List<ItemId> SelectableItemIds { get; set; }

        private Canvas _canvas;

        // Start is called before the first frame update
        private void Start()
        {
            Events.Current.OnToggleResourceManagerOverlay += ToggleOverlay;
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
        }

        private void ToggleOverlay()
        {
            Cursor.visible = !Cursor.visible;
            _canvas.enabled = !_canvas.enabled;

            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }
        public void HandleEscapeKey()
        {
            if (_canvas.enabled)
                ToggleOverlay();
        }


        private void OnDestroy()
        {
            Events.Current.OnToggleResourceManagerOverlay -= ToggleOverlay;
        }

        public void AddResource(ItemQuantity itemQuantity)
        {
            if (MissingResourceQuantities.Any(i => i.Id == itemQuantity.Id))
            {
                var alreadyAddedQuantity = MissingResourceQuantities.Single(s => s.Id == itemQuantity.Id);
                alreadyAddedQuantity.Quantity += itemQuantity.Quantity;
            }
            else
            {
                MissingResourceQuantities.Add(itemQuantity);
            }

            UpdateText();
        }

        private void UpdateText()
        {
            var text = GetComponentsInChildren<Text>().Single(s => s.name == "resume");
            var builder = new StringBuilder();
            foreach (var itemQuantity in MissingResourceQuantities)
            {
                builder.AppendLine(itemQuantity.ToString());
            }

            text.text = builder.ToString();
        }
        
        public static readonly List<ItemQuantity> MissingResourceQuantities = new List<ItemQuantity>();
    }
}