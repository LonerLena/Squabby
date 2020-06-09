namespace Squabby.Helpers.Config
{
    public class Config
    {
        /// <summary>
        /// Determines connection string of database
        /// </summary>
        public string ConnectionString { get; set; }
        
        /// <summary>
        /// Determines server endpoints
        /// </summary>
        public string[] ServerEndPoints { get; set; }
    }
}