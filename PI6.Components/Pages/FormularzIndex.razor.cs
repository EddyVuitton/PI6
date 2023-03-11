﻿using Microsoft.AspNetCore.Components;
using PI6.Shared.Data.Dtos;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Services;

namespace PI6.Components.Pages;

public partial class FormularzIndex
{
    [Inject] public IApplicationService ApplicationService { get; set; }

    private List<FormularzKafelekDto> _formTiles;

    protected override async Task OnInitializedAsync()
    {
        _formTiles = await ApplicationService.PobierzFormularzKafelekDto();
    }
}