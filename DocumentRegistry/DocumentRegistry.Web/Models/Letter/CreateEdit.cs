using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocumentRegistry.Web.Models.Letter
{
    public class CreateEdit
    {
        public int Number { get; set; }

        [DisplayName("Firma do listów")]
        public int PostCompanyId { get; set; }
        
        // public IEnumerable<SelectListItem> PostCompanies { get; set; }
        
        [DisplayName("Data")]
        public DateTime Date { get; set; }
        
        [DisplayName("Data odbioru")]
        public DateTime ReceiveDate { get; set; }
        
        [DisplayName("Zawartość")]
        public string Content { get; set; }

        [DisplayName("Pracownik")]
        public int EmployeeId { get; set; }
        
        // public IEnumerable<SelectListItem> Employees { get; set; }

        [DisplayName("Firma")] 
        public int KeyCompanyId { get; set; }
        // public IEnumerable<SelectListItem> Companies { get; set; }

        [DisplayName("Typ dokumentu")] 
        public int DocumentTypeId { get; set; }
        // public IEnumerable<SelectListItem> DocumentTypes { get; set; }

        [DisplayName("Dane dodatkowe")]
        public string Other { get; set; }
        [DisplayName("Rodzaj listu")]
        public bool PR { get; set; }
        public bool PO { get; set; }
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


        public static CreateEdit BuildFromModel(Letter letter)
        {
            return new()
            {
                Number = letter.Number,
                PostCompanyId = letter.KeyPostCompanyId,
                Date = letter.Date,
                ReceiveDate = letter.ReceiveDate,
                Content = letter.Content,
                EmployeeId = letter.KeyEmployeeId,
                DocumentTypeId = letter.KeyDocumentTypeId,
                Other = letter.Other,
                PR = letter.PR,
                PO = letter.PO,
                CompanyName = letter.CompanyName,
                CompanyStreet = letter.CompanyStreet,
                CompanyCity = letter.CompanyCity,
                CompanyPostalCode = letter.CompanyPostalCode,
                CompanyPostName = letter.CompanyPostName,
                CompanyPostStreet = letter.CompanyPostStreet,
                CompanyPostCity = letter.CompanyPostCity,
                CompanyPostPostalCode = letter.CompanyPostPostalCode,
                EmployeeFirstName = letter.EmployeeFirstName,
                EmployeeLastName = letter.EmployeeLastName,
                Kind = letter.Kind
            };
        }
    }
}