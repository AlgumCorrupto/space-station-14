namespace Content.Client.Dmm.Interpreter
{
    public interface IDmmParser
    {
        public DmmMap Parse(byte[] content);
    }
}
