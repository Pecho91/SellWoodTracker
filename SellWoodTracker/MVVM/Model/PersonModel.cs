using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.MVVM.Model
{
    public class PersonModel
    {
        /// <summary>
        /// The unique identifier for the person
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The first name of the person
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the person
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// The primary email address of the person
        /// </summary>
        public string? EmailAddress { get; set; }

        /// <summary>
        /// The primary cell phone number of the person
        /// </summary>
        public string? CellphoneNumber { get; set; }
        /// <summary>
        /// Date order
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// Amount of m3 that person order
        /// </summary>
        public decimal MetricAmount { get; set; }

        /// <summary>
        /// Price of m3
        /// </summary>
        public decimal MetricPrice { get; set; }

        public decimal GrossIncome { get; set; }

        public PersonModel() 
        {   
            Date = DateTime.Now;
        }

        //public string FullName
        //{
        //    get
        //    {
        //        return $"{FirstName} {LastName}";
        //    }
        //}

    }
}
