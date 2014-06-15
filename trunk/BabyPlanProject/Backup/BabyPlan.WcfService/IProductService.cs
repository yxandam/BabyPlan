﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BabyPlan.Common;
using BabyPlan.Meta;
using BabyPlan.DataStructure;

namespace BabyPlan.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IProductService”。
    [ServiceContract]
    public interface IProductService
    {
        /// <summary>
        /// 关闭宝贝帖子
        /// </summary>
        /// <param name="bbID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [OperationContract]
        RespResult CloseBBPost(int bbPostID, string token);
        /// <summary>
        /// 开启宝贝帖子
        /// </summary>
        /// <param name="bbID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [OperationContract]
        RespResult OpenBBPost(int bbPostID, string token);
        /// <summary>
        /// 编辑宝贝帖子信息
        /// </summary>
        /// <param name="bbID"></param>
        /// <param name="title"></param>
        /// <param name="qq"></param>
        /// <param name="mobile"></param>
        /// <param name="bbinfo"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [OperationContract]
        RespResult EditBBPostInfo(int bbPostID, string title, string qq, string mobile, string bbinfo, bool iswash, double price, int age, ItemType bbType,ItemSort bbSort, SexType sex, string token);
        /// <summary>
        /// 获取单个宝贝帖子信息
        /// </summary>
        /// <param name="bbID"></param>
        /// <returns></returns>
        [OperationContract]
        AdvancedResult<ProProduct> GetBBInfo(int bbID);

    
        /// <summary>
        /// 获取用户宝贝帖子列表
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [OperationContract]
        AdvancedResult<PageEntity<ProProduct>> SearchBBPostList(string token, int pageIndex, int pageSize);

        /// <summary>
        /// 增加宝贝帖子浏览数（浏览单个宝贝也要增加）
        /// </summary>
        /// <param name="bbID"></param>
        /// <returns></returns>
        [OperationContract]
        RespResult SetBBPostViewNum(int bbID);
        /// <summary>
        /// 获取宝贝帖子浏览数
        /// </summary>
        /// <param name="bbID"></param>
        /// <returns></returns>
        [OperationContract]
        AdvancedResult<int> GetBBViewNum(int bbID);
        ///// <summary>
        ///// 获取宝贝通过宝贝ID
        ///// </summary>
        ///// <param name="bbid"></param>
        ///// <returns></returns>
        //[OperationContract]
        //AdvancedResult<ProProduct> GetBBInfoByBBid(int bbid);

        /// <summary>
        /// 获取宝贝列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sex"></param>
        /// <param name="age"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [OperationContract]
        AdvancedResult<PageEntity<ProProduct>> LoadBBList(ItemType bbType,ItemSort bbSort,PriceRange bbRange, SexType sex, int age, int pageSize, int pageIndex);
        [OperationContract]
        AdvancedResult<List<ProProduct>> LoadInterestingBB(string token);
        /// <summary>
        /// 发布宝贝帖子，返回帖子ID
        /// </summary>
        /// <param name="title"></param>
        /// <param name="qq"></param>
        /// <param name="mobile"></param>
        /// <param name="bbinfo"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [OperationContract]
        AdvancedResult<int> publishBBPost(string title, string qq, string mobile, string bbinfo, bool iswash, double price, int age, ItemType bbType,ItemSort bbSort, SexType sex, string token);
        [OperationContract]
        RespResult BindBBPic(int postid,  int picid, string token);
        /// <summary>
        /// 获取获取用户发布帖子数
        /// <param name="userid"></param>
        /// </summary>
         [OperationContract]
        AdvancedResult<int> GetProductCountByUserId(int userid);

        /// <summary>
        /// 删除单个宝贝图片
        /// </summary>
        /// <param name="bbID">宝贝ID</param>
        /// <returns></returns>
        [OperationContract]
         RespResult DeleteBBPic(int resID, string token);
    }
}
