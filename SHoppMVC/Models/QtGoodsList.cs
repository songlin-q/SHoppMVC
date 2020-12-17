using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHoppMVC.Models
{
    public class QtGoodsList
    {
        private int _id;
        public int ID
        {
            get { return _id; }

            set { _id = value; }
        }

        public string _goodsName;
        public string goodsName
        {
            get { return _goodsName; }

            set { _goodsName = value; }
        }
        public string _imgUrl;
        public string imgUrl
        {
            get { return _imgUrl; }

            set { _imgUrl = value; }
        }
        public string _TypeName;
        public string TypeName
        {
            get { return _TypeName; }

            set { _TypeName = value; }
        }
        public decimal? _promoteprice;
        public decimal? promoteprice
        {
            get { return _promoteprice; }

            set { _promoteprice = value; }
        }

        public decimal? _marketprice;
        public decimal? marketprice
        {
            get { return _marketprice; }

            set { _marketprice = value; }
        }

        public string _imgDesc;
        public string imgDesc
        {
            get { return _imgDesc; }

            set { _imgDesc = value; }
        }
       
    }
}