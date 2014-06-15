﻿/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2012/6/26 1:18:03
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BabyPlan.Common;

namespace BabyPlan.Meta
{
    [Serializable]
    public class GenVote
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 Vid
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Vtitle
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String VDes
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 ObjId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 ObjType
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 UpNum
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 DownNum
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 CreateId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 State
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public GenVote BuildSampleEntity(IDataReader reader)
        {
            this.Vid = DBConvert.ToInt32(reader["vid"]);
            this.Vtitle = DBConvert.ToString(reader["vtitle"]);
            this.VDes = DBConvert.ToString(reader["v_des"]);
            this.ObjId = DBConvert.ToInt32(reader["obj_id"]);
            this.ObjType = DBConvert.ToInt32(reader["obj_type"]);
            this.UpNum = DBConvert.ToInt32(reader["up_num"]);
            this.DownNum = DBConvert.ToInt32(reader["down_num"]);
            this.CreateTime = DBConvert.ToDateTime(reader["create_time"]);
            this.CreateId = DBConvert.ToInt32(reader["create_id"]);
            this.State = DBConvert.ToInt32(reader["state"]);
            return this;
        }
    }
}