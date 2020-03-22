using LinqToQuerystring.AntlrGenerated;
using LinqToQuerystring.Helpers;
using LinqToQuerystring.Nodes;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqToQuerystring.Visitors
{
    internal class OrderByVisitor : LinqToQuerystringBaseVisitor<IQueryable>
    {
        private IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public OrderByVisitor(IQueryable sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override IQueryable VisitOrderBy(LinqToQuerystringParser.OrderByContext context)
        {
            return context.expression.Accept(this);
        }

        public override IQueryable VisitOrderByExpression(LinqToQuerystringParser.OrderByExpressionContext context)
        {
            foreach (var atom in context.orderByAtom())
            {
                _sourceQueryable = atom.Accept(this);
            }

            return _sourceQueryable;
        }

        public override IQueryable VisitOrderByAtom(LinqToQuerystringParser.OrderByAtomContext context)
        {
            Expression propertyExpression = new PropertyNode(context.propertyName.Text, _parameter).CreateExpression();

            MethodInfo lambda = TypeCashe.Expression.LambdaFunc(_parameter.Type, propertyExpression.Type);

            object expression = lambda.Invoke(
                null,
                new object[] { propertyExpression, new ParameterExpression[] { _parameter } });

            Type[] args = { _parameter.Type, propertyExpression.Type };

            MethodInfo order = context.sortType == null || context.sortType.Type == LinqToQuerystringLexer.ASC
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
