using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MuseumOfFineArts.Tests
{
	[TestClass]
	public class MuseumUnitTests
	{
		[TestMethod]
		public void MuseumWithOneAnonymousPainting_GetCountOfAnonymousPaintings_ReturnsOne()
		{
			Museum m = new Museum();
			Painting p = new Painting
			{
				Name = "Painting with unknown artist",
				Artist = null,
			};
			m.Paintings.Add(p);
			Painting pp = new Painting
			{
				Name = "Painting with known artist",
				Artist = "Deadbeef",
			};
			m.Paintings.Add(pp);

			int count = m.GetCountOfAnonymousPaintings();

			Assert.AreEqual(1, count);
		}

		[TestMethod]
		public void WorthlessPainting_FinalPaintingValue_ReturnsZero()
		{
			Museum m = new Museum();
			Painting p = new Painting { Value = 0.0m };

			decimal actual = m.FinalPaintingValue(p);

			Assert.AreEqual(0.0m, actual);
		}

		[TestMethod]
		public void RarePaintingWithNonTrendingArtMarket_FinalPaintingValue_IsCorrect()
		{
			Museum m = new Museum();
			m.IsArtCurrentlyTrending = false;
			Painting p = new Painting { Value = 2.0m, Rarity = "rare" };

			decimal actual = m.FinalPaintingValue(p);

			Assert.AreEqual(4.8m, actual);
		}

		[TestMethod]
		public void UniquePaintingWithNonTrendingArtMarketAndPopularRarity_FinalPaintingValue_IsCorrect()
		{
			Museum m = new Museum();
			m.IsArtCurrentlyTrending = false;
			m.CurrentlyPopularRarity = "unique";
			Painting p = new Painting { Value = 4.0m, Rarity = "unique" };

			decimal actual = m.FinalPaintingValue(p);

			Assert.AreEqual(9.6m, actual);
		}
	}
}
