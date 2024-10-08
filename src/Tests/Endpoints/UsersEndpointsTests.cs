﻿using JsonPlaceholderClient.API;
using System.Net;

namespace JsonPlaceholderClient.Tests.Endpoints;

public sealed class UsersEndpointsTests : IClassFixture<CustomWebApplicationFactory<APIEntryPoint>>
{
    private readonly HttpClient _Client;
    private const string SingleItemResponseContent = """
    {"id":1,"name":"Sonoda117","username":"sonoda117","email":"sonoda.117@fakemail.com","address":{"street":"Random Street","suite":"Random Suite","city":"Random City","zipcode":"99999","geo":{"lat":"RandomLat","lng":"RandomLng"}},"phone":"999-123-4567","website":"sonoda.117.org","company":{"name":"Fake Company","catchPhrase":"Fake Phrase","bs":"Something"}}
    """;
    private const string MultipleItemsResponseContent = """
    [{"id":1,"name":"Sonoda117","username":"sonoda117","email":"sonoda.117@fakemail.com","address":{"street":"Random Street","suite":"Random Suite","city":"Random City","zipcode":"99999","geo":{"lat":"RandomLat","lng":"RandomLng"}},"phone":"999-123-4567","website":"sonoda.117.org","company":{"name":"Fake Company","catchPhrase":"Fake Phrase","bs":"Something"}},{"id":2,"name":"Sonoda117","username":"sonoda117","email":"sonoda.117@fakemail.com","address":{"street":"Random Street","suite":"Random Suite","city":"Random City","zipcode":"99999","geo":{"lat":"RandomLat","lng":"RandomLng"}},"phone":"999-123-4567","website":"sonoda.117.org","company":{"name":"Fake Company","catchPhrase":"Fake Phrase","bs":"Something"}},{"id":3,"name":"Sonoda117","username":"sonoda117","email":"sonoda.117@fakemail.com","address":{"street":"Random Street","suite":"Random Suite","city":"Random City","zipcode":"99999","geo":{"lat":"RandomLat","lng":"RandomLng"}},"phone":"999-123-4567","website":"sonoda.117.org","company":{"name":"Fake Company","catchPhrase":"Fake Phrase","bs":"Something"}},{"id":4,"name":"Sonoda117","username":"sonoda117","email":"sonoda.117@fakemail.com","address":{"street":"Random Street","suite":"Random Suite","city":"Random City","zipcode":"99999","geo":{"lat":"RandomLat","lng":"RandomLng"}},"phone":"999-123-4567","website":"sonoda.117.org","company":{"name":"Fake Company","catchPhrase":"Fake Phrase","bs":"Something"}},{"id":5,"name":"Sonoda117","username":"sonoda117","email":"sonoda.117@fakemail.com","address":{"street":"Random Street","suite":"Random Suite","city":"Random City","zipcode":"99999","geo":{"lat":"RandomLat","lng":"RandomLng"}},"phone":"999-123-4567","website":"sonoda.117.org","company":{"name":"Fake Company","catchPhrase":"Fake Phrase","bs":"Something"}}]
    """;

    public UsersEndpointsTests(CustomWebApplicationFactory<APIEntryPoint> ApplicationFactory)
    {
        _Client = ApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_Returns200()
    {
        using var Response = await _Client.GetAsync("api/v1/users");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(MultipleItemsResponseContent, Content);
    }

    [Fact]
    public async Task GetById_WhenIdExists_Returns200()
    {
        using var Response = await _Client.GetAsync("api/v1/users/1");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(SingleItemResponseContent, Content);
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_Returns404()
    {
        using var Response = await _Client.GetAsync("api/v1/users/0");
        Assert.Equal(HttpStatusCode.NotFound, Response.StatusCode);
    }
}