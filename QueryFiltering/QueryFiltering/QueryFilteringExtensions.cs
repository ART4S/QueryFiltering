﻿using Antlr4.Runtime;
using QueryFiltering.AntlrGenerated;
using QueryFiltering.Visitors;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

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
            if (sourceQueryable == null) throw new ArgumentNullException(nameof(sourceQueryable));
            if (query == null) throw new ArgumentNullException(nameof(query));

            var parser = new QueryFilteringParser(
                new CommonTokenStream(
                    new QueryFilteringLexer(
                        new AntlrInputStream(query))));

            QueryVisitor visitor = new QueryVisitor(sourceQueryable);
            IQueryable resultQueryable = parser.query().Accept(visitor);

            return resultQueryable;
        }
    }
}
