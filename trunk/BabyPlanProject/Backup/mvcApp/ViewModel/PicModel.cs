﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabyPlan.Meta;

namespace BabyPlan.mvcApp.ViewModel
{
    [Serializable]
    public class PicModel : BaseModel
    {

        public int PicId { get; set; }

        public string BPicUrl { get; set; }

        public string PicUrl
        {
            get
            {
                if (string.IsNullOrEmpty(BPicUrl))
                    return BPicUrl;
                int idx = BPicUrl.LastIndexOf("/");
                if (idx > 0)
                {
                    return BPicUrl.Substring(0, idx + 1) + "m" + BPicUrl.Substring(idx + 1);
                }
                else
                {
                    return BPicUrl;
                }
            }
        }

        public string SPicUrl
        {
            get
            {
                if (string.IsNullOrEmpty(BPicUrl))
                    return BPicUrl;
                int idx = BPicUrl.LastIndexOf("/");
                if (idx > 0)
                {
                    return BPicUrl.Substring(0, idx + 1) + "s" + BPicUrl.Substring(idx + 1);
                }
                else
                {
                    return BPicUrl;
                }
            }
        }

        public int PicHeight { get; set; }

        public int PicWidth { get; set; }

        public int DisWidth { get; set; }

        public int DisHeight { get; set; }

        public string DisMargin { get; set; }

        public int DisMarginTop { get; set; }

        public int DisMarginLeft { get; set; }

        public string Description { get; set; }

        public DateTime CreateTime { get; set; }

        public PicModel Bind(ResPic pic)
        {
            this.PicHeight = pic.PicHeight;
            this.PicWidth = pic.PicWidth;
            this.BPicUrl = pic.PicUrl;
            this.PicId = pic.PicId;
            this.Description = pic.PicDescription;
            this.CreateTime = pic.CreateTime;
            return this;
        }

        public static IList<PicModel> BindList(IList<ResPic> items, ViewModelBindOption bindOption)
        {
            List<PicModel> models = new List<PicModel>();
            if (bindOption.BindProductPicsCount < 0)
                return models;
            if (items != null)
            {
                for (int i = 0, c = items.Count(); i < c; i++)
                {
                    models.Add(new PicModel().Bind(items[i]));
                    if (i + 1 == bindOption.BindProductPicsCount)
                        break;
                }
            }
            return models;
        }

        public void DisplaySeting(int maxWidth, int MaxHeight, int marginTop = 0, int marginLeft = 0)
        {
            this.DisWidth = DisplayWidth(maxWidth, MaxHeight);
            this.DisHeight = DisplayHeight(maxWidth, MaxHeight);
            this.DisMargin = DisplayMargin(maxWidth, MaxHeight, marginTop, marginLeft);
        }

        public int ScaleWidth(int height, int offset = 0)
        {
            return (int)Math.Floor((height == 0 ? 1 : height) / (double)PicHeight * PicWidth + offset);
        }

        public int ScaleHeight(int width, int offset = 0)
        {
            return (int)Math.Floor((width == 0 ? 1 : width) / (double)PicWidth * PicHeight + offset);
        }

        public int DisplayWidth(int maxWidth, int MaxHeight)
        {
            double ws = maxWidth / (double)PicWidth;
            double hs = MaxHeight / (double)PicHeight;
            if (ws < hs)
            {
                return maxWidth;
            }
            else
            {
                return ScaleWidth(MaxHeight);
            }
        }
        public int DisplayHeight(int maxWidth, int MaxHeight)
        {
            double ws = maxWidth / (double)PicWidth;
            double hs = MaxHeight / (double)PicHeight;
            if (hs < ws)
            {
                return MaxHeight;
            }
            else
            {
                return ScaleHeight(maxWidth);
            }
        }
        public string DisplayMargin(int maxWidth, int MaxHeight, int marginTop = 0, int marginLeft = 0)
        {
            int displayWidth = DisplayWidth(maxWidth, MaxHeight);
            int displayHeight = DisplayHeight(maxWidth, MaxHeight);
            int topMargin = 0;
            int leftMargin = 0;
            if (maxWidth > displayWidth)
            {
                leftMargin = (maxWidth - displayWidth) / 2;
            }
            if (MaxHeight > displayHeight)
            {
                topMargin = (MaxHeight - displayHeight) / 2;
            }
            topMargin += marginTop;
            leftMargin += marginLeft;
            this.DisMarginTop = topMargin;
            this.DisMarginLeft = leftMargin;
            return string.Format("{0}px {1}px", topMargin, leftMargin);
        }
    }
}