using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SM.TapTapTap;
using SM.TapTapTap.Interfaces;

namespace TapTapTap.Test
{
	[TestClass]
	public class TapRecorderTests
	{
		[TestMethod]
		public void TapRecorder_Construct()
		{
			var test = CreateTestObject();

			Assert.IsNotNull(test);
		}

		[TestMethod]
		public void TapRecorder_Clear_ClearsAllTaps()
		{
			var test = CreateTestObject();

			test.Press();

			Assert.AreEqual(1, test.AllTaps.Count());

			test.Clear();

			Assert.AreEqual(0, test.AllTaps.Count());
		}

		[TestMethod]
		public void TapRecorder_Press_CreatesATap()
		{
			var test = CreateTestObject();

			test.Press();

			Assert.AreEqual(1, test.AllTaps.Count());
			Assert.IsNull(test.AllTaps.First().Released);
		}

		[TestMethod]
		public void TapRecorder_Press_CreatesACodedTap()
		{
			var test = CreateTestObject();

			test.Press("A");
			test.Press();

			Assert.AreEqual(2, test.AllTaps.Count());
			Assert.AreEqual("A", test.AllTaps.First().Code);
		}

		[TestMethod]
		public void TapRecorder_Release_SetCodedReleaseTime()
		{
			var test = CreateTestObject();

			test.Press();
			test.Press("A");
			test.Press();
			test.Release("A");

			Assert.IsNotNull(test.AllTaps.FirstOrDefault(tap => tap.Code == "A").Released);
		}

		[TestMethod]
		public void TapRecorder_Release_SetsReleaseTime()
		{
			var test = CreateTestObject();

			test.Press();
			test.Release();

			Assert.IsNotNull(test.AllTaps.First().Released);
		}

		private ITapRecorder CreateTestObject()
		{
			return new TapRecorder();	
		}
	}
}