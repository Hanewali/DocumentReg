﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocumentRegistry.Web.Models.Letter
{
    public class CreateEdit
    {
        public int Number { get; set; }

        public int CompanyId { get; set; }
        
        [DisplayName("Firma do listów")]
        public IEnumerable<SelectListItem> PostCompanies { get; set; }
        
        [DisplayName("Data")]
        public DateTime Date { get; set; }
        
        [DisplayName("Data odbioru")]
        public DateTime ReceiveDate { get; set; }
        
        [DisplayName("Zawartość")]
        public string Content { get; set; }
        public int KeyEmployeeId { get; set; }
        
        [DisplayName("Pracownik")]
        public IEnumerable<SelectListItem> Employees { get; set; }

        public int KeyCompanyId { get; set; }
        [DisplayName("Firma")] 
        public IEnumerable<SelectListItem> Companies { get; set; }


        public int KeyDocumentTypeId { get; set; }
        [DisplayName("Typ dokumentu")] 
        public IEnumerable<SelectListItem> DocumentTypes { get; set; }

        [DisplayName("Inne")]
        public string Other { get; set; }
        [DisplayName("PR")]
        public string PR { get; set; }
        [DisplayName("PO")]
        public string PO { get; set; }
        [DisplayName("Odbiorca - Nazwa firmy")]
        public string CompanyName { get; set; }
        [DisplayName("Odbiorca - Ulica")]
        public string CompanyStreet { get; set; }
        [DisplayName("Odbiorca - Miasto")]
        public string CompanyCity { get; set; }
        [DisplayName("Odbiorca - Kod pocztowy")]
        public string CompanyPostalCode { get; set; }
        [DisplayName("Adres korespondecyjny - Nazwa")]
        public string CompanyPostName { get; set; }
        [DisplayName("Adres korespondecyjny - Ulica")]
        public string CompanyPostStreet { get; set; }
        [DisplayName("Adres korespondecyjny - Miasto")]
        public string CompanyPostCity { get; set; }
        [DisplayName("Adres korespondecyjny - Kod pocztowy")]
        public string CompanyPostPostalCode { get; set; }
        [DisplayName("Imię")]
        public string EmployeeFirstName { get; set; }
        [DisplayName("Nazwisko")]
        public string EmployeeLastName { get; set; }
        [DisplayName("Rodzaj")]
        public string Kind { get; set; }
    }
}