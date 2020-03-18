using Antlr4.Runtime;
using QueryFiltering.AntlrGenerated;
using QueryFiltering.Visitors;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable enable

[assembly: InternalsVisibleTo("QueryFiltering.Tests")]
namespace QueryFiltering
{
    public static class QueryFilteringExtensions
    {
        public static IQueryable<T> ApplyQuery<T>(this IQueryable<T> sourceQueryable, string query)
        {
            return (IQueryable<T>)ApplyQuery((IQueryable)sourceQueryable, query);
        }

        public static IQueryable ApplyQuery(this IQueryable sourceQueryable, string query)
        {
            var parser = new QueryFilteringParser(
                new CommonTokenStream(
                    new QueryFilteringLexer(
                        new AntlrInputStream(query))));

            var visitor = new QueryVisitor(sourceQueryable);
            var resultQueryable = parser.query().Accept(visitor);

            return resultQueryable;
        }
    }
}
