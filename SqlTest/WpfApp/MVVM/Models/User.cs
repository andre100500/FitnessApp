using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlTest.Data
{
    public enum Sex { Male, Female} 
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Name { set; get; }
        public string Email { get; set; }
        public int Age { set; get; }
        public DateTime Date { get; set; }
        public Sex Gender { set; get; }
        public double Mass { get; set; }
        public double Height { get; set; }
    }
}
