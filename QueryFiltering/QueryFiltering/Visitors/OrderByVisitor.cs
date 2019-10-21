﻿using QueryFiltering.Infrastructure;
using QueryFiltering.Nodes;
using System;
using System.Linq.Expressions;

namespace QueryFiltering.Visitors
{
    internal class OrderByVisitor : QueryFilteringBaseVisitor<object>
    {
        private object _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public OrderByVisitor(object sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override object VisitOrderBy(QueryFilteringParser.OrderByContext context)
        {
            return context.expression.Accept(this);
        }

        public override object VisitOrderByExpression(QueryFilteringParser.OrderByExpressionContext context)
        {
            foreach (var atom in context.orderByAtom())
            {
                _sourceQueryable = atom.Accept(this);
            }

            return _sourceQueryable;
        }

        public override object VisitOrderByAtom(QueryFilteringParser.OrderByAtomContext context)
        {
            var property = new PropertyNode(context.propertyName.Text, _parameter).CreateExpression();

            var lambda = ReflectionCache.Lambda.MakeGenericMethod(
                typeof(Func<,>).MakeGenericType(_parameter.Type, property.Type));

            var expression = lambda.Invoke(null, new object[] { property, new ParameterExpression[] { _parameter } });
            
            var orderBy = (context.sortType == null || context.sortType.Type == QueryFilteringLexer.ASC ?
                    context.isFirstSort ? ReflectionCache.OrderBy : ReflectionCache.ThenBy :
                    context.isFirstSort ? ReflectionCache.OrderByDescending : ReflectionCache.ThenByDescending)
                .MakeGenericMethod(_parameter.Type, property.Type);

            return orderBy.Invoke(null, new[] { _sourceQueryable, expression });
        }
    }
}
