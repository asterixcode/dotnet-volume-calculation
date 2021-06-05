namespace VolumeWebService.Models
{
    public class VolumeResult
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public decimal Height { get; set; }
        public decimal Radius { get; set; }
        public double Volume { get; set; }
    }
}