using Business.Models;
using Business.Services;
using Data.Entities;
using EventService.Protos;
using System.Diagnostics;
using static Grpc.Core.Metadata;

namespace Business.Factories;

public class BookingFactory
{
    public static BookingModel ToModel(BookingsEntity entity )
    {
        if (entity == null) return null!;

        return new BookingModel
        {
            BookingId = entity.BookingId,
            BookingNumber = entity.BookingNumber,
            UserId = entity.UserId,
            BookingFirstName = entity.BookingFirstName,
            BookingLastName = entity.BookingLastName,
            BookingStreetName = entity.BookingStreetName,
            BookingCity = entity.BookingCity,
            BookingPostalCode = entity.BookingPostalCode,
            BookingEmail = entity.BookingEmail,
            BookingPhone = entity.BookingPhone,
            EventId = entity.EventId,
            EventName = entity.EventName,
            EventCategory = entity.EventCategory,
            EventDate = entity.EventDate,
            EventTime = entity.EventTime,

            Tickets = entity.Tickets
                .Select(t => new TicketModel
                {
                    TicketId = t.TicketId,
                    BookingId = t.BookingId,
                    TicketCategory = t.TicketCategory,
                    TicketPrice = t.TicketPrice,
                    TicketQuantity = t.TicketQuantity
                })
                .ToList()
        };
    }

    public static BookingsEntity ToEntity(UpdateBookingInformationForm formData)
    {
        return formData == null
        ? null!
        : new BookingsEntity
        {
            BookingId = formData.BookingId,
            BookingNumber = formData.BookingNumber,
            UserId = formData.UserId,
            BookingFirstName = formData.BookingFirstName,
            BookingLastName = formData.BookingLastName,
            BookingStreetName = formData.BookingStreetName,
            BookingCity = formData.BookingCity,
            BookingPostalCode = formData.BookingPostalCode,
            BookingEmail = formData.BookingEmail,
            BookingPhone = formData.BookingPhone,
            EventId = formData.EventId,
            EventName = formData.EventName,
            EventCategory = formData.EventCategory,
            EventDate = formData.EventDate,
            EventTime = formData.EventTime,
            Tickets = formData.Tickets?.Select(ticketForm => new TicketEntity
            {
                TicketId = ticketForm.TicketId,
                TicketCategory = ticketForm.TicketCategory,
                TicketPrice = ticketForm.TicketPrice,
                TicketQuantity = ticketForm.TicketQuantity
            }).ToList() ?? new List<TicketEntity>() 
        };
    }

    public static BookingsEntity ToEntity(Event request, AddBookingInformationForm formData, TicketEntity tickets)
    {

        var booking = new BookingsEntity
        {
            BookingId = Guid.NewGuid().ToString(),
            BookingNumber = GenerateBookingCode(),
            UserId = formData.UserId,
            BookingFirstName = formData.BookingFirstName,
            BookingLastName = formData.BookingLastName,
            BookingStreetName = formData.BookingStreetName,
            BookingCity = formData.BookingCity,
            BookingPostalCode = formData.BookingPostalCode,
            BookingEmail = formData.BookingEmail,
            BookingPhone = formData.BookingPhone,
            EventId = formData.EventId,
            EventName = request.EventName,
            EventCategory = request.EventCategory,
            EventDate = TryParseDate(request.EventDate),
            EventTime = TryParseDateTime(request.EventTime),
        };
        for (int i = 0; i < formData.TicketQuantity; i++)
        {
            booking.Tickets.Add(
            new TicketEntity
            {
                TicketId = Guid.NewGuid().ToString(),
                BookingId = booking.BookingId,
                TicketCategoryId = tickets.TicketCategoryId,
                TicketCategory = tickets.TicketCategory,
                TicketPrice = tickets.TicketPrice,
                TicketQuantity = tickets.TicketQuantity
            });
        }


        return booking;

    }

    public static string GenerateBookingCode()
    {
        string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
        string randomPart = Guid.NewGuid().ToString("N")[..8].ToUpper();
        return $"BN-{datePart}-{randomPart}";
    }

    private static DateTime? TryParseDate(string? date)
    {
        return DateTime.TryParse(date, out var result) ? result.Date : null;
    }

    private static DateTime? TryParseDateTime(string? dateTime)
    {
        return DateTime.TryParse(dateTime, out var result) ? result : null;
    }
}
