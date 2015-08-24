using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

    public class ShoppingCartItems
    {
        public List<ShoppingItems> Items { get; private set; }
        public static ShoppingCartItems Instance;

        static ShoppingCartItems()
        {
            if(HttpContext.Current.Session["CSharpShoppingCart"] == null)
            {
                Instance = new ShoppingCartItems();
                Instance.Items = new List<ShoppingItems>();
                HttpContext.Current.Session["CSHarpShoppingCart"] = Instance;
            }
            else
            {
                Instance = (ShoppingCartItems)HttpContext.Current.Session["CSharpSHoppingCart"];
                    
            }
        }

        protected ShoppingCartItems() { }

        public void AddItem(int itemID)
        {
            ShoppingItems newItem = new ShoppingItems(itemID);
            if(Items.Contains(newItem))
            {
                foreach(ShoppingItems item in Items)
                {
                    if(item.Equals(newItem))
                    {
                        item.quantity++;
                        return;
                    }
                }
            }
            else
            {
                newItem.quantity = 1;
                Items.Add(newItem);

            }
        }

        public void AddItem(string projectID)
        {
            ShoppingItems newItem = new ShoppingItems(projectID);
            if (Items.Contains(newItem))
            {
                foreach (ShoppingItems item in Items)
                {
                    if (item.Equals(newItem))
                    {
                        item.quantity++;
                        return;
                    }
                }
            }
            else
            {
                newItem.quantity = 1;
                Items.Add(newItem);

            }
        }

        public void SetItemQuantity(int itemID, int quantity)
        {
            if(quantity == 0)
            {
                RemoveItem(itemID);
            }

            ShoppingItems updatedItem = new ShoppingItems(itemID);

            foreach(ShoppingItems item in Items)
            {
                if (item.Equals(updatedItem))
                {
                    item.quantity = quantity;
                    return;
                }
            }
        }

        public void RemoveItem(int itemID)
        {
            ShoppingItems removedItem = new ShoppingItems(itemID);
            Items.Remove(removedItem);
        }

        public decimal getSubtotal()
        {
            decimal subTotal = 0;
            foreach (ShoppingItems item in Items)
            {
                subTotal += item.TotalPrice;
            }
            return subTotal;
        }

    }
