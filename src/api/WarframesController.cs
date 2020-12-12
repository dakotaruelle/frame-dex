﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Api.DataUpdates.Warframe;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
  [ApiController]
  [Route("warframes")]
  public class WarframesController : ControllerBase
  {
    private readonly ILogger<WarframesController> _logger;

    public WarframesController(ILogger<WarframesController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public async Task<List<WarframeResponse>> Get()
    {
      try
      {
        var warframes = new List<Warframe>();

        using var httpClient = new HttpClient();
        var response = await httpClient.GetFromJsonAsync<List<WarframeResponse>>("https://api.warframestat.us/warframes");
        return response;

        // using (var connection = new SqlConnection("Server=localhost\\SqlExpress; Database=FrameDex; Trusted_connection=true"))
        // {
        //   string getWarframesSql = @"
        //     SELECT * FROM dbo.warframe
        //   ";

        //   warframes = connection.Query<Warframe>(getWarframesSql).ToList();
        // }

        // return warframes;
      }
      catch (Exception exception)
      {
        System.Console.WriteLine(exception);
        // return new List<WarframeResponse>();
        return new List<WarframeResponse>();
      }
    }
  }
}
