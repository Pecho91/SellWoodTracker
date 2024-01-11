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
        public decimal MetricAmount
        {
            get => Math.Round(_metricAmount, 2);
            set => _metricAmount = value;
        }
       
        private decimal _metricAmount;

        /// <summary>
        /// Price of m3
        /// </summary>
        public decimal MetricPrice
        {
            get => Math.Round(_metricPrice, 2); 
            set => _metricPrice = value; 
        }

        private decimal _metricPrice; 

        /// <summary>
        /// Gross income (metric price * metric amount)
        /// </summary>
        public decimal GrossIncome 
        {
            get => Math.Round(_grossIncome, 2);
            set => _grossIncome = value;
        }

        private decimal _grossIncome;

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
