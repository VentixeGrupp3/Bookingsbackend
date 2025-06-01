# BookingServiceProvider

**BookingServiceProvider** is a RESTful API built with ASP.NET Core that allows clients to perform CRUD operations (Create, Read, Update, Delete) on ticket bookings. It leverages Entity Framework for data access and integrates with Azure Service Bus for message-based communication.

## Features

- Create new ticket bookings
- Retrieve existing bookings
- Update booking details
- Delete bookings
- Asynchronous messaging with Azure Service Bus

## Tech Stack

- **.NET Core (ASP.NET Core Web API)**
- **Entity Framework Core** – for database access
- **SQL Server** – primary data store
- **Azure Service Bus** – message-based integration

## API Overview

All endpoints follow REST conventions and return JSON-formatted responses.

## Azure Service Bus Integration
The API publishes booking events to an Azure Service Bus queue to support asynchronous processing and integration with Invoices.

## User Endpoints
─────────────────────────────────────────────────────────────────────────

 Method | Endpoint                                           | Description
 
--------+----------------------------------------------------+-------------------------------

 POST   | /api/booking/user-create                           | Create a booking
 
 GET    | /api/booking/user-bookings/{userId}                | Get all bookings by user ID
 
 GET    | /api/booking/user-bookingnumber/{bookingNumber}    | Get booking by booking number
 
 GET    | /api/booking/user-specific-id/{bookingId}          | Get specific booking by ID

## Admin Endpoints
─────────────────────────────────────────────────────────────────────────

 Method | Endpoint                                              | Description
 
--------+-------------------------------------------------------+-------------------------------

 POST   | /api/booking/admin-create                             | Create a booking
 
 DELETE | /api/booking/admin-delete/{bookingId}                 | Delete a booking by ID
 
 GET    | /api/booking/admin-get-all                            | Get all bookings
 
 GET    | /api/booking/admin-specific-id/{bookingId}            | Get specific booking by ID
 
 GET    | /api/booking/admin-bookingnumber/{bookingNumber}      | Get booking by booking number
 
 PUT    | /api/booking/admin-update/{bookingId}                 | Update an existing booking
