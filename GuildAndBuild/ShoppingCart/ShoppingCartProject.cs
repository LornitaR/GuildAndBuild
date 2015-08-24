using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    public class ShoppingCartProject
    {
        public List<ShoppingItems> Items { get; private set; }
        public static readonly ShoppingCartProject Instance;

        static ShoppingCartProject()
        {
            if(HttpContext.Current.Session["ProjectsShoppingCart"] == null)
            {
                Instance = new ShoppingCartProject();
                Instance.Items = new List<ShoppingItems>();
                HttpContext.Current.Session["ProjectsShoppingCart"] = Instance;
            }
            else
            {
                Instance = (ShoppingCartProject)HttpContext.Current.Session["ProjectsShoppingCart"];
            }
        }

        public void AddItem(string projectID)
        {
            ShoppingItems newItem = new ShoppingItems(projectID);
            if (!Items.Contains(newItem))
            {
                newItem.quantity = 1;
                Items.Add(newItem);
            }
        }

        public void RemoveItem(string projectID)
        {
            ShoppingItems removedItem = new ShoppingItems(projectID);
            Items.Remove(removedItem);
        }

    }
