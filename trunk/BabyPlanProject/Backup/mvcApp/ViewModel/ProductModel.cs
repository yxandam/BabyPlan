﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabyPlan.Meta;
using BabyPlan.WcfService;
using BabyPlan.Common;
using BabyPlan.DataStructure;

namespace BabyPlan.mvcApp.ViewModel
{
    [Serializable]
    public class ProductModel : BaseModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string QQ { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// 宝贝是否清洗
        /// </summary>
        public bool IsWash { get; set; }

        public DateTime CreateTime { get; set; }

        public int State { get; set; }

        public string FriendlyDate
        {
            get
            {
                return FriendlyDate(CreateTime, "yyyy-MM-dd");
            }
        }

        public int ReplyNum { get; set; }

        public int ViewNum { get; set; }

        public Double Price { get; set; }

        public int Age { get; set; }

        public SexType Sex { get; set; }
        /// <summary>
        /// 商品类型：0：全部 
        /// </summary>
        public ItemType ItemType { get; set; }

        public ItemSort ItemSort { get; set; }

        public IList<PicModel> Pics { get; set; }

        public UserModel Author { get; set; }

        public ProductModel Bind(ProProduct product, ViewModelBindOption bindOptions)
        {
            this.Id = product.Pid;
            this.Title = product.Title;
            this.Description = product.BbInfo;
            this.IsWash = product.IsWash;
            this.CreateTime = product.CreateTime;
            this.ViewNum = product.ViewNum;
            this.State = product.State;
            this.Price = product.Price;
            this.Sex = product.Sex;
            this.ItemType = product.ItemType;
            this.ItemSort = product.ItemSort;
            this.Age = (int)((product.MinAge + product.MaxAge) / 2.0);

            if (ContainEnumType<ProductBindType>(bindOptions.ProductBindType, ProductBindType.Pics))
            {
                this.Pics = PicModel.BindList(product.pics, bindOptions);
            }

            if (ContainEnumType<ProductBindType>(bindOptions.ProductBindType, ProductBindType.Author))
            {
                UserServiceClient client = new UserServiceClient();
                AdvancedResult<AdUser> userRes = client.GetUserInfoByID(product.CreateId);
                if (userRes.Error == DataStructure.AppError.ERROR_SUCCESS && userRes.Data != null)
                {
                    this.Author = new UserModel().Bind(userRes.Data, bindOptions);
                    if (!string.IsNullOrEmpty(userRes.Data.Qq))
                        this.QQ = userRes.Data.Qq;
                    if (!string.IsNullOrEmpty(userRes.Data.Mobile))
                        this.Phone = userRes.Data.Mobile;
                }
            }
            if (ContainEnumType<ProductBindType>(bindOptions.ProductBindType, ProductBindType.ReplyCount))
            {
                ReplyServiceClient client = new ReplyServiceClient();
                AdvancedResult<PageEntity<GenReply>> response = client.LoadReplyListByBBPostID(product.Pid, 0, 0);
                if (response.Error == AppError.ERROR_SUCCESS)
                {
                    this.ReplyNum = response.Data.RecordsCount;
                }
            }
            return this;
        }

        public IList<ProductModel> BindList(IEnumerable<ProProduct> products, ViewModelBindOption bindOptions)
        { 
            List<ProductModel> models= new List<ProductModel>();
            if (products != null)
            {
                foreach (ProProduct p in products)
                {
                    models.Add(new ProductModel().Bind(p, bindOptions));
                }
            }
            return models;
        }
    }
}