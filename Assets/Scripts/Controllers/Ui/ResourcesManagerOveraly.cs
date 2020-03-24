using System;
using System.Collections.Generic;
using System.Linq;
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
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;

            SelectableItemIds = new List<ItemId>()
            {
                ItemId.Berry,
                ItemId.Branch,
                ItemId.Stone,
                ItemId.ThrowableStick
            };

            var j = 0;
            foreach (var item in SelectableItemIds)
            {
                var btn = Instantiate(InputFieldPrefab, Vector3.zero, Quaternion.identity, transform.gameObject.transform);
                btn.name = item.ToString();
                btn.transform.position += (Vector3.up * 10) + (Vector3.up * (btn.transform.GetComponent<RectTransform>().rect.height * j));
                btn.transform.position += (Vector3.right * (btn.transform.GetComponent<RectTransform>().rect.width * j)) + (Vector3.right * 10); 
                // btn.GetComponent<InputField>().onEndEdit.AddListener(call => call.)
                var text = btn.GetComponentInChildren<Text>();
                text.text = item.ToString();
                j++;
            }
        }

        private void OnDestroy()
        {
            
        }
    }
}