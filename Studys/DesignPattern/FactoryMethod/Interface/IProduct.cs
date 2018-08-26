namespace FactoryMethod.Interface
{
    public interface IProduct
    {
        void Use();

        // IProductのメンバーに変更
        string Owner { get; }
    }
}