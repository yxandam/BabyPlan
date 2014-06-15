﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using BabyPlan.Common;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Drawing;
using System.Diagnostics;
using BabyPlan.DataStructure;
using BabyPlan.DataAccess;
using BabyPlan.Meta;
using BabyPlan.Cache;

namespace BabyPlan.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“FileService”。
    public class FileServiceClient : IFileService
    {
        public void Close()
        { }
        #region IFileService 成员

        public string AddVoteTypeImg(string voteTypeID, byte[] fileByte)
        {
            throw new NotImplementedException();
        }

        public string EditVoteType(int voteTypeID, string title, string description, byte[] fileByte)
        {
            throw new NotImplementedException();
        }

        public string EditVoteTypeImg(int voteTypeID, byte[] fileByte)
        {
            throw new NotImplementedException();
        }

        public string UploadVoteImage(int voteTypeID, byte[] fileByte)
        {
            throw new NotImplementedException();
        }

        public AdvancedResult<ResPic> UploadPic(byte[] fileByte, int picHeight, int picWidth, string token)
        {
            AdvancedResult<ResPic> result = new AdvancedResult<ResPic>();
            try
            {
                int userid = string.IsNullOrWhiteSpace(token) ? 0 : Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                string fileUrl = string.Empty;
                fileUrl = FileHelper.UploadFile(userid, fileByte, "jpg", PicType.Ignore);
                ResPic pic = new ResPic();
                pic.ObjId = 0;
                pic.ObjType = PicType.Ignore;
                pic.PicUrl = fileUrl;
                pic.PicHeight = picHeight;
                pic.PicWidth = picWidth;
                pic.State = StateType.Active;
                int picid = ResPicAccessor.Instance.Insert(pic);
                pic.PicId = picid;
                result.Data = pic;
                result.Error = AppError.ERROR_SUCCESS;

            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }

            return result;
        }

        public AdvancedResult<ResPic> UploadBBPic(byte[] fileByte, int picHeight, int picWidth, string token)
        {
            AdvancedResult<ResPic> result = new AdvancedResult<ResPic>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    string fileUrl = string.Empty;
                    fileUrl = FileHelper.UploadFile(userid, fileByte, "jpg", PicType.BBPicture);

                    ResPic pic = new ResPic();
                    pic.ObjId = 0;
                    pic.ObjType = PicType.BBPicture;
                    pic.PicUrl = fileUrl;
                    pic.PicHeight = picHeight;
                    pic.PicWidth = picWidth;
                    pic.State = StateType.Active;

                    int picid = ResPicAccessor.Instance.Insert(pic);
                    pic.PicId = picid;

                    result.Data = pic;
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }

            return result;
        }

        public AdvancedResult<ResPic> UploadUserImage(byte[] fileByte, int picHeight, int picWidth, string token)
        {
            AdvancedResult<ResPic> result = new AdvancedResult<ResPic>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    string fileUrl = string.Empty;
                    fileUrl = FileHelper.UploadFile(userid, fileByte, "jpg", PicType.UserHeadImg);
                    AdUser user = UserAccessor.Instance.Get(userid, string.Empty, string.Empty, StateType.Ignore);
                    ResPic pic = new ResPic();
                    pic.ObjId = userid;
                    pic.ObjType = PicType.UserHeadImg;
                    pic.PicUrl = fileUrl;
                    pic.PicHeight = picHeight;
                    pic.PicWidth = picWidth;
                    pic.State = StateType.Active;
                    if (user.PicId > 0)
                    {
                        ResPicAccessor.Instance.Delete(user.PicId);
                    }

                    int picid = ResPicAccessor.Instance.Insert(pic);

                    result.Data = pic;
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        public RespResult DeleteBBPic(int picId, string token)
        {
            RespResult result = new RespResult();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    ResPicAccessor.Instance.Delete(picId);
                    result.Error = AppError.ERROR_SUCCESS;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        #endregion
    }
}
