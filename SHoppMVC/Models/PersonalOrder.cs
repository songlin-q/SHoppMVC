using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHoppMVC.Models
{
    public class PersonalOrder
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
        public int? _payState;
        public int? payState
        {
            get { return _payState; }

            set { _payState = value; }
        }
        public int? _shippingStatus;
        public int? shippingStatus
        {
            get { return _shippingStatus; }

            set { _shippingStatus = value; }
        }
        public int? _userId;
        public int? userId
        {
            get { return _userId; }

            set { _userId = value; }
        }

        public decimal? _orderAmount;
        public decimal? orderAmount
        {
            get { return _orderAmount; }

            set { _orderAmount = value; }
        }

        public string _imgUrl;
        public string imgUrl
        {
            get { return _imgUrl; }

            set { _imgUrl = value; }
        }
        public decimal? _marketPrice;
        public decimal? marketPrice
        {
            get { return _marketPrice; }

            set { _marketPrice = value; }
        }

        public string _goodsName;
        public string goodsName
        {
            get { return _goodsName; }

            set { _goodsName = value; }
        }

        public int? _buyNumber;
        public int? buyNumber
        {
            get { return _buyNumber; }

            set { _buyNumber = value; }
        }

        public int? _count;
        public int? count
        {
            get { return _count; }

            set { _count = value; }
        }
    }
}