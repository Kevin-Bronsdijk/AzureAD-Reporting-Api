Azure AD Reporting Api
=============
Azure graph API wrapper for retrieving active directory reporting information only.

* [Introduction](#introduction)
* [Requirements](#requirements)
* [Samples](#samples)

## Introduction
Simple and light way solution of accessing active directory reporting information in .NET projects. Currently only **auditEvents** has been implemented and will implement the other missing reports and filters as soon as I required this functionality.

### Requirements

Active Directory application and service principal - [details](https://azure.microsoft.com/en-us/documentation/articles/resource-group-create-service-principal-portal/

### Samples

**Request:**

```C#
using AzureADReportingApi;
using AzureADReportingApi.Http;
using AzureADReportingApi.Models;

var connection = AzureConnection.Create(
                "clientId",
                "clientSecret",
                "tenantDomain"
                );

var client = new AzureAdReportingClient(connection);
var auditEvents = test.GetAuditEvents();

```

