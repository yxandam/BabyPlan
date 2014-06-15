﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.ServiceModel.Web;
using BabyPlan.Common;
using BabyPlan.Meta;

namespace BabyPlan.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IFileService”。
    [ServiceContract]
    public interface IFileService
    {
        string AddVoteTypeImg(string voteTypeID, byte[] fileByte);
        string EditVoteType(int voteTypeID, string title, string description, byte[] fileByte);
        string EditVoteTypeImg(int voteTypeID, byte[] fileByte);
        string UploadVoteImage(int voteTypeID, byte[] fileByte);

        //删除宝贝图片
        [OperationContract]
         RespResult DeleteBBPic(int picId, string token);
        //上传宝贝图片
        [OperationContract]
        AdvancedResult<ResPic> UploadBBPic(byte[] fileByte, int picHeight, int picWidth, string token);
        //上传头像
        [OperationContract]
        AdvancedResult<ResPic> UploadUserImage(byte[] fileByte, int picHeight, int picWidth, string token);


    }
}
