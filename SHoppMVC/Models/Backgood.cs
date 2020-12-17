using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHoppMVC.Models
{
    public class Backgood
    {
        private int _id;
        public int ID
        {
            get { return _id; }

            set { _id = value; }
        }

        public string _goodsSn;
        public string goodsSn
        {
            get { return _goodsSn; }

            set { _goodsSn = value; }
        }
        public decimal? _marketPrice;
        public decimal? marketPrice
        {
            get { return _marketPrice; }

            set { _marketPrice = value; }
        }
        public int? _buyNumber;
        public int? buyNumber
        {
            get { return _buyNumber; }

            set { _buyNumber = value; }
        }
        public int? _userId;
        public int? userId
        {
            get { return _userId; }

            set { _userId = value; }
        }

        public string _goodsName;
        public string goodsName
        {
            get { return _goodsName; }

            set { _goodsName = value; }
        }

        public string _thumbUrl;
        public string thumbUrl
        {
            get { return _thumbUrl; }

            set { _thumbUrl = value; }
        }
        public decimal? _totalprice;
        public decimal? totalprice
        {
            get { return _totalprice; }

            set { _totalprice = value; }
        }
    }
}