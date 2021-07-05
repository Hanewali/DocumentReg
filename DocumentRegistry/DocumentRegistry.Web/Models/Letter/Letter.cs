using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DocumentRegistry.Web.Controllers;

namespace DocumentRegistry.Web.Models.Letter
{
    public class Letter
    {
        [DisplayName("Numer")]
        public int Id { get; set; }

        [DisplayName("Data")]
        public DateTime Date { get; set; }

        public string DateString => Date.ToShortDateString();
        [DisplayName("Data odbioru")]
        public DateTime ReceiveDate { get; set; }

        public string ReceiveDateString => ReceiveDate.ToShortDateString();
        [DisplayName("Zawartość")]
        public string Content { get; set; }
        public int EmployeeId { get; set; }
        [DisplayName("Pracownik")]
        public string EmployeeFullName { get; set; }

        public int CompanyId { get; set; }
        [DisplayName("Firma")] 
        public int DocumentTypeId { get; set; }
        [DisplayName("Typ")] 
        public string DocumentTypeName { get; set; }
        [DisplayName("Dane dodatkowe")]
        public string Other { get; set; }
        public bool PR { get; set; }

        public string PRText => PR ? "Tak" : "Nie";        
        public bool PO { get; set; }
        public string POText => PO ? "Tak" : "Nie";
        [DisplayName("Odbiorca - Nazwa firmy")]
        public string CompanyName { get; set; }
        [DisplayName("Odbiorca - Ulica")]
        public string CompanyStreet { get; set; }
        [DisplayName("Odbiorca - Miasto")]
        public string CompanyCity { get; set; }
        [DisplayName("Odbiorca - Kod pocztowy")]
        [DataType(DataType.PostalCode)]
        public string CompanyPostalCode { get; set; }
        [DisplayName("Adres korespondecyjny - Nazwa")]
        public string CompanyPostName { get; set; }
        [DisplayName("Adres korespondecyjny - Ulica")]
        public string CompanyPostStreet { get; set; }
        [DisplayName("Adres korespondecyjny - Miasto")]
        public string CompanyPostCity { get; set; }
        [DisplayName("Adres korespondecyjny - Kod pocztowy")]
        [DataType(DataType.PostalCode)]
        public string CompanyPostPostalCode { get; set; }
        [DisplayName("Imię")]
        public string EmployeeFirstName { get; set; }
        [DisplayName("Nazwisko")]
        public string EmployeeLastName { get; set; }
        public int DocumentDirectionId { get; set; }
        [DisplayName("Rodzaj")] 
        public string DocumentDirectionName { get; set; }
    }
}