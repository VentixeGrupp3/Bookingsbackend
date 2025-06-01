using Azure.Messaging.ServiceBus;
using Business.Models;
using Data.Entities;
using EventService.Protos;
using Infrastructure.Models;
using Microsoft.SqlServer.Server;
using System.Text.Json;

namespace Infrastructure.Services;

public class QueueService
{
    private readonly string _queueName = "invoice-service";
    private readonly ServiceBusClient _serviceBusClient;

    public QueueService(ServiceBusClient serviceBusClient)
    {
        _serviceBusClient = serviceBusClient;
    }

    public async Task<bool> SendToInvoice(Event request, BookingsEntity bookingsEntity)
    {
        try
        {
            var invoiceMsg = new InvoiceMessageDto
            {
                BookingId = bookingsEntity.BookingId,
                UserId = bookingsEntity.UserId,
                FirstName = bookingsEntity.BookingFirstName,
                LastName = bookingsEntity.BookingLastName,
                BookingAddress = $"{bookingsEntity.BookingStreetName} {bookingsEntity.BookingCity} {bookingsEntity.BookingPostalCode}",
                BookingEmail = bookingsEntity.BookingEmail,
                BookingPhone = bookingsEntity.BookingPhone,
                EventId = bookingsEntity.EventId,
                EventName = bookingsEntity.EventName,
                EventOwnerName = request.EventOwnerName,
                EventOwnerEmail = request.EventOwnerEmail,
                EventOwnerAddress = request.EventOwnerAddress,
                EventOwnerPhone = request.EventOwnerPhone,
                Tickets = bookingsEntity.Tickets
                    .Select(i => new InvoiceTicketDto
                    {
                        TicketCategory = i.TicketCategory,
                        Price = i.TicketPrice,
                        Quantity = i.TicketQuantity
                    })
                    .ToList()
            };

            var sender = _serviceBusClient.CreateSender(_queueName);

            var jsonBody = JsonSerializer.Serialize(invoiceMsg);
            var busMessage = new ServiceBusMessage(jsonBody);

            await sender.SendMessageAsync(busMessage);
            await sender.DisposeAsync();


            return true;
        }
        catch
        {
            return false;
        }
    }
}
