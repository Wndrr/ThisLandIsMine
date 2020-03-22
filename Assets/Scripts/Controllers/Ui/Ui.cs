using System.Collections.Generic;
using System.Text;
using Controllers.Misc;
using Data.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Ui
{
    public class Ui : MonoBehaviour
    {

        private Text _inventoryText;

        // Start is called before the first frame update
        private void Start()
        {
            Events.Current.OnInventoryUpdate += OnInventoryUpdate;
            _inventoryText = GetComponentInChildren<Text>();
        
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        public void OnInventoryUpdate(List<ItemQuantity> items)
        {
            var builder = new StringBuilder();
            items.ForEach(i => builder.AppendLine(i.ToString()));

            _inventoryText.text = builder.ToString();
        }

        public void OnDestroy()
        {
            Events.Current.OnInventoryUpdate -= OnInventoryUpdate;
        }
    }
}
