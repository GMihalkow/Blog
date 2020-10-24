namespace Blog.Web.Models.Common
{
    public class Alert
    {
        public Alert(AlertType type, string message)
        {
            this.AlertType = type;
            this.Message = message;
        }

        public AlertType AlertType { get; set;  }
        
        public string Message { get; set; }
    }
}