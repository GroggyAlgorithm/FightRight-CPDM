using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chart.Mvc;

namespace FightRight.Controllers
{
    public class ViewFighterController : Controller
    {

        Models.FighterChart chart = Models.DBHandler.fighterInfo;

        [HttpGet]
        public ActionResult Index()
        {
            return View(chart);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {

            if (form["btnFighterClear"] == "Submit")
            {
                chart = new Models.FighterChart();
                Models.DBHandler.fighterInfo = chart;

                return View(chart);
            }
            if (form["fighterSelectA"] != null && form["fighterSelectA"] != "")
            {
                chart.currentFighterNameA = form["fighterSelectA"];

                if (chart.fightersByNameA.TryGetValue(chart.currentFighterNameA, out int fId) == true)
                {
                    chart.currentSelectionA = fId;
                    chart.dcFighterA_stats = chart.CreateFighterStatsChart(fId);
                    chart.dcFighterA_total_stats = chart.CreateFighterTotalsChart(fId);
                    chart.dcFighterA_profile = chart.CreateFighterProfileChart(fId);
                }

                chart.currentSelectionA = fId;
            }




            Models.DBHandler.fighterInfo = chart;

            return View(chart);
        }



    }



}
