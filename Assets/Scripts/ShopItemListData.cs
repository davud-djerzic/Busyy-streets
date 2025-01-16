using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "ShopItemList", menuName = "ScriptableObjects/ShopItemList", order = 1)]
    public class ShopItemList : ScriptableObject
    {
        public List<shopItem> shopItemList = new List<shopItem>();
    }

    [System.Serializable]
    public class shopItem
    {
        public Sprite image;
        public int price;
        public bool isPurchased;
    }

