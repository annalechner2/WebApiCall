﻿using System.Collections.Generic;

namespace WebApiCall.Models
{
  // forwards returns the lat long
  public class Forward
  {
    public List<LatLong> data { get; set; }
  }
  public class LatLong
  {
    public double latitude { get; set; }
    public double longitude { get; set; }
    public string type { get; set; }
    public string name { get; set; }
    public string number { get; set; }
    public string postal_code { get; set; }
    public string street { get; set; }
    public int confidence { get; set; }
    public string region { get; set; }
    public string region_code { get; set; }
    public string county { get; set; }
    public string locality { get; set; }
    public object administrative_area { get; set; }
    public string neighbourhood { get; set; }
    public string country { get; set; }
    public string country_code { get; set; }
    public string continent { get; set; }
    public string label { get; set; }
  }


}
