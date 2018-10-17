namespace ThreeTier.IBLL
{
    public interface IService<T>
    {
        T Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
