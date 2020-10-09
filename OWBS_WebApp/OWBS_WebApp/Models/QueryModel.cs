using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OWBS_WebApp.Models
{
    public class QueryListModel
    {
        public QueryListModel()
        {
            ID         = "";
            RecordLen  = 0;
            ShowLen    = "0分00秒";
            RecordFile = "";
            Percentage = 0;
            StationFK  = "";
            StationName = "";
        }
        public string ID { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "錄影時間")]
        public DateTime RecordDT { get; set; }
        [Display(Name = "錄影長度")]
        public int RecordLen { get; set; }
        [Display(Name = "錄影長度")]
        public string ShowLen { get; set; }
        [Display(Name = "錄影檔案")]
        public string RecordFile { get; set; }
        [Display(Name = "有魚影像百分比")]
        public int Percentage { get; set; }
        [Display(Name = "魚場地點")]
        public string StationFK { get; set; }
        [Display(Name = "魚場地點")]
        public string StationName { get; set; }
    }

    public class QueryResultModel
    {
        public QueryResultModel()
        {
            ID        = "";
            BgnDate   = DateTime.Now.Date.AddDays(-60);
            EndDate   = DateTime.Now.Date.AddDays(-30);
            ListModel = new List<QueryListModel>();
        }
        public string ID { get; set; }
        [Required(ErrorMessage = "開始錄影時間必須要輸入!")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "開始錄影時間")]
        public DateTime BgnDate { get; set; }
        [Required(ErrorMessage = "結束錄影時間必須要輸入!")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "結束錄影時間")]
        public DateTime EndDate { get; set; }
        [Range(0, 100, ErrorMessage = " {0} 必須在 {1} - {2} 之間")]
        [Display(Name = "有魚影像百分比")]
        public int Percentage { get; set; }
        [Display(Name = "魚場地點")]
        public string StationFK { get; set; }
        [Display(Name = "魚場地點")]
        public List<SelectListItem> StationItems { get; set; }
        //
        public List<QueryListModel> ListModel { get; set; }
    }

    public class QueryTslModel
    {
        public QueryTslModel()
        {
            SeqNo   = 0;
            BgnTime = 0;
            EndTime = 0;
            LenTime = 0;
            Percent = 0.0;
            BgnShow = "0分00秒";
            EndShow = "0分00秒";
        }
        [Display(Name = "序號")]
        public int SeqNo { get; set; }
        [Display(Name = "開始秒數")]
        public int BgnTime { get; set; }
        [Display(Name = "結束秒數")]
        public int EndTime { get; set; }
        [Display(Name = "長度(秒)")]
        public int LenTime { get; set; }
        [Display(Name = "長度佔全影片百分比(%)")]
        public double Percent { get; set; }
        [Display(Name = "開始時間")]
        public string BgnShow { get; set; }
        [Display(Name = "結束時間")]
        public string EndShow { get; set; }
    }

    public class QueryDetailModel
    {
        public QueryDetailModel()
        {
            RecordFile = "";
            FrameFile  = "";
            RecordLen  = 0;
            Percentage = 0;
            TslModel   = new List<QueryTslModel>();
        }
        [Display(Name = "錄影檔案")]
        public string RecordFile { get; set; }
        [Display(Name = "有框檔案")]
        public string FrameFile { get; set; }
        [Display(Name = "錄影時間")]
        [DataType(DataType.DateTime)]
        public DateTime RecordDT { get; set; }
        [Display(Name = "錄影長度")]
        public int RecordLen { get; set; }
        [Display(Name = "有魚影像百分比")]
        public int Percentage { get; set; }
        //
        public List<QueryTslModel> TslModel { get; set; }
    }

    public class TimeMarkModel
    {
        public TimeMarkModel()
        { 
            time     = 0;
            text     = "";
            duration = 0;
        }
        public int time { get; set; }
        public string text { get; set; }
        public int duration { get; set; }
    }
}