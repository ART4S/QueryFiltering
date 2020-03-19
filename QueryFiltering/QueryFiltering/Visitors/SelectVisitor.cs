using QueryFiltering.AntlrGenerated;
using QueryFiltering.Helpers;
using QueryFiltering.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryFiltering.Visitors
{
    internal class SelectVisitor : QueryFilteringBaseVisitor<IQueryable>
    {
        private readonly IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public SelectVisitor(IQueryable sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override IQueryable VisitSelect(QueryFilteringParser.SelectContext context)
        {
            return context.expression.Accept(this);
        }

        public override IQueryable VisitSelectExpression(QueryFilteringParser.SelectExpressionContext context)
        {
            var properties = context.PROPERTYACCESS()
                .Select(x => x.Symbol.Text.ToLower())
                .ToHashSet();

            Type dictType = typeof(Dictionary<string, object>);
            MethodInfo addMethod = dictType.GetMethod("Add");

            ElementInit[] elementInitProperties = _parameter.Type
                .GetProperties()
                .Select(prop => prop.Name)
                .Where(propName => properties.Contains(propName.ToLower()))
                .Select(p => Expression.ElementInit(
                    addMethod,
                    Expression.Constant(p),
                    Expression.Convert(
                        new PropertyNode(p, _parameter).CreateExpression(),
                        typeof(object))))
                .ToArray();

            ListInitExpression body = Expression.ListInit(
                Expression.New(dictType), elementInitProperties);

            MethodInfo lambda = TypeCashe.Expression.LambdaFunc(_parameter.Type, dictType);

            object expression = lambda.Invoke(
                null,
                new object[] { body, new ParameterExpression[] { _parameter } });

            MethodInfo select = TypeCashe.Queryable.Select(_parameter.Type, dictType);

            return (IQueryable)select.Invoke(null, new[] { _sourceQueryable, expression });
        }
    }
}
