namespace API_Examen.DTO
{
    public class ActivityListDTO
    {
        public int Id { get; set; }
        public string Schedule { get; set; }
        public string Title { get; set; }
        public string CreatedAt { get; set; }
        public string Status { get; set; }
        public string Condition { get; set; }
        public PropertyDTO Property { get; set; }
        public List<int> Survey { get; set; }
    }



}
