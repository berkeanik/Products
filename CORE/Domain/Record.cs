namespace CORE.Domain
{
    // Base class for all entities and models, every record has an Id.
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
