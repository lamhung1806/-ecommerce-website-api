namespace API.Data.EntityBase.Entities
{
    public class Contact : EntityBase
    {
        public int Id { get; set; }

        public string Phone { get; set; }

        public string NameUser { get; set; }

        public string Content { get; set; }
    }
}