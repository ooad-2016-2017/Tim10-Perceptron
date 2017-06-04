﻿using InteraktivnaMapaEvenata.Services.Interfaces;
using InteraktivnaMapaEvenata.UWP.Models;
using InteraktivnaMapaEvenata.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections;
using InteraktivnaMapaEvenata.Helpers;
using System.Windows.Input;

namespace InteraktivnaMapaEvenata.ViewModels
{
    public class AdminVM : BindableBase
    {
        public AuthenticationVM AuthenticationVM { get; set; }

        public ObservableRangeCollection<Event> Events { get; set; } = new ObservableRangeCollection<Event>();

        public ObservableRangeCollection<Owner> Owners { get; set; } = new ObservableRangeCollection<Owner>();

        public ObservableRangeCollection<Customer> Customers { get; set; } = new ObservableRangeCollection<Customer>();

        public ObservableRangeCollection<Flag> Flags { get; set; } = new ObservableRangeCollection<Flag>();

        public IEventService _eventService { get; set; }

        public IOwnerService _ownerService { get; set; }

        public ICustomerService _customerService { get; set; }


        public AdminVM(IEventService eventService,
            IOwnerService ownerService,
            ICustomerService customerService)
        {
            _eventService = eventService;
            _ownerService = ownerService;
            _customerService = customerService;
        }

        public async Task LoadData()
        {
            Events.AddRange(await _eventService.GetEvents());
            Owners.AddRange(await _ownerService.GetOwners());
            Customers.AddRange(await _customerService.GetCustomers());
        }
    }
}
