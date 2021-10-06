using KomodoOutings.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KomodoOutings.Tests
{
    [TestClass]
    public class OutingRepoTests
    {
        OutingRepo repo = new OutingRepo();

        [TestMethod]
        public void AddToList_OutingExists_True()
        {
            Outing outing = new Outing(Outing.Event.AmusementPark, 1, DateTime.Now, 11.11m);
            repo.AddToList(outing);
            CollectionAssert.Contains(repo.GetList(), outing);
        }
        [TestMethod]
        public void GetList_OutingExists_True()
        {
            Outing outing = new Outing(Outing.Event.AmusementPark, 1, DateTime.Now, 11.11m);
            repo.AddToList(outing);
            CollectionAssert.Contains(repo.GetList(), outing);
        }
        [TestMethod]
        public void GetList_OutingDoesNotExist_False()
        {
            Outing outing = new Outing(Outing.Event.AmusementPark, 1, DateTime.Now, 11.11m);
            CollectionAssert.DoesNotContain(repo.GetList(), outing);
        }
        [TestMethod]
        public void GetOutingsByEvent_OutingDoesExist_True()
        {
            Outing outing = new Outing(Outing.Event.AmusementPark, 1, DateTime.Now, 11.11m);
            repo.AddToList(outing);
            CollectionAssert.Contains(repo.GetOutingsByEvent((int)outing.EventType), outing);
        }
        [TestMethod]
        public void GetTotalCost_OutingsDoExist()
        {
            Outing outing = new Outing(Outing.Event.AmusementPark, 1, DateTime.Now, 11.11m);
            Outing outing1 = new Outing(Outing.Event.AmusementPark, 1, DateTime.Now, 11.11m);
            repo.AddToList(outing);
            repo.AddToList(outing1);
            Assert.AreEqual(repo.GetTotalCost(repo.GetList()), 22.22m);
        }
    }
}
