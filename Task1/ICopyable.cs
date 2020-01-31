using Task1.Storages.Abstractions;

namespace Task1
{
    public interface ICopyable
    {
        void Copy(BaseStorage destination);
        bool CanBeCopied(BaseStorage destination);
    }
}