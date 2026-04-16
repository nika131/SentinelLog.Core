# SentinelLog.Core

## Overview
SentinelLog.Core is a distributed security event logging system designed to securely log and manage security events across multiple systems using Windows Communication Foundation (WCF) and Entity Framework Core. It ingests and categorizes system alerts via a JSON-based microservice architecture.

## Features
- **Distributed Architecture**: Allows the logging of security events from different systems
- **WCF Integration**: Utilizes WCF for communication between logging clients and the central logging server
- **Entity Framework Core**: Uses EF Core for data access and ORM capabilities
- **JSON-based Microservice Architecture**: REST/JSON APIs for event ingestion
- **Scalability**: Designed to handle a growing number of logging sources
- **Security**: Implements best practices for securing data in transit and at rest
- **Event Categorization**: Automatic categorization and classification of security alerts
- **Audit Trail**: Complete audit trail of all security events

## Technologies Used
- **C#**: Primary language (100%)
- **WCF**: Windows Communication Foundation for service-oriented architecture
- **Entity Framework Core**: ORM and data access
- **SQL Server / SQLite**: Database options
- **.NET Framework**: Application framework

## Prerequisites
- .NET Framework 4.7.2 or higher / .NET 6.0+
- Visual Studio 2022 or compatible IDE
- SQL Server 2019 or higher / SQLite

   cd SentinelLog.Core
