using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Linq;
//using System.Runtime.Serialization;


namespace FightRight.Models
{


	////DataContract for Serializing Data - required to serve in JSON format
	//[DataContract]

	/// <summary>
	/// Class for fighter values
	/// </summary>
	public class FighterChart
	{

		public int maxItems { get; private set; } //The maximum amount of possible items
		public bool showingTotals = false; //Boolean for if we're showing the totals or the percentages
		public bool orderAscending = true; //If the order of the fighters should be in ascending alphabetical order
		
		public string weightClass { get; private set; }


		//public DataRow prediction = null;
		public int predictedID { get; private set; }
		public string predictedName { get; private set; }
		public double fighterACertainty { get; private set; }
		public double fighterBCertainty { get; private set; }
		public List<DataChart> dcPrediction { get; private set; }
		public Dictionary<int, string> fightersPrediction = new Dictionary<int, string>();

		public int rangeIndexA {get;private set; } //The index for the current range of fighters chosen
		public int itemCountA { get; private set; } //The amount of values to show in the range, also is the amount added to the index for each range

		public int currentSelectionA = 0; //The currently selected fighter(side A)
		public string currentFighterNameA = ""; //The name of the current fighter(for comparison fighter A)
		public Dictionary<int, string> fightersA { get; private set; } //Fighters(A for comparisons)
		public Dictionary<string, int> fightersByNameA { get; private set; } //Fighters by name(A for comparisons)


		public List<DataChart> dcFighterA_stats = new List<DataChart>(); //Fighter comparison A list of datachart type that contains a label and value for an item
		public List<DataChart> dcFighterA_profile = new List<DataChart>(); //Fighter comparison A list of datachart type that contains a label and value for an item
		

		public List<DataChart> dcFighterA_total_stats = new List<DataChart>(); //Fighter datachart of the total stats
		public List<DataChart> dcFighterB_total_stats = new List<DataChart>(); //Fighter datachart of the total stats


		public int rangeIndexB { get; private set; } //The index for the current range of fighters chosen
		public int itemCountB {get;private set;} //The amount of values to show in the range, also is the amount added to the index for each range
		public int currentSelectionB = 0; //The currently selected fighter(side B)
		public string currentFighterNameB = ""; //The name of the current fighter(for comparison fighter B)
		public Dictionary<int, string> fightersB { get; protected set; } //Fighters(B for comparisons)
		public Dictionary<string, int> fightersByNameB { get; protected set; } //Fighters by name(B for comparisons)
		public List<DataChart> dcFighterB_stats = new List<DataChart>(); //Fighter comparison B list of datachart type that contains a label and value for an item
		public List<DataChart> dcFighterB_profile = new List<DataChart>(); //Fighter comparison B list of datachart type that contains a label and value for an item


		



		/// <summary>
		/// Initializer for the class
		/// </summary>
		public FighterChart()
		{
			rangeIndexA = 0;
			rangeIndexB = 0;
			itemCountA = 0;
			itemCountB = 0;
			maxItems = 0;
			currentSelectionA = 0;
			currentSelectionB = 0;
			showingTotals = false;
			orderAscending = true;
			weightClass = "";

			fightersA = new Dictionary<int, string>();
			fightersByNameA = new Dictionary<string, int>();
			dcFighterA_stats = new List<DataChart>();
			dcFighterA_profile = new List<DataChart>();
			fighterACertainty = 0;
			
			fightersB = new Dictionary<int, string>();
			fightersByNameB = new Dictionary<string, int>();
			dcFighterB_stats = new List<DataChart>();
			dcFighterB_profile = new List<DataChart>();

			dcFighterA_total_stats = new List<DataChart>();
			dcFighterB_total_stats = new List<DataChart>();

			fighterBCertainty = 0;




			predictedID = 0;
			predictedName = "";
			dcPrediction = new List<DataChart>();


			fightersPrediction = new Dictionary<int, string>();




			currentFighterNameA = "";
			currentFighterNameB = "";

			var cpyFightersByNameA = new Dictionary<string, int>();
			var cpyFightersByNameB = new Dictionary<string, int>();

			//if (orderAscending)
			//{
			//	fightersA = DBHandler.GetFighters(out cpyFightersByNameA, "ORDER BY Fighter_Name ASC");
			//	fightersB = DBHandler.GetFighters(out cpyFightersByNameB, "ORDER BY Fighter_Name ASC");
			//}
			////Descending
			//else
			//{
			//	fightersA = DBHandler.GetFighters(out cpyFightersByNameA, "ORDER BY Fighter_Name DESC");
			//fightersB = DBHandler.GetFighters(out cpyFightersByNameB, "ORDER BY Fighter_Name DESC");
			//}


			fightersA = DBHandler.GetFighters(out cpyFightersByNameA);
			fightersB = DBHandler.GetFighters(out cpyFightersByNameB);
			fightersByNameA = cpyFightersByNameA;
			fightersByNameB = cpyFightersByNameB;

		}


		/// <summary>
		/// Initializer for the class
		/// </summary>
		public FighterChart(int minIndex, int indexOffset, int maxItemCount = 1000)
		{
			rangeIndexA = 0;
			rangeIndexB = 0;
			itemCountA = 0;
			itemCountB = 0;
			maxItems = 0;
			currentSelectionA = 0;
			currentSelectionB = 0;
			showingTotals = false;
			orderAscending = true;
			weightClass = "";

			fightersA = new Dictionary<int, string>();
			fightersByNameA = new Dictionary<string, int>();
			dcFighterA_stats = new List<DataChart>();
			dcFighterA_profile = new List<DataChart>();
			fighterACertainty = 0;

			fightersB = new Dictionary<int, string>();
			fightersByNameB = new Dictionary<string, int>();
			dcFighterB_stats = new List<DataChart>();
			dcFighterB_profile = new List<DataChart>();

			dcFighterA_total_stats = new List<DataChart>();
			dcFighterB_total_stats = new List<DataChart>();

			fighterBCertainty = 0;




			predictedID = 0;
			predictedName = "";
			dcPrediction = new List<DataChart>();


			fightersPrediction = new Dictionary<int, string>();




			currentFighterNameA = "";
			currentFighterNameB = "";


			if (itemCountA < 0)
			{
				rangeIndexA = 0;
				itemCountA = 25;
			}
			else
			{
				fightersA = DBHandler.GetFighters(out var cpyFightersByNameA, CreateFighterSelect_A());
				fightersByNameA = cpyFightersByNameA;
				
			}

			if (itemCountB < 0)
			{
				rangeIndexB = 0;
				itemCountB = 25;
			}
			else
			{
				fightersB = DBHandler.GetFighters(out var cpyFightersByNameB, CreateFighterSelect_B());
				fightersByNameB = cpyFightersByNameB;

			}

		}


		



		/// <summary>
		/// Creates a totals stats data chart list from the id of a fighter
		/// </summary>
		/// <param name="fighterID"></param>
		/// <returns></returns>
		public List<DataChart> CreateFighterTotalsChart(int fighterID)
		{
			DataRow fighter = DBHandler.GetFighterStats(fighterID);
			List<DataChart> dataPoints = new List<DataChart>();
			if (fighter != null)
			{
				dataPoints.Add(new DataChart("Knockdowns", (int)fighter["Knockdowns"]));
				dataPoints.Add(new DataChart("Submission Attempts", (int)fighter["Submission_Attempts"]));
				dataPoints.Add(new DataChart("Takedowns", (int)fighter["Takedowns"]));
				dataPoints.Add(new DataChart("Takedowns Attempted", (int)fighter["Takedowns_Attempted"]));
				dataPoints.Add(new DataChart("Total Strikes", (int)fighter["Total_Strikes"]));
				dataPoints.Add(new DataChart("Total Strikes Attempted", (int)fighter["Total_Strikes_Attempted"]));
				dataPoints.Add(new DataChart("Significant Strikes", (int)fighter["Significant_Strikes"]));
			}

			return dataPoints;
		}


		/// <summary>
		/// Creates a percentage stats data chart list from the id of a fighter
		/// </summary>
		/// <param name="fighterID"></param>
		/// <returns></returns>
		public List<DataChart> CreateFighterStatsChart(int fighterID)
		{
			DataRow fighter = DBHandler.GetFighterStats(fighterID);
			List<DataChart> dataPoints = new List<DataChart>();

			if (fighter != null)
			{
				dataPoints.Add(new DataChart("Knockdowns", (int)fighter["Knockdowns"]));
				dataPoints.Add(new DataChart("Takedowns", (System.Decimal)fighter["Takedown_Percentage"]));
				dataPoints.Add(new DataChart("Significant Strikes", (System.Decimal)fighter["Significant_Strike_Percentage"]));
				dataPoints.Add(new DataChart("Total Strikes", (System.Decimal)fighter["Total_strike_Percentage"]));
			}
			return dataPoints;
		}
		


