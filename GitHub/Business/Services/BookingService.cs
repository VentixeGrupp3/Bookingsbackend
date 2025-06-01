using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using EventService.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Business.Services;

public interface IBookingService
{
    Task<bool> CreateBookingAdminAsync(AddBookingInformationForm formData);
    Task<bool> CreateBookingUserAsync(AddBookingInformationForm formData);
    Task<bool> DeleteBookingAsync(string id);
    Task<IEnumerable<BookingModel>> GetAllBookings();
    Task<IEnumerable<BookingModel>> GetAllBookingsByUserIdAsync(string id);
    Task<BookingModel> GetBookingByBookingIdAsync(string bookingId);
    Task<BookingModel> GetBookingByBookingNumberAsync(string bookingNumber);
    Task<BookingModel> GetBookingByUserIdAsync(string userId);
    Task<bool> UpdateProjectAsync(UpdateBookingInformationForm formData);
}

public class BookingService : IBookingService
{

    private readonly IBookingRepository _bookingRepository;
    private readonly EventGrpcService.EventGrpcServiceClient _grpcClient;
    private readonly QueueService _queueService;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _http;


    public BookingService(IBookingRepository bookingRepository, EventGrpcService.EventGrpcServiceClient grpcClient, QueueService queueService, IConfiguration configuration, HttpClient http)
    {
        _bookingRepository = bookingRepository;
        _grpcClient = grpcClient;
        _queueService = queueService;
        _configuration = configuration;
        _http = http;
        _http.BaseAddress = new Uri("https://ticketservice-fec7e9f6gqbdh0cv.swedencentral-01.azurewebsites.net/api/");
    }

    public async Task<bool> CreateBookingAdminAsync(AddBookingInformationForm formData)
    {
        try
        {
            var adminApiKey = _configuration["ApiKeys:Admin"];

            var headers = new Metadata
            {
                { "x-api-key", adminApiKey! }
            };

            var callOptions = new CallOptions(headers);
            var request = await _grpcClient.GetEventByIdAsync(new GetEventRequest { EventId = formData.EventId }, headers);

            var tickets = await _http.GetFromJsonAsync<TicketEntity>($"ticket/{formData.TicketId}");

            if (tickets == null)
                return false;
            if (request == null)
                return false;
            if (formData == null)
                return false;

            var bookingEntity = BookingFactory.ToEntity(request.Event, formData, tickets);

            var result = await _bookingRepository.AddAsync(bookingEntity);

            if (result != false)
            {
                var invoiceQueued = await _queueService.SendToInvoice(request.Event, bookingEntity);
            }

            return result;

        }
        catch (Exception ex)
        {
            Debug.Write(ex.Message);
            return false;
        }


    }

    public async Task<bool> CreateBookingUserAsync(AddBookingInformationForm formData)
    {
        try
        {

            var userApiKey = _configuration["ApiKeys:User"];

            var headers = new Metadata
            {
                { "x-api-key", userApiKey! }
            };

            var callOptions = new CallOptions(headers);
            var request = await _grpcClient.GetEventByIdAsync(new GetEventRequest { EventId = formData.EventId }, headers);

            var tickets = await _http.GetFromJsonAsync<TicketEntity>($"ticket/{formData.TicketId}");

            if (tickets == null)
                return false;
            if (request == null)
                return false;
            if (formData == null)
                return false;

            var bookingEntity = BookingFactory.ToEntity(request.Event, formData, tickets);

            var result = await _bookingRepository.AddAsync(bookingEntity);

            if (result != false)
            {
                var invoiceQueued = await _queueService.SendToInvoice(request.Event, bookingEntity);
            }

            return result;

        }
        catch (Exception ex)
        {
            Debug.Write(ex.Message);
            return false;
        }


    }

    public async Task<bool> DeleteBookingAsync(string id)
    {
        var result = await _bookingRepository.DeleteAsync(x => x.BookingId == id);
        return result;
    }

    public async Task<IEnumerable<BookingModel>> GetAllBookings()
    {
        var entities = await _bookingRepository.GetAllAsync(
            orderByDescending: true,
            sortBy: x => x.BookingCreated,
            filterBy: null,
            i => i.Tickets);

        return entities.Select(BookingFactory.ToModel);
    }

    public async Task<IEnumerable<BookingModel>> GetAllBookingsByUserIdAsync(string id)
    {
        var entities = await _bookingRepository.GetAllAsync(
            orderByDescending: true,
            sortBy: null,
            filterBy: x => x.UserId == id,
            i => i.Tickets);


        return entities.Select(BookingFactory.ToModel);
    }

    public async Task<BookingModel> GetBookingByBookingIdAsync(string bookingId)
    {
        var entity = await _bookingRepository.GetAsync(
            x => x.BookingId == bookingId,
            i => i.Tickets);
        return BookingFactory.ToModel(entity);
    }

    public async Task<BookingModel> GetBookingByBookingNumberAsync(string bookingNumber)
    {
        var entity = await _bookingRepository.GetAsync(
            x => x.BookingNumber == bookingNumber,
            i => i.Tickets);
        return BookingFactory.ToModel(entity);
    }

    public async Task<BookingModel> GetBookingByUserIdAsync(string userId)
    {
        var entity = await _bookingRepository.GetAsync(
            x => x.BookingId == userId,
            i => i.Tickets);
        return BookingFactory.ToModel(entity);
    }

    public async Task<bool> UpdateProjectAsync(UpdateBookingInformationForm formData)
    {
        if (formData == null)
            return false;

        if (!await _bookingRepository.ExistsAsync(x => x.BookingId == formData.BookingId))
            return false;

        var bookingEntity = BookingFactory.ToEntity(formData);
        var result = await _bookingRepository.UpdateAsync(bookingEntity);

        return result;
    }

} 
