using QueryFiltering.AntlrGenerated;
using QueryFiltering.Helpers;
using QueryFiltering.Nodes;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryFiltering.Visitors
{
    internal class OrderByVisitor : QueryFilteringBaseVisitor<IQueryable>
    {
        private IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public OrderByVisitor(IQueryable sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override IQueryable VisitOrderBy(QueryFilteringParser.OrderByContext context)
        {
            return context.expression.Accept(this);
        }

        public override IQueryable VisitOrderByExpression(QueryFilteringParser.OrderByExpressionContext context)
        {
            foreach (var atom in context.orderByAtom())
            {
                _sourceQueryable = atom.Accept(this);
            }

            return _sourceQueryable;
        }

        public override IQueryable VisitOrderByAtom(QueryFilteringParser.OrderByAtomContext context)
        {
            Expression propertyExpression = new PropertyNode(context.propertyName.Text, _parameter).CreateExpression();

            MethodInfo lambda = ReflectionCache.Lambda.MakeGenericMethod(
                typeof(Func<,>).MakeGenericType(_parameter.Type, propertyExpression.Type));

            object expression = lambda.Invoke(null, new object[] { propertyExpression, new ParameterExpression[] { _parameter } });

            MethodInfo orderBy = (context.sortType == null || context.sortType.Type == QueryFilteringLexer.ASC
                    ? context.isFirstSort
                        ? ReflectionCache.OrderBy
                        : ReflectionCache.ThenBy
                    : context.isFirstSort
                        ? ReflectionCache.OrderByDescending
                        : ReflectionCache.ThenByDescending)
                .MakeGenericMethod(_parameter.Type, propertyExpression.Type);

            return (IQueryable)orderBy.Invoke(null, new[] { _sourceQueryable, expression });
        }
    }
}
