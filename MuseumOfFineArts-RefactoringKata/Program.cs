/*
 * Museum Curator Refactoring Kata
 * It's your first day on the job as museum curator at the Fowler Museum of Fine Arts.
 * Your job involves keeping an eye on the value of all the museums paintings, a value which depends on certain circumstances in the art market
 * and the rarity of each individual painting. Luckily, your predecessor wrote up this C# program to make some solid assumptions for you.
 * 
 * You've inputed a few paintings (the ones defined in Main()) but still aren't sure how this tool even works. 
 * The code must be refactored!
 *
 * [] Replace Primitive with Object
 * [] Encapsulate Collection
 * [] Replace Temp with Query
 * [] Extract Class
 * [] Substitute Algorithm
 * [] BONUS: Encapsulate Record 
 *
*/

using System;
using System.Collections.Generic;

namespace MuseumOfFineArts
{
	public class Painting
	{
		public string Name { get; set; }
		public string Artist { get; set; }
		public decimal Value { get; set; }
		public string Rarity { get; set; } = "common";
		public Dictionary<string, (string, decimal)> TransactionLog { get; set; } = new Dictionary<string, (string, decimal)>();
	}

	public class Museum
	{
		public List<Painting> Paintings { get; set; } = new List<Painting>();

		// art market conditions
		public bool IsArtCurrentlyTrending { get; set; }
		public float ArtMarketSaturation { get; set; } = 1.0f;
		public string CurrentlyPopularRarity { get; set; } = "very_rare";

		public decimal FinalPaintingValue(Painting p)
		{
			decimal rarityModifier = 1.0m;
			if (p.Rarity == "very_common")
			{
				rarityModifier = 0.5m;
			}
			else if (p.Rarity == "unique")
			{
				rarityModifier = 1.5m;
			}
			else if (p.Rarity == "rare")
			{
				rarityModifier = 3.0m;
			}

			if (p.Rarity == CurrentlyPopularRarity)
			{
				rarityModifier *= 2.0m;
			}

			decimal artMarketModifier;
			if (!IsArtCurrentlyTrending)
			{
				artMarketModifier = 0.8m;
			}
			else
			{
				artMarketModifier = 1.25m - (decimal)ArtMarketSaturation;
			}

			decimal val = p.Value * rarityModifier * artMarketModifier;
			return val;
		}

		public void DisplayPaintingValues()
		{
			foreach (Painting p in Paintings)
			{
				decimal value = FinalPaintingValue(p);
				Console.WriteLine($"{p.Name} ({p.Rarity}) is worth {value} zorknids and has transfered hands {p.TransactionLog.Count} times");
			}
		}

		public int GetCountOfAnonymousPaintings()
		{
			int count = 0;
			foreach (Painting p in Paintings)
			{
				if (string.IsNullOrWhiteSpace(p.Artist))
				{
					count += 1;
				}
			}

			return count;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Museum museum = new Museum();

			Painting abstractPainting = new Painting
			{
				Name = "Bold and Brash",
				Artist = "Squidward",
				Value = 100.0m,
				TransactionLog =
			{
				{ "08/15/1997", ("sold to Bob Binley", 1000) },
				{ "08/18/1997", ("sold to Larry Schmelton", 2500) },
				{ "02/05/2004", ("sold to museum", 100) }
			}
			};
			museum.Paintings.Add(abstractPainting);

			Painting lostPainting = new Painting
			{
				Name = "The Eating of The Cheeseburger",
				Value = 275000.0m,
				Rarity = "rare",
				TransactionLog =
			{
				{ "06/02/2006", ("sold to museum", 100000) }
			}
			};
			museum.Paintings.Add(lostPainting);

			Painting anotherPainting = new Painting
			{
				Name = "Day Eagles",
				Artist = "Edward Bopper",
				Value = 500000.0m,
				Rarity = "very_rare",
				TransactionLog =
			{
				{ "08/15/1994", ("sold to Jenny Downloadface", 50000) },
				{ "02/05/2010", ("sold to museum", 150000) }
			}
			};
			museum.Paintings.Add(anotherPainting);

			museum.DisplayPaintingValues();

			// change some market values and check again
			museum.IsArtCurrentlyTrending = true;
			museum.ArtMarketSaturation = 0.4f;

			museum.DisplayPaintingValues();

			int anonymousCount = museum.GetCountOfAnonymousPaintings();
			Console.WriteLine($"There are {anonymousCount} anonymous paintings in the museum");
		}
	}
}
