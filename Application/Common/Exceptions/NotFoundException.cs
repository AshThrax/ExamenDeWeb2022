namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base() { }

        public NotFoundException(string message)
         : base(message) { }

        public NotFoundException(string message, int id) : this(message)
        {
            Id = id;
        }
        public NotFoundException(string name, object key):
            base($"Entity: {name} ({key} was not found )")
        { 
            
        }
        public int Id { get; }
    }
}
