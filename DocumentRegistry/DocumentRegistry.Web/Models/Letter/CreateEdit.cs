using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocumentRegistry.Web.Models.Letter
{
    public class CreateEdit
    {
        [DisplayName("Numer ")]
        public int Id { get; set; }

        // [DisplayName("Firma do listów")]
        // public int PostCompanyId { get; set; }
        
        // public IEnumerable<SelectListItem> PostCompanies { get; set; }
        
        [DisplayName("Data")]
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        
        [DisplayName("Data odbioru")]
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReceiveDate { get; set; }
        
        [DisplayName("Zawartość")]
        public string Content { get; set; }

        [DisplayName("Pracownik")]
        public int EmployeeId { get; set; }
        
        [DisplayName("Firma")] 
        public int CompanyId { get; set; }
        
        [DisplayName("Typ dokumentu")] 
        public int DocumentTypeId { get; set; }

        public string DocumentTypeName { get; set; }

        [DisplayName("Dane dodatkowe")]
        public string Other { get; set; }
        [DisplayName("PR")]
        public bool PR { get; set; }
        [DisplayName("PO")]
        public bool PO { get; set; }
        [DisplayName("Adres - Nazwa firmy")]
        public string CompanyName { get; set; }
        [DisplayName("Adres - Ulica")]
        public string CompanyStreet { get; set; }
        [DisplayName("Adres - Miasto")]
        public string CompanyCity { get; set; }
        [DisplayName("Adres - Kod pocztowy")]
        public string CompanyPostalCode { get; set; }
        [DisplayName("Korespondecyjny - Nazwa")]
        public string CompanyPostName { get; set; }
        [DisplayName("Korespondecyjny - Ulica")]
        public string CompanyPostStreet { get; set; }
        [DisplayName("Korespondecyjny - Miasto")]
        public string CompanyPostCity { get; set; }
        [DisplayName("Korespondecyjny - Kod pocztowy")]
        public string CompanyPostPostalCode { get; set; }
        [DisplayName("Imię")]
        public string EmployeeFirstName { get; set; }
        [DisplayName("Nazwisko")]
        public string EmployeeLastName { get; set; }

        public string EmployeeFullName { get; set; }
        [DisplayName("Rodzaj")] 
        public int DocumentDirectionId { get; set; }
        public IEnumerable<DocumentDirection.DocumentDirection> DocumentDirections { get; set; }


        public static CreateEdit BuildForCreate()
        {
            return new()
            {
                Date = DateTime.Now,
                ReceiveDate = DateTime.Now
            };
        }
        public static CreateEdit BuildFromModel(Letter letter)
        {
            return new()
            {
                // PostCompanyId = letter.KeyPostCompanyId,
                Date = letter.Date,
                ReceiveDate = letter.ReceiveDate,
                Content = letter.Content,
                EmployeeId = letter.EmployeeId,
                CompanyId = letter.CompanyId,
                DocumentTypeId = letter.DocumentTypeId,
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
                DocumentDirectionId = letter.DocumentDirectionId,
                EmployeeFullName = letter.EmployeeFullName,
                DocumentTypeName = letter.DocumentTypeName,
            };
        }

        public Letter ToDomainModel()
        {
            return new()
            {
                Id = Id,
                Date = Date,
                ReceiveDate = ReceiveDate,
                Content = Content,
                EmployeeId = EmployeeId,
                EmployeeFirstName = EmployeeFirstName,
                EmployeeLastName = EmployeeLastName,
                DocumentTypeId = DocumentTypeId,
                CompanyId = CompanyId,
                Other = Other,
                PR = PR,
                PO = PO,
                CompanyName = CompanyName,
                CompanyStreet = CompanyStreet,
                CompanyCity = CompanyCity,
                CompanyPostalCode = CompanyPostalCode,
                CompanyPostName = CompanyPostName,
                CompanyPostStreet = CompanyPostStreet,
                CompanyPostCity = CompanyPostalCode,
                CompanyPostPostalCode = CompanyPostPostalCode,
                DocumentDirectionId = DocumentDirectionId
            };
        }
    }
}