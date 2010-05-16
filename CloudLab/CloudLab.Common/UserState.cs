using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudLab.Common
{
    public class UserState
    {
        public static string CurrentProject { get; set; }
        public static List<string> projects = new List<string>();

        public static void AddProject(string project)
        {
            projects.Add(project);
        }

        public static List<string> GetProjects(string user)
        {
            return projects;
        }

        public static List<string> GetTasks(string project)
        {
            List<string> tasks = new List<string>();
            tasks.Add("Initial Download");
            return tasks;
        }
    }
}
