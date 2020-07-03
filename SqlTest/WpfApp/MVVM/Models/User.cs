using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.MVVM.Models;
using WpfApp.Utils;

namespace SqlTest.Data
{ 
    public enum Sex { Male, Female} 
    public class User
    {
        public List<ProgressChart> ProgressChart { get;  set; }
        /// <summary>
        /// Упражнениея на день
        /// </summary>
        public List<Exercise> ExerciseList { get;  set; }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Name { set; get; }
        public string Email { get; set; }
        public int Age { set; get; } 
        public Sex Gender { set; get; }
        public double Mass { get; set; }
        public double Height { get; set; } 

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
