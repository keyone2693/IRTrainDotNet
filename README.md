# IRTrainWebService

Iran Railway-Trains Service Package

Webservice package for iran Railway-Trains api on Raja-Fadak-Safir website

## Development 

### Before posting new issues: [Test samples](https://github.com/keyone2693/IRTrainDotNet/tree/master/TestExample)

Note that: you should register on Fadak-Raja-Safir website and get your username and password to using this package
also its possible to just use one of these services(Fadak-Raja-Safir) :)

[![Build status](https://img.shields.io/appveyor/ci/keyone2693/IRTrainDotNet.svg)](https://ci.appveyor.com/project/keyone2693/irtraindotnet)
[![NuGet](https://img.shields.io/nuget/v/IRTrainDotNet.svg)](https://www.nuget.org/packages/IRTrainDotNet/)
[![GitHub issues](https://img.shields.io/github/issues/keyone2693/IRTrainDotNet.svg?maxAge=25920?style=plastic)](https://img.shields.io/github/issues/keyone2693/IRTrainDotNet)
[![GitHub stars](https://img.shields.io/github/issues/keyone2693/IRTrainDotNet.svg?maxAge=25920?style=plastic)](https://img.shields.io/github/stars/keyone2693/IRTrainDotNet)
[![GitHub license](https://img.shields.io/npm/l/IRTrainDotNet.svg?maxAge=25920?style=plastic)](https://img.shields.io/github/license/keyone2693/IRTrainDotNet)

#### Current version: 1.9.x [Stable]
In this version:
you have the "Fadak" service completely good to go [Stable]
and you can use it if you had the username-password

The "Raja" system under development [Not Stable !!!]
the only thing still not working is two or three methods of Raja system, that's it !!!

and the "Safir" system I am working on it [Not Implemented Yet :(]

## Overview

## Cross-platform by design
you can use it in both .net core and .net framework 
its use .net standard

## Easy to install
Use library as dll, reference from [nuget](https://www.nuget.org/packages/IRTrainDotNet/)
or just use this in package manager console
```c#
Install-Package IRTrainDotNet
```

## Features
Currently the library supports following method:

You can use these methods both Async and nonAsync :)
***
- Login
- ValidateTokenWithTime
- ValidateTokenWithRequest
- GetStations
- GetStationById
- GetLastVersion
- GetWagonAvailableSeatCount
- LockSeat
- LockSeatBulk
- UnlockSeat
- SaveTicketsInfo
- RegisterTickets
- TicketReportA
- RefundTicketInfo
- RefundTicket
- UserSales
- AgentCredit
***
# Wiki (In Persian sorry other languages 😁)

[Documentation](https://github.com/keyone2693/IRTrainDotNet/wiki)

# License

MIT

