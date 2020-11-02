using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class FormCustomerViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Display(Name = "Membership Type")]
        public byte MemberShipTypeId { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
        public FormCustomerViewModel()
        {
            Id = 0;
        }

        public FormCustomerViewModel(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            BirthDate = customer.BirthDate;
            MemberShipTypeId = (byte)customer.MemberShipTypeId;
        }
    }
}