		/// <summary>
		/// Creates a profile data chart list from the id of a fighter
		/// </summary>
		/// <param name="fighterID"></param>
		/// <returns></returns>
		public List<DataChart> CreateFighterProfileChart(int fighterID)
		{
			DataRow fighter = DBHandler.GetFighterProfile(fighterID);
			List<DataChart> dataPoints = new List<DataChart>();

			if(fighter != null)
			{
				//12:00:00 AM
				//Try gettind bday first so wer can remove the times
				if (fighter["nickname"] != null) dataPoints.Add(new DataChart("AKA: ", fighter["nickname"]));

				if (fighter["weight"] != null)
				{
					var fighterWeight = Convert.ToDouble((System.Decimal)fighter["weight"]);
					float fighterWeightIndex = 0;
					string fghtWeightClass = "";



					foreach (var wc in FightRightUtilities.KgWeightClass.Keys)
					{
						
						fighterWeightIndex = wc;
						
						if (fighterWeight < wc)
						{
							FightRightUtilities.KgWeightClass.TryGetValue(fighterWeightIndex, out fghtWeightClass);
							break;
						}

					}

					dataPoints.Add(new DataChart("Weight Class: ", fghtWeightClass));
					dataPoints.Add(new DataChart("Weight (kg): ", (System.Decimal)fighter["weight"]));
				}

				if(fighter["reach"] != null)dataPoints.Add(new DataChart("Reach (cm): ", (System.Decimal)fighter["reach"]));
				if(fighter["height"] != null)dataPoints.Add(new DataChart("Height (cm): ", (System.Decimal)fighter["height"]));
				if(fighter["wins"] != null) dataPoints.Add(new DataChart("Wins: ", (int)fighter["wins"]));
				if(fighter["losses"] != null) dataPoints.Add(new DataChart("Losses: ", (int)fighter["losses"]));
				if (fighter["draws"] != null) dataPoints.Add(new DataChart("Draws: ", (int)fighter["draws"]));
				

				string strBDay = fighter["birth_date"]?.ToString().TrimEnd("12:00:00 AM".ToCharArray());

				if(fighter["birth_date"] != null) dataPoints.Add(new DataChart("Born: ", strBDay));
				if(fighter["birth_city"] != null)dataPoints.Add(new DataChart("City: ", fighter["birth_city"]));
				if(fighter["birth_state"] != null)dataPoints.Add(new DataChart("State: ", fighter["birth_state"]));
				if(fighter["birth_country"] != null) dataPoints.Add(new DataChart("Country: ", fighter["birth_country"]));
			}

			return dataPoints;
		}


		/// <summary>
		/// Gets the prediction of fighters fighting and returns them as a list of datachart type
		/// </summary>
		/// <returns></returns>
		//public List<DataChart> CreateFighterPrecitionChart()
		public void CreateFighterPrecitionChart()
		{
			//Variables
			List<DataChart> dataPoints = new List<DataChart>(); //The datapoints to return
			
			//Check that the current selections are not 0 and...
			if (currentSelectionB != 0 && currentSelectionA != 0)
			{
				//Get the data row from the database handler
				var predictionRow = DBHandler.GetFighterPrediction(currentSelectionA, currentSelectionB);

				//Check that the row is not null and...
				if(predictionRow != null)
				{
					//Try this, don't let it break
					try
					{
						//Set the predicted ID
						predictedID = (int)predictionRow["Winner_Fighter_ID"];

						//Set the predicted name
						predictedName = predictionRow["Winner_Name"].ToString();

						//Get the predicted certainties
						var winnerCertainty = (System.Decimal)predictionRow["Prediction_Certainty"] * 100;
						var loserCertainty = 100 - winnerCertainty;

						//Check for the winner and set the fighters variable
						if(predictedID == currentSelectionA)
						{
							fighterACertainty = Convert.ToDouble(winnerCertainty);
							fighterBCertainty = Convert.ToDouble(loserCertainty);
						}
						else if(predictedID == currentSelectionB)
						{
							fighterACertainty = Convert.ToDouble(loserCertainty);
							fighterBCertainty = Convert.ToDouble(winnerCertainty);
						}
						else //If neither selected fighter is the predicted winner...
						{
							//Not sure what to do, throw an exception I guess. This shouldnt happen. Maybe a tie?
							throw new Exception("FighterChart.cs: In CreateFighterPrecitionChart: Neither selected fighter is the winner");
						}
						

						//Add the winning fighters name to the prediction
						dataPoints.Add(new DataChart("Predicted Winner", predictedName));
						dataPoints.Add(new DataChart("Winner Certainty", winnerCertainty));
						dataPoints.Add(new DataChart("Loser Certainty", loserCertainty));


					} 
					//Catch any exception and...
					catch(Exception ex)
					{
						//Cleanup and reset the values
						dataPoints = new List<DataChart>();
						predictedID = 0;
						predictedName = "";
						fighterACertainty = 0;
						fighterBCertainty = 0;
					}



				}
				
			}

			dcPrediction = dataPoints;
			//return dataPoints;
		}




