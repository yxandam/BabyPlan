﻿/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2012/7/15 22:10:44
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BabyPlan.DataStructure;
using BabyPlan.Common;

namespace BabyPlan.Meta
{
    [Serializable]
    public class SocialUser
    {
        /// <summary>
        /// 社交账户类型
        /// </summary>
        public SocialUserTypeEnum SocialUserType
        { get; set; }

        /// <summary>
        /// 经过授权的access_token
        /// </summary>
        public String AccessToken
        { get; set; }

        /// <summary>
        /// 授权后的secret
        /// 该字段可以缺省(Sina账号根本没有access_token_secret)
        /// </summary>
        public String AccessTokenSecret
        { get; set; }

        /// <summary>
        /// 用户名或者ID(新浪为uid,腾讯为user name)
        /// </summary>
        public String Uid
        { get; set; }

        /// <summary>
        /// 网站用户ID
        /// </summary>
        public Int32 UserId
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SocialUser BuildSampleEntity(IDataReader reader)
        {
            this.SocialUserType = (SocialUserTypeEnum)DBConvert.ToInt32(reader["social_user_type"]);
            this.AccessToken = DBConvert.ToString(reader["access_token"]);
            this.AccessTokenSecret = DBConvert.ToString(reader["access_token_secret"]);
            this.Uid = DBConvert.ToString(reader["uid"]);
            this.UserId = DBConvert.ToInt32(reader["user_id"]);
            return this;
        }
    }
}