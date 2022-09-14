﻿using Application.Guests.DTOs;
using Application.Guests.Ports;
using Application.Guests.Requests;
using Application.Guests.Responses;
using Domain.Exceptions;
using Domain.Ports;

namespace Application;
public class GuestManager : IGuestManager
{
    private IGuestRepository _guestRepository;
    public GuestManager(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }
    public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
    {
        try
        {
            if(request.Data != null)
            {
                var guest = GuestDTO.MapToEntity(request.Data);
                await guest.Save(_guestRepository);

                request.Data.Id = guest.Id;

                return new GuestResponse
                {
                    Data = request.Data,
                    Success = true
                };
            }
            throw new MissingFieldException();
        }
        catch (InvalidPersonDocumentIdException)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.INVALID_PERSON_ID,
                Message = "The ID passed is not valid"
            };
        }
        catch (InvalidEmailException)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.INVALID_EMAIL,
                Message = "The email is invalid"
            };
        }
        catch (MissingRequiredInformationException)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                Message = "Missing required fields"
            };
        }
        catch (System.Exception)
        {
            return new GuestResponse
            {
                Success = false,
                ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA,
                Message = "There was an error when saving on DB"
            };
        }
    }
}
