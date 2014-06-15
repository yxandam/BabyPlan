﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BabyPlan.Common;
using BabyPlan.DataStructure;
using BabyPlan.Meta;

namespace BabyPlan.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IBookService”。
    [ServiceContract]
    public interface IBookService
    {
         [OperationContract]
        AdvancedResult<int> AddBook(string bookname, BookSize booksize, BookCoverType bookcover, int booktype, string introduction, string token);
         [OperationContract]
         RespResult EditBook(int bookid, string bookname, BookSize booksize, BookCoverType bookcover, int booktype, string introduction, string token);
         [OperationContract]
         AdvancedResult<ProBook> GetBook(int bookid);
         [OperationContract]
         AdvancedResult<List<ProBook>> SearchBooks(string token,int pageIndex,int pageSize);
         [OperationContract(Name = "SearchDefaultBooks")]
         AdvancedResult<List<ProBook>> SearchBooks();
        [OperationContract]
         RespResult BindBookPic(int bookid, int picid, string description, DateTime createtime, string token);
        [OperationContract]
        RespResult DeleteBook(int bookid, string token);
    }
}
