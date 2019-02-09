namespace HackKU2019.Models
{
    public class Warning
    {
        public IContent Content { get; set; }
        public WarningSeverity Severity { get; set; }
    }
    
    public enum WarningSeverity
    {
        Warning,
        Danger
    }
}