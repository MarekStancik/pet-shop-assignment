using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApp.Core.Entities
{
    public class Filter
    {

        public int ItemsPrPage { get; set; }
        public int CurrentPage { get; set; }

        public string SortBy  { get; set; }

        public string SortOrder { get; set; }
    }
}
