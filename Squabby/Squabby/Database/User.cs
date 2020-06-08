using System.ComponentModel.DataAnnotations.Schema;

namespace Squabby.Database
{
    public class User
    {
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string AnonUserKey { get; set; }
    }
}