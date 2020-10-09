using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using OWBS_WebApp.Models;

namespace OWBS_WebApp.Controllers
{
    public class HomeController : Controller
    {   
        private OWBSEntities db = new OWBSEntities();
        //
        const string VALUE_ALL = "";
        const string TEXT_ALL  = "全部";
        //
        const string VALUE_00006 = "00006";
        const string TEXT_00006  = "台南七股 - 短棘鰏";
        //
        const string VALUE_00004 = "00004";
        const string TEXT_00004  = "新北中和 – 紅雙劍";


        // GET: Index
        public ActionResult Index()
        {
            QueryResultModel qry_rlt_model = new QueryResultModel();

            if ((Session["StationID"] != null) && (string.IsNullOrEmpty(Session["StationID"].ToString()) == false))
            {
                qry_rlt_model.StationFK = Session["StationID"].ToString();
            }

            if ((Session["BgnDate"] != null) && (string.IsNullOrEmpty(Session["BgnDate"].ToString()) == false))
            {
                qry_rlt_model.BgnDate = (DateTime)Session["BgnDate"];
            }

            if ((Session["EndDate"] != null) && (string.IsNullOrEmpty(Session["EndDate"].ToString()) == false))
            {
                qry_rlt_model.EndDate = (DateTime)Session["EndDate"];
            }

            if ((Session["Percentage"] != null) && (string.IsNullOrEmpty(Session["Percentage"].ToString()) == false))
            {
                qry_rlt_model.Percentage = (int)Session["Percentage"];
            }

            qry_rlt_model.StationItems = GetStationItems();
            qry_rlt_model.ListModel    = GetQueryList(qry_rlt_model);

            return View(qry_rlt_model);
        }

        // POST: Index
        [HttpPost]
        public ActionResult Index(QueryResultModel AQueryResultModel)
        {
            QueryResultModel qry_rlt_model = new QueryResultModel();
            qry_rlt_model.StationItems = GetStationItems();

            if (ModelState.IsValid)
            {
                QryHelper.Setting(AQueryResultModel.StationFK,
                                  AQueryResultModel.BgnDate,
                                  AQueryResultModel.EndDate,
                                  AQueryResultModel.Percentage
                                  );
                    
                qry_rlt_model.StationFK  = AQueryResultModel.StationFK;
                qry_rlt_model.BgnDate    = AQueryResultModel.BgnDate;
                qry_rlt_model.EndDate    = AQueryResultModel.EndDate;
                qry_rlt_model.Percentage = AQueryResultModel.Percentage;
                qry_rlt_model.ListModel  = GetQueryList(AQueryResultModel);
            }

            return View(qry_rlt_model);
        }

        // GET: Detail
        public ActionResult Detail(string id, string ATitle)
        {
            ViewBag.Title = ATitle;

            QueryDetailModel detect_model = new QueryDetailModel();
            List<TimeMarkModel> time_marks = new List<TimeMarkModel>();

            DetectData detect_data = db.DetectData.Find(id);
            if (detect_data != null)
            {
                List<QueryTslModel> tls_models = new List<QueryTslModel>();
                List<TlsData> tls_datas = new List<TlsData>();

                string detect_path = string.Format(@"/OWBS_HIS/Detect/{0}/{1}/{2}/{3}/",
                                                   detect_data.StationFK,
                                                   detect_data.RecordDT.ToString("yyyy"),
                                                   detect_data.RecordDT.ToString("MM"),
                                                   detect_data.RecordDT.ToString("dd")
                                                   );

                var tlsdatas = from t in db.TlsData
                               where (t.IS_CANCEL == "N") && (t.DetectFK == id)
                               orderby t.BgnTime, t.EndTime
                               select t;
                if (tlsdatas.Count() > 0)
                {
                    tls_datas = tlsdatas.ToList();
                }

                detect_model.RecordDT   = detect_data.RecordDT;
                detect_model.RecordLen  = detect_data.RecordLen;
                detect_model.Percentage = detect_data.Percentage;
                detect_model.RecordFile = detect_path + detect_data.Tls_1_mp4;
                detect_model.FrameFile  = detect_path + detect_data.Tls_2_mp4;

                int i = 1;
                foreach (TlsData tls_data in tls_datas)
                {
                    QueryTslModel tsl_model = new QueryTslModel();
                    TimeMarkModel time_mark = new TimeMarkModel();
                    //
                    string bgn_show = string.Format("{0}分{1}秒", tls_data.BgnTime / 60, tls_data.BgnTime % 60);
                    string end_show = string.Format("{0}分{1}秒", tls_data.EndTime / 60, tls_data.EndTime % 60);
                    int len_time = tls_data.EndTime - tls_data.BgnTime + 1;

                    tsl_model.SeqNo   = i;
                    tsl_model.BgnTime = tls_data.BgnTime;
                    tsl_model.EndTime = tls_data.EndTime;
                    if (len_time >= 0)
                    {
                        tsl_model.LenTime = len_time;
                        tsl_model.Percent = (len_time * 100.0) / detect_data.RecordLen;
                    }
                    else
                    {
                        tsl_model.LenTime = 0;
                        tsl_model.Percent = 0;
                    }
                    tsl_model.BgnShow = bgn_show;
                    tsl_model.EndShow = end_show;
                    detect_model.TslModel.Add(tsl_model);

                    time_mark.time = tsl_model.BgnTime;
                    time_mark.text = string.Format("序號 {0}: 有魚影像 {1}秒", i, len_time);
                    time_mark.duration = tsl_model.LenTime;
                    time_marks.Add(time_mark);

                    i++;
                }
            }

            ViewBag.TimeMarks1 = Newtonsoft.Json.JsonConvert.SerializeObject(time_marks);
            ViewBag.TimeMarks2 = Newtonsoft.Json.JsonConvert.SerializeObject(time_marks);

            return View(detect_model);
        }

