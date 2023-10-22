using JDR.Model;
using JDR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Core
{
    public class ShopCore
    {
        private IMainRepository _Repository;

        public ShopCore(IMainRepository repo, IImageStorage imageUploader)
        {
            _Repository = repo;
        }

        public bool PurchaseItem(Character character, InventoryItem item) 
        {
            return false;
        }

    }
}
