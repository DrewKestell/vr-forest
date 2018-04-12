using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryUI : MonoBehaviour
{
    const int inventorySize = 10;

    Transform video;
    Transform itemNameText;
    Transform descriptionText;

    List<ItemSlot> itemSlots;

    int displayItemIndex = -1;

    Transform canvas;

    void Start()
    {
        canvas = transform.Find("Canvas");
        itemSlots = canvas.GetComponentsInChildren<ItemSlot>().ToList();

        // once this inventory is persistent, we'll want to initialize these ItemSlots from data in a DB or something
        for (var i = 0; i < itemSlots.Count; i++)
        {
            var itemSlot = itemSlots.ElementAt(i);

            if (itemSlot.Quantity > 1)
            {
                var uiElement = gameObject.transform.Find($"ItemSlot_{i}");
                uiElement.Find("QuantityBackground").gameObject.SetActive(true);
                var quantityText = uiElement.Find("QuantityText");
                quantityText.GetComponent<Text>().text = itemSlot.Quantity.ToString();
                quantityText.gameObject.SetActive(true);
            }
        }

        video = canvas.Find("Video");
        itemNameText = canvas.Find("ItemNameText");
        descriptionText = canvas.Find("DescriptionText");
    }

    public bool AddItemToInventory(Storable storable)
    {
        if (!storable.Stackable && !itemSlots.Any(i => i.Storable == null))
            return false;

        // first check this item is stackable, and if we have a stack in our inventory we can increment
        var itemSlot = TryIncrementExistingItem(storable);

        if (itemSlot == null)
        {
            itemSlot = itemSlots.FirstOrDefault(i => i.Storable == null);
            itemSlot.Storable = storable;
            itemSlot.Quantity = 1;
        }

        var uiElement = itemSlot.gameObject.transform;

        var itemImage = uiElement.Find("ItemImage");        
        itemImage.GetComponent<Image>().sprite = storable.Sprite;
        itemImage.gameObject.SetActive(true);

        if (itemSlot.Quantity > 1)
        {
            uiElement.Find("QuantityBackground").gameObject.SetActive(true);
            var quantityText = uiElement.Find("QuantityText");
            quantityText.GetComponent<Text>().text = itemSlot.Quantity.ToString();
            quantityText.gameObject.SetActive(true);
        }

        return true;
    }

    ItemSlot TryIncrementExistingItem(Storable storable)
    {
        if (storable.Stackable)
        {
            var existingItemStack = itemSlots.FirstOrDefault(i => i.Storable?.Id == storable.Id);
            if (existingItemStack != null)
            {
                existingItemStack.Quantity++;
                return existingItemStack;
            }          
        }
        return null;
    }

    public void ToggleItemDisplay(ItemSlot itemSlot)
    {
        var index = itemSlots.IndexOf(itemSlot);

        if (displayItemIndex == index)
            ClearItemDisplay();
        else
            DisplayItem(itemSlot);
    }

    void DisplayItem(ItemSlot itemSlot)
    {
        var index = itemSlots.IndexOf(itemSlot);
        var videoPlayer = video.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.clip = itemSlot.Storable.VideoClip;

        if (!videoPlayer.isPlaying)
            videoPlayer.Play();

        var rawImage = video.GetComponent<RawImage>();
        rawImage.enabled = true;

        itemNameText.GetComponent<Text>().text = itemSlot.Storable.ItemName;
        descriptionText.GetComponent<Text>().text = itemSlot.Storable.DescriptionText;

        ClearCurrentHighlight();

        var uiElement = canvas.Find($"ItemSlot_{index}");
        uiElement.Find("Highlight").gameObject.SetActive(true);

        displayItemIndex = index;
    }

    void ClearItemDisplay()
    {
        video.GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
        video.GetComponent<RawImage>().enabled = false;
        itemNameText.GetComponent<Text>().text = string.Empty;
        descriptionText.GetComponent<Text>().text = string.Empty;

        ClearCurrentHighlight();

        displayItemIndex = -1;
    }

    void ClearCurrentHighlight()
    {
        if (displayItemIndex != -1)
        {
            var uiElement = canvas.Find($"ItemSlot_{displayItemIndex}");
            uiElement.Find("Highlight").gameObject.SetActive(false);
        }       
    }
}
