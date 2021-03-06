﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureADReportingApi;
using AzureADReportingApi.Http;

namespace AzureADReportingApiTest
{
    /// <summary>
    /// Just some integration tests
    /// </summary>
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestConnection_ClientIdIsEmptyException_True()
        {
            try
            {
                AzureConnection.Create(
                    string.Empty,
                    "value2",
                    "value3"
                 );

                Assert.IsTrue(false, "No Exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestConnection_ClientSecretIsEmptyException_True()
        {
            try
            {
                AzureConnection.Create(
                    "value1",
                    string.Empty,
                    "value3"
                 );

                Assert.IsTrue(false, "No Exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestConnection_ClienTanentIsEmptyException_True()
        {
            try
            {
                AzureConnection.Create(
                    "value1",
                    "value2",
                    string.Empty
                 );

                Assert.IsTrue(false, "No Exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestReport_AuditEventsCallFakeClientFail_True()
        {
            var client = new AzureAdReportingClient(HelperFunctions.GetAzureConnectionFakeId());

            try
            {
                client.GetAuditEvents();

                Assert.IsTrue(false, "No Exception");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestReport_AuditEventsCallSuccess_True()
        {
            var client = new AzureAdReportingClient(HelperFunctions.GetAzureConnection());

            try
            {
                var task = client.GetAuditEvents();

                Assert.IsTrue(task.Result != null && task.Result.Success);
            }
            catch (Exception)
            {
                Assert.IsTrue(false, "Exception");
            }
        }

        [TestMethod]
        public void TestReport_AuditEventsCallSuccessWithFilter_True()
        {
            var client = new AzureAdReportingClient(HelperFunctions.GetAzureConnection());

            try
            {
                var task = client.GetAuditEvents(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(2));

                Assert.IsTrue(task.Result != null && task.Result.Success);
            }
            catch (Exception)
            {
                Assert.IsTrue(false, "Exception");
            }
        }

        [TestMethod]
        public void TestReport_ReportsCallSuccess_True()
        {
            var client = new AzureAdReportingClient(HelperFunctions.GetAzureConnection());

            try
            {
                var task = client.GetReports();

                Assert.IsTrue(task.Result != null && task.Result.Success);
            }
            catch (Exception)
            {
                Assert.IsTrue(false, "Exception");
            }
        }
    }
}
