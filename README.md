Azure AD Reporting Api
=============
Azure graph API wrapper for retrieving active directory reporting information.

* [Introduction](#introduction)
* [Requirements](#requirements)
* [Samples](#samples)

## Introduction
Simple and light way solution for accessing active directory reporting information in .NET projects. Currently only implemented report **auditEvents** but will implement the missing reports and filters as soon as I required this functionality. This also includes the release of a Nuget Package.

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
var auditEvents = client.GetAuditEvents();

```

