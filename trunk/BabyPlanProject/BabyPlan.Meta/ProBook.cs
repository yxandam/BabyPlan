﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BabyPlan.Common;
using BabyPlan.DataStructure;

namespace BabyPlan.Meta
{
    [Serializable]
    public class ProBook
    {
        /// <summary>
        /// ID
        /// </summary>
        public Int32 BookId
        { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public String BookName
        { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public String Introduction
        { get; set; }

        /// <summary>
        /// 书尺寸 1：A5 2：A6
        /// </summary>
        public BookSize BookSize
        { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public Int32 BookType
        { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Int32 AdUserId
        { get; set; }

        /// <summary>
        /// 图片列表
        /// </summary>
        public List<ResPic> Pics
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StateType State
        { get; set; }

        /// <summary>
        ///编辑时间
        /// </summary>
        public DateTime EditeTime
        { get; set; }

        /// <summary>
        /// 封面类型
        /// </summary>
        public BookCoverType BookCover
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public ProBook BuildSampleEntity(IDataReader reader)
        {
            this.BookId = DBConvert.ToInt32(reader["book_id"]);
            this.BookName = DBConvert.ToString(reader["book_name"]);
            this.BookSize = (BookSize)DBConvert.ToInt32(reader["book_size"]);
            this.BookType = DBConvert.ToInt32(reader["book_type"]);
            this.CreateTime = DBConvert.ToDateTime(reader["create_time"]);
            this.AdUserId = DBConvert.ToInt32(reader["ad_user_id"]);
            this.Introduction = DBConvert.ToString(reader["introduction"]);
            this.State = (StateType)DBConvert.ToInt32(reader["state"]);
            this.EditeTime = DBConvert.ToDateTime(reader["edite_time"]);
            this.BookCover = (BookCoverType)DBConvert.ToInt32(reader["book_cover"]);
            return this;
        }
    }
}