		/// <summary>
		/// Shifts the range of fighters for the fighter lists down
		/// </summary>
		/// <returns></returns>
		public int ShiftDown_A()
		{
			rangeIndexA -= itemCountA;

			if(rangeIndexA < 0) rangeIndexA = 0;
			else if (maxItems - itemCountA > 0)
			{
				//var cpyFightersByNameA = new Dictionary<string, int>();
				fightersA = DBHandler.GetFighters(out var cpyFightersByNameA, CreateFighterSelect_A());
				fightersByNameA = cpyFightersByNameA;
				//fightersA = DBHandler.GetFighters(out fightersByNameA, CreateFighterSelect_A());
			}

			return rangeIndexA;
		}


		/// <summary>
		/// Shifts the the range of fighters for the fighter lists down
		/// </summary>
		/// <returns></returns>
		public int ShiftDown_B()
		{
			rangeIndexB -= itemCountB;

			if(rangeIndexB < 0) rangeIndexB = 0;
			else if (maxItems - itemCountB < rangeIndexB && maxItems - itemCountB > 0)
			{
				fightersB = DBHandler.GetFighters(out var cpyFightersByNameB, CreateFighterSelect_B());
				fightersByNameB = cpyFightersByNameB;
			}

			return rangeIndexB;
		}


		/// <summary>
		/// Shifts the the range of fighters for the fighter lists up
		/// </summary>
		/// <returns></returns>
		public int ShiftUp_A()
		{
			rangeIndexA += itemCountA;


			if(itemCountA > maxItems)
			{
				rangeIndexA = maxItems- itemCountA;
			}
			else if(maxItems-itemCountA > rangeIndexA)
			{
				fightersA = DBHandler.GetFighters(out var cpyFightersByNameA, CreateFighterSelect_A());
				fightersByNameA = cpyFightersByNameA;
			}


			return rangeIndexA;
		}



		/// <summary>
		/// Shifts the the range of fighters for the fighter lists up
		/// </summary>
		/// <returns></returns>
		public int ShiftUp_B()
		{
			rangeIndexB += itemCountB;


			if(itemCountB > maxItems)
			{
				rangeIndexB = maxItems- itemCountB;
			}
			else if(maxItems-itemCountB > rangeIndexB)
			{
				//fightersB = DBHandler.GetFighters(out fightersByNameB, CreateFighterSelect_B())
				fightersB = DBHandler.GetFighters(out var cpyFightersByNameB, CreateFighterSelect_B());
				fightersByNameB = cpyFightersByNameB; ;
			}


			return rangeIndexB;
		}


		/// <summary>
		/// Creates the select string for fighter collection A
		/// </summary>
		/// <returns></returns>
		public string CreateFighterSelect_A()
		{
			return ("WHERE Fighter_ID >= " + rangeIndexA.ToString() + " AND Fighter_ID <= " + (rangeIndexA + itemCountA).ToString());
		}

		/// <summary>
		/// Creates the select string for fighter collection B
		/// </summary>
		/// <returns></returns>
		public string CreateFighterSelect_B()
		{
			return ("WHERE Fighter_ID >= " + rangeIndexB.ToString() + " AND Fighter_ID <= " + (rangeIndexB + itemCountB).ToString());
		}




	} //end class









} //end namespace