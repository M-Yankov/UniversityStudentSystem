namespace UniversityStudentSystem.Web.Models.Solutions
{
    using System.Collections.Generic;
    using Data.Models;

    public class SolutionEqialityComparer : IEqualityComparer<Solution>
    {
        public bool Equals(Solution firstSolution, Solution secondSolution)
        {
            return firstSolution.User.UserName == secondSolution.User.UserName;
        }

        public int GetHashCode(Solution solution)
        {
            return solution.User.UserName.GetHashCode();
        }
    }
}