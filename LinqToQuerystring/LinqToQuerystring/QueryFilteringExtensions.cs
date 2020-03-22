using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Antlr4.Runtime;
using LinqToQuerystring.AntlrGenerated;
using LinqToQuerystring.Visitors;

[assembly: InternalsVisibleTo("LinqToQuerystring.Tests")]
namespace LinqToQuerystring
{
    public static class LinqToQuerystringExtensions
    {
        public static IQueryable<T> ApplyQuery<T>(this IQueryable<T> sourceQueryable, string query)
        {
            return (IQueryable<T>)ApplyQuery((IQueryable)sourceQueryable, query);
        }

        public static IQueryable ApplyQuery(this IQueryable sourceQueryable, string query)
        {
            if (sourceQueryable == null) throw new ArgumentNullException(nameof(sourceQueryable));
            if (query == null) throw new ArgumentNullException(nameof(query));

            var parser = new LinqToQuerystringParser(
                new CommonTokenStream(
                    new LinqToQuerystringLexer(
                        new AntlrInputStream(query))));

            QueryVisitor visitor = new QueryVisitor(sourceQueryable);
            IQueryable resultQueryable = parser.query().Accept(visitor);

            return resultQueryable;
        }
    }
}
