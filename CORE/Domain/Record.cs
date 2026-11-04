namespace CORE.Domain
{
    // base class for entities and models
    public abstract class Record
    {
        public int Id { get; set; }

        protected Record(int id)
        {
            Id = id;
        }

        protected Record()
        {
        }
    }
}
