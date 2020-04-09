﻿using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class PersonCarCreateEditViewModel
    {
        public PersonCar PersonCar { get; set; } = default!;
        public SelectList? CarSelectList { get; set; }
        public SelectList? PersonSelectList { get; set; }
    }
}