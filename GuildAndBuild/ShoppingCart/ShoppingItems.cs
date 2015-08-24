using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

    public class ShoppingItems: IEquatable<ShoppingItems>
    {
        public int quantity { get; set; }
        //
        private string _projectID;
        public string ProjectID
        {
            get { return _projectID; }
            set { _projectID = value; }
        }



        private int _itemID;
        public int ItemID 
        {
            get { return _itemID; }

            set { _itemID = value; } 
        }

        private Item _item = null;

        private Item _project = null;
        //

        public Item item
        {
            get
            {
                if(_item == null)
                {
                    _item = new Item(ItemID);
                }
                return _item;
            }
        }

        public Item project
        {
            get
            {
                 if(_project == null)
                 {
                     _project = new Item(ProjectID);
                 }
                 return _project;
            }
        }

        public string Artisan 
        {
            get { return item.itemArtisan; }
        }

        public string ProjectArtisan
        {
            get { return project.itemArtisan; }
        }

        public decimal TotalPrice
        {
            get { return item.itemPrice * quantity;  }
        }

        public decimal ProjectPrice
        {
            get { return project.itemPrice; }
        }

        public string Photo
        {
            get { return item.itemPhoto;  }
        }

        public string ProjectPhoto
        {
            get { return project.itemPhoto;  }
        }

        public string itemName
        {
            get { return item.itemName; }
        }

        public string ProjectName
        {
            get { return project.itemName; }
        }

        public ShoppingItems(int itemID)
        {
            this.ItemID = itemID;
        }
        public ShoppingItems(string projectID)
        {
            this.ProjectID = projectID;
        }

        public bool Equals(ShoppingItems item)
        {
            if(item.ProjectID == null)
            {
                return item.ItemID == this.ItemID;
            }
            else
            {
                return item.ProjectID == this.ProjectID;
            }
        }
    }

