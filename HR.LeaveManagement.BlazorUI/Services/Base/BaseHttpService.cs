﻿using Blazored.LocalStorage;

namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService
{
    protected IClient _client;
    protected readonly ILocalStorageService _localStorageService;
    public BaseHttpService(IClient client, ILocalStorageService localStorageService)
    {
        _client = client;
        _localStorageService = localStorageService;
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        switch (ex.StatusCode)
        {
            case 400:
                return new Response<Guid>()
                    { Message = "Invalid data was submitted", ValidationErrors = ex.Response, Success = false };
            case 404:
                return new Response<Guid>()
                    { Message = "The record was not found.", Success = false };
            default:
                return new Response<Guid>() { Message = "Something went wrong, please try again later.", Success = false };
        }
    }
}