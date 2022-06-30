using System;

namespace WebAPI.Models
{
    public class JobSearch
    {
      
         public SearchParameters search_parameters { get; set; }

          public List<JobsResult> jobs_results { get; set; }

        

    }
}