        private List<QueryListModel> GetQueryList(QueryResultModel AQueryResultModel)
        { 
            List<QueryListModel> qry_rlt_models = new List<QueryListModel>();

            var qryrltmodels = from t in db.DetectData
                               where (t.IS_CANCEL == "N")
                                     && (AQueryResultModel.BgnDate <= t.RecordDT) 
                                     && (t.RecordDT <= AQueryResultModel.EndDate)
                                     && (t.Percentage >= AQueryResultModel.Percentage)
                               orderby t.RecordDT
                               select new QueryListModel
                               {
                                   ID          = t.ID,
                                   RecordDT    = t.RecordDT,
                                   RecordLen   = t.RecordLen,
                                   ShowLen     = "0分00秒",
                                   RecordFile  = "",
                                   Percentage  = t.Percentage,
                                   StationFK   = t.StationFK,
                                   StationName = t.StationFK,
                               };
            if (qryrltmodels.Count() > 0)
            {
                if (String.IsNullOrEmpty(AQueryResultModel.StationFK) == false)
                {
                    qryrltmodels = qryrltmodels.Where(t => t.StationFK == AQueryResultModel.StationFK);
                }

                qry_rlt_models = qryrltmodels.ToList();
                foreach (QueryListModel qry_rlt_model in qry_rlt_models)
                {
                    #region RecordFile
                    string detect_path = "";
                    if (qry_rlt_model.ID.Length >= 17)
                    {
                        detect_path = string.Format(@"/OWBS_HIS/Detect/{0}/{1}/{2}/",
                                                    qry_rlt_model.ID.Substring(0, 4),
                                                    qry_rlt_model.ID.Substring(4, 2),
                                                    qry_rlt_model.ID.Substring(6, 2)
                                                    );
                    }
                    if (string.IsNullOrEmpty(detect_path) == false)
                    {
                        qry_rlt_model.RecordFile = detect_path + string.Format(@"{0}.mp4", qry_rlt_model.ID);
                    }
                    #endregion

                    #region StationFK
                    if (qry_rlt_model.StationFK == VALUE_00004)
                    {
                        qry_rlt_model.StationName = TEXT_00004;
                    }
                    else if (qry_rlt_model.StationFK == VALUE_00006)
                    {
                        qry_rlt_model.StationName = TEXT_00006;
                    }
                    #endregion

                    #region ShowLen
                    qry_rlt_model.ShowLen = string.Format("{0}分{1}秒", qry_rlt_model.RecordLen / 60, qry_rlt_model.RecordLen % 60);
                    #endregion
                }
            }

            return qry_rlt_models;
        }

        private List<SelectListItem> GetStationItems()
        {
            List<SelectListItem> station_items = new List<SelectListItem>();

            SelectListItem station_all = new SelectListItem();
            station_all.Value = VALUE_ALL;
            station_all.Text = TEXT_ALL;
            station_items.Add(station_all);

            SelectListItem station_00006 = new SelectListItem();
            station_00006.Value = VALUE_00006;
            station_00006.Text  = TEXT_00006;
            station_items.Add(station_00006);

            SelectListItem station_00004 = new SelectListItem();
            station_00004.Value = VALUE_00004;
            station_00004.Text  = TEXT_00004;
            station_items.Add(station_00004);

            return station_items;
        }

        // Dispose Database
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}