﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace GuitarStarBackOffice.ServerSide;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService localStorage;

    public CustomAuthStateProvider(ILocalStorageService localStorage)
    {
        this.localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (!IsSessionLoaded)
            await LoadSession();

        if (!IsSessionStarted)
        {
            var anonymous = new ClaimsIdentity();
            return new AuthenticationState(new ClaimsPrincipal(anonymous));
        }

        var identity = new ClaimsIdentity("Authorize");
        identity.AddClaim(new Claim(ClaimTypes.Name, FullName));
        identity.AddClaim(new Claim(ClaimTypes.Role, Role));
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }


    public async Task StartSession(string fullName, string userName, string role)
    {
        await localStorage.SetItemAsync(FullNameKey, fullName);
        await localStorage.SetItemAsync(RoleKey, role);
        await localStorage.SetItemAsync(UsernameKey, userName);
        await localStorage.SetItemAsync(SessionStartedKey, true);

        FullName = fullName;
        Username = userName;
        Role = role;
        IsSessionStarted = true;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
    private async Task LoadSession()
    {
        FullName = await localStorage.GetItemAsync<string>(FullNameKey);
        Role = await localStorage.GetItemAsync<string>(RoleKey);
        Token = await localStorage.GetItemAsync<string>(TokenKey);
        Username = await localStorage.GetItemAsync<string>(UsernameKey);
        IsSessionStarted = await localStorage.GetItemAsync<bool>(SessionStartedKey);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        IsSessionLoaded = true;
    }
    public async Task FinishSession()
    {

        await localStorage.RemoveItemAsync(FullNameKey);
        await localStorage.RemoveItemAsync(RoleKey);
        await localStorage.RemoveItemAsync(UsernameKey);
        await localStorage.RemoveItemAsync(TokenKey);
        await localStorage.SetItemAsync(SessionStartedKey, false);


        FullName = default;
        Role = default;
        Username = default;
        Token = default;
        IsSessionStarted = default;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private const string FullNameKey = "admin.fullname";
    private const string UsernameKey = "admin.username";
    private const string RoleKey = "admin.userrole";
    private const string TokenKey = "admin.token";
    private const string SessionStartedKey = "admin.issessionstarted";



    #region Properties

    public Guid UserId { get; set; }

    /// <summary>
    ///     Полное имя пользователя
    /// </summary>
    public string FullName { get; private set; }


    /// <summary>
    ///     Логин пользователя
    /// </summary>
    public string Username { get; private set; }

    /// <summary>
    ///     Роль пользователя
    /// </summary>
    public string Role { get; private set; }

    /// <summary>
    ///     Токен
    /// </summary>
    public string Token { get; private set; }

    /// <summary>
    ///     Сессия начата?
    /// </summary>
    public bool IsSessionStarted { get; set; }

    /// <summary>
    ///     Сессия загружена
    /// </summary>
    private bool IsSessionLoaded { get; set; }

    #endregion Properties

}
