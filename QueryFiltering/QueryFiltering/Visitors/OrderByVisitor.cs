using System;
using QueryFiltering.AntlrGenerated;
using QueryFiltering.Helpers;
using QueryFiltering.Nodes;
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

            MethodInfo lambda = TypeCashe.Expression.LambdaFunc(_parameter.Type, propertyExpression.Type);

            object expression = lambda.Invoke(
                null,
                new object[] { propertyExpression, new ParameterExpression[] { _parameter } });

            Type[] args = { _parameter.Type, propertyExpression.Type };

            MethodInfo order = context.sortType == null || context.sortType.Type == QueryFilteringLexer.ASC
                    ? context.isFirstSort
                        ? TypeCashe.Queryable.OrderBy(args)
                        : TypeCashe.Queryable.ThenBy(args)
                    : context.isFirstSort
                        ? TypeCashe.Queryable.OrderByDescending(args)
                        : TypeCashe.Queryable.ThenByDescending(args);

            return (IQueryable)order.Invoke(null, new[] { _sourceQueryable, expression });
        }
    }
}
