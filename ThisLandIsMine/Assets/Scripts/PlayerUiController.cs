using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUiController : MonoBehaviour
{
    private Text ResourcesText;

    public void Start()
    {
        ResourcesText = gameObject.transform.Find("ResourceCount").GetComponent<Text>();
    }

    public void UpdateResources(Dictionary<ResourceType, int> resources)
    {
        ResourcesText.text = GetResourcesText(resources);
    }

    public string GetResourcesText(Dictionary<ResourceType, int> resources)
    {
        var resourceTypes = Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>();
        var stringBuilder = new StringBuilder();
        foreach (ResourceType resourceType in resourceTypes)
        {
            var hasResource = resources.TryGetValue(resourceType, out var resourceCount);
            if (hasResource)
                stringBuilder.AppendLine($"{resourceType.ToString()}: {resourceCount}");
        }

        return stringBuilder.ToString();
    }
}