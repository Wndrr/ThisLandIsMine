using System.Linq;
using Controllers.Misc;
using Data;
using Data.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Ui
{
    public class CraftingOverlay : MonoBehaviour
    {

        public GameObject ButtonPrefab;

        private Canvas _canvas;
        // Start is called before the first frame update
        private void Start()
        {
            Events.Current.OnToggleCraftingOverlay += ToggleCraftingOverlay;
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;

            var j = 0;
            foreach (var item in ItemsDatabase.Instance.items.Where(i => i.IsCraftable))
            {
                var btn = Instantiate(ButtonPrefab, Vector3.zero, Quaternion.identity, transform.gameObject.transform);
                btn.name = item.Name;
                btn.transform.position += Vector3.up * 20;
                btn.transform.position += (Vector3.right * (btn.transform.GetComponent<RectTransform>().rect.width * j)) + (Vector3.right * 10); 
                btn.GetComponent<Button>().onClick.AddListener(() => Events.Current.TriggerCraftThing(item.Id));
                var text = btn.GetComponentInChildren<Text>();
                text.text = item.Name;

                j++;
            }
            
        }

        private void ToggleCraftingOverlay()
        {
            Cursor.visible = !Cursor.visible;
            _canvas.enabled = !_canvas.enabled;

            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }

        private void OnDestroy()
        {
            Events.Current.OnToggleCraftingOverlay -= ToggleCraftingOverlay;
        }
    }
}