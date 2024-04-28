using System.Linq.Expressions;

namespace GuitarShop.Data.DataAccess
{
    public class QueryOptions<T>
    {
        // public properties for sorting and filtering
        public Expression<Func<T, object>> OrderBy { get; set; }
        public string OrderByDirection { get; set; } = "asc"; // default
        public Expression<Func<T, bool>> Where { get; set; }

        // read-only properties
        public bool HasWhere => Where != null;
        public bool HasOrderBy => OrderBy != null;

    }
}
