namespace Mashup.Data.Model.dbo
{
    using System.Collections.Generic;

    public class Users
    {
        public int ID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public virtual List<RSS> RSS { get; set; }

        public virtual List<Weather> Weather { get; set; } 

    }
}