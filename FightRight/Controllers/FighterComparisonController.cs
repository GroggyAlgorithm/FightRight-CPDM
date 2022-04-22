using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace FightRight.Controllers
{
    public class FighterComparisonController : Controller
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


            if (form["fighterSelectB"] != null && form["fighterSelectB"] != "")
            {
                chart.currentFighterNameB = form["fighterSelectB"];
                if (chart.fightersByNameB.TryGetValue(chart.currentFighterNameB, out int fId) == true)
                {
                    chart.dcFighterB_stats = chart.CreateFighterStatsChart(fId);
                    chart.dcFighterB_total_stats = chart.CreateFighterTotalsChart(fId);
                    chart.dcFighterB_profile = chart.CreateFighterProfileChart(fId);
                }

                chart.currentSelectionB = fId;
            }


            if (chart.currentSelectionB != 0 && chart.currentSelectionA != 0)
            {
                chart.CreateFighterPrecitionChart();
            }


            



            Models.DBHandler.fighterInfo = chart;

            return View(chart);
        }







        [HttpGet]
        public ActionResult FighterChart()
        {
            return RedirectToAction("Index", "FighterComparison");
        }



		


    }
}

