using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHoppMVC.Areas.Admin.Models
{
    public class SalesManage
    {
        private int _id;
        public int ID
        {
            get { return _id; }

            set { _id = value; }
        }
        public string _TypeName;
        public string TypeName
        {
            get { return _TypeName; }

            set { _TypeName = value; }
        }
        public string _goodsName;
        public string goodsName
        {
            get { return _goodsName; }

            set { _goodsName = value; }
        }
        public string _sipplierName;
        public string sipplierName
        {
            get { return _sipplierName; }

            set { _sipplierName = value; }
        }
        public decimal ? _marketprice;
        public decimal ? marketprice
        {
            get { return _marketprice; }

            set { _marketprice = value; }
        }
        public decimal ? _promoteprice;
        public decimal ? promoteprice
        {
            get { return _promoteprice; }

            set { _promoteprice = value; }
        }
        public DateTime ? _promoteStartDate;
        public DateTime ? promoteStartDate
        {
            get { return _promoteStartDate; }

            set { _promoteStartDate = value; }
        }
        public decimal ? _postage;
        public decimal ? postage
        {
            get { return _postage; }

            set { _postage = value; }
        }

        public int ? _goodsNumber;
        public int ? goodsNumber
        {
            get { return _goodsNumber; }

            set { _goodsNumber = value; }
        }

        public int ? _salesVolume;
        public int ? salesVolume
        {
            get { return _salesVolume; }

            set { _salesVolume = value; }
        }
        public int ? _isShipping;
        public int ? isShipping
        {
            get { return _isShipping; }

            set { _isShipping = value; }
        }
    }
}