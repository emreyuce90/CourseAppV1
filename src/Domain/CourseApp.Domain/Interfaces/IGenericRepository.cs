using CourseApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CourseApp.Domain.Interfaces {
    public interface IGenericRepository<T> where T : BaseEntity, new() {
        Task<T?> CreateAsync(T entity);
        T? UpdateAsync(T entity);
        Task<bool> DeleteAsync(int Id);
        bool DeleteAsync(T entity);

        public DbSet<T> Table { get; }
        IQueryable<T> GetAllAsync(bool isTracking = true);
        IQueryable<T> GetAllAsync(Expression<Func<T, bool>> expression, bool isTracking = true);
        Task<T?> GetSingle(Expression<Func<T, bool>> expression, bool isTracking = true);
        Task<T?> GetById(int Id);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        Task<int> SaveAsync();
    }
}
