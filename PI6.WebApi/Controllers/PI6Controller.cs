﻿using Microsoft.AspNetCore.Mvc;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Repositories;

namespace PI6.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PI6Controller : Controller
{
    private readonly IApplicationRepository _applicationRepository;

    public PI6Controller(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    [HttpGet("GetForms")]
    public async Task<ActionResult<List<formularz>>> GetForms()
    {
        return Ok(await _applicationRepository.GetForms());
    }


    [HttpGet("GetForm")]
    public async Task<ActionResult<List<formularz>>> GetForm(int for_id)
    {
        return Ok(await _applicationRepository.GetForm(for_id));
    }

    [HttpGet("GetFormType")]
    public async Task<ActionResult<List<formularz_typ>>> GetFormType()
    {
        return Ok(await _applicationRepository.GetFormType());
    }

    [HttpGet("GetFormTileDto")]
    public async Task<ActionResult<List<FormularzKafelekDto>>> GetFormTileDto()
    {
        return Ok(await _applicationRepository.GetFormTileDto());
    }

    [HttpPost("CreateForm")]
    public async Task CreateForm(FormularzDto form)
    {
        await _applicationRepository.CreateForm(form);
    }

    [HttpGet("GetFormQuestions")]
    public async Task<ActionResult<List<formularz_pytanie>>> GetFormQuestions(int for_id)
    {
        return Ok(await _applicationRepository.GetFormQuestions(for_id));
    }

    [HttpGet("GetFormOptions")]
    public async Task<ActionResult<List<formularz_pytanie_opcja>>> GetFormOptions(int for_id)
    {
        return Ok(await _applicationRepository.GetFormOptions(for_id));
    }

    [HttpPost("SaveSolvedForm")]
    public async Task SaveSolvedForm(FormularzPodejscieDto solvedForm)
    {
        await _applicationRepository.SaveSolvedForm(solvedForm);
    }
}