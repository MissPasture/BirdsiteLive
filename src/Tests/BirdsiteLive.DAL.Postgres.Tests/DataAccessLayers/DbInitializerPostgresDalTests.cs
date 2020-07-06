﻿using System;
using System.Threading.Tasks;
using BirdsiteLive.DAL.Postgres.DataAccessLayers;
using BirdsiteLive.DAL.Postgres.Tests.DataAccessLayers.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BirdsiteLive.DAL.Postgres.Tests.DataAccessLayers
{
    [TestClass]
    public class DbInitializerPostgresDalTests : PostgresTestingBase
    {
        [TestCleanup]
        public async Task CleanUp()
        {
            var dal = new DbInitializerPostgresDal(_settings, _tools);
            try
            {
                await dal.DeleteAllAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [TestMethod]
        public async Task GetCurrentDbVersionAsync_UninitializedDb()
        {
            var dal = new DbInitializerPostgresDal(_settings, _tools);
            
            var current = await dal.GetCurrentDbVersionAsync();
            Assert.IsNull(current);
        }

        [TestMethod]
        public async Task InitDbAsync()
        {
            var dal = new DbInitializerPostgresDal(_settings, _tools);

            await dal.InitDbAsync();
            var current = await dal.GetCurrentDbVersionAsync();
            var mandatory = dal.GetMandatoryDbVersion();
            Assert.IsNotNull(current);
            Assert.AreEqual(mandatory.Minor, current.Minor);
            Assert.AreEqual(mandatory.Major, current.Major);
        }
    }
}