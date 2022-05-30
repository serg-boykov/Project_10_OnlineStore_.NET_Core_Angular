using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BaseSpecification()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BaseSpecification(Expression<Func<T, bool>> criteria)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = 
            new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set;}

        public Expression<Func<T, object>> OrderByDescending { get; private set;}

        public int Take { get; private set;}

        public int Skip { get; private set;}

        public bool IsPagingEnabled { get; private set;}

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> OrderByDescExpression)
        {
            OrderByDescending = OrderByDescExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}