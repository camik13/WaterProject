using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterProject.Models
{
    public interface IWaterProjectRepository //an interface is NOT a class, rather it is a template for a class, this template will ensure the class is used correctly
    {
        //class set up specifically for querying data
        IQueryable<Project> Projects { get; } //get only because you can read from this data but not add to it (set)

        public void SaveProject(Project p);
        public void CreateProject(Project p);
        public void DeleteProject(Project p);
    }
}
