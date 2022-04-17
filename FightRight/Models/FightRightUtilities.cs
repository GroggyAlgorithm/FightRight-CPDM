using System;
using System.Collections.Generic;

namespace FightRight.Models
{

	/// <summary>
	/// Enum of possible weight class indices
	/// </summary>
	public enum WeightClasses
	{
		Strawweight,
		Flyweight,
		Bantamweight,
		Featherweight,
		Lightweight,
		Welterweight,
		Middleweight,
		Light_Heavyweight,
		Heavyweight
	};


	public static class FightRightUtilities
	{


		//Heavyweight: 265 lb (120.2 kg)
		//Light Heavyweight: 205 lb(102.1 kg)
		//Middleweight: 185 lb(83.9 kg)
		//Welterweight: 170 lb(77.1 kg)
		//Lightweight: 155 lb(70.3 kg)
		//Featherweight: 145 lb(65.8 kg)
		//Bantamweight: 135 lb(61.2 kg)
		//Flyweight: 125 lb(56.7 kg)
		//Strawweight: 115 lb(52.5 kg)


		/// <summary>
		/// List of possible weight class values, in Kg's
		/// </summary>
		public readonly static Dictionary<float, string> KgWeightClass = new Dictionary<float, string>()
		{
			{52.5f, "Strawweight"},
			{56.7f, "Flyweight"},
			{61.2f, "Bantamweight"},
			{65.8f, "Featherweight"},
			{70.3f, "Lightweight"},
			{77.1f, "Welterweight"},
			{83.9f, "Middleweight"},
			{102.1f, "Light-Heavyweight"},
			{120.2f, "Heavyweight" }
		};


		/// <summary>
		/// Returns the weight class max limit
		/// </summary>
		/// <param name="weightClass">The weight class to use</param>
		/// <returns></returns>
		//public static float GetWeightClassMaximum(WeightClasses weightClass) => KgWeightClass[weightClass]
	
	}

}